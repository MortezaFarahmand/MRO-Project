using _0_Framework.Application;
using PartManagement.Application.Contracts.Part;
using PartManagement.Application.Contracts.Part.Commands;
using PartManagement.Application.Contracts.Part.DTOs;
using PartManagement.Application.Contracts.Part.Queries;
using PartManagement.Domain.PartAgg;
using PartManagement.Domain.PartAgg.Enum;

namespace PartManagement.Application
{
    public class PartAlternateApplication : IPartAlternateApplication
    {
        private readonly IPartAlternateRepository _repository;
        private readonly IPartRepository _partRepository;

        public PartAlternateApplication(
            IPartAlternateRepository repository,
            IPartRepository partRepository)
        {
            _repository = repository;
            _partRepository = partRepository;
        }

        // ==================== Commands ====================

        public OperationResult Create(CreatePartAlternateCommand command)
        {
            var result = new OperationResult();

            if (command.PrimaryPartId <= 0)
                return result.Failed("قطعه اصلی الزامی است.");

            if (command.AlternatePartIds == null || !command.AlternatePartIds.Any())
                return result.Failed("حداقل یک قطعه جایگزین انتخاب کنید.");

            if (!_partRepository.Exists(x => x.Id == command.PrimaryPartId))
                return result.Failed("قطعه اصلی یافت نشد.");

            // پیش‌فرض RelationType = TwoWay
            var relationType = command.RelationType == null
                ? AlternateRelationType.TwoWay
                : command.RelationType;

            // SuperSession بدون مقدار صریح → پیش‌فرض OneWay
            if (command.AlternateCategory == AlternateCategory.SuperSession
                && command.RelationType == null )
            {
                relationType = AlternateRelationType.OneWay;
            }

            var preference = command.PreferenceLevel <= 0 ? 1 : command.PreferenceLevel;
            var isApproved = command.IsApproved ?? true; // پیش‌فرض تأییدشده
            var added = 0;

            foreach (var altId in command.AlternatePartIds.Distinct())
            {
                if (altId == command.PrimaryPartId)
                    continue;

                if (!_partRepository.Exists(x => x.Id == altId))
                    continue;

                if (_repository.Exists(command.PrimaryPartId, altId))
                    continue;

                var entity = new PartAlternate(
                    command.PrimaryPartId,
                    altId,
                    relationType,
                    command.AlternateCategory,
                    preference,
                    isApproved,
                    command.EffectiveFrom,
                    command.EffectiveTo,
                    command.Notes);

                _repository.Create(entity);
                added++;

                // TwoWay → رابطه معکوس در صورت نبود
                if (relationType == AlternateRelationType.TwoWay
                    && !_repository.Exists(altId, command.PrimaryPartId))
                {
                    _repository.Create(new PartAlternate(
                        altId,
                        command.PrimaryPartId,
                        AlternateRelationType.TwoWay,
                        command.AlternateCategory,
                        command.PreferenceLevel,
                        isApproved,
                        command.EffectiveFrom,
                        command.EffectiveTo,
                        command.Notes) );
                }
            }
                _repository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult SyncForPrimary(EditPartAlternatesForPartCommand command)
        {
            var result = new OperationResult();

            if (command.PrimaryPartId <= 0)
                return result.Failed("قطعه اصلی الزامی است.");

            if (!_partRepository.Exists(x => x.Id == command.PrimaryPartId))
                return result.Failed("قطعه اصلی یافت نشد.");

            var relationType = command.RelationType == null
                ? AlternateRelationType.TwoWay
                : command.RelationType;

            var preference = command.PreferenceLevel <= 0 ? 1 : command.PreferenceLevel;
            var isApproved = command.IsApproved ?? true;
            var newIds = (command.AlternatePartIds ?? new List<long>())
                .Where(id => id != command.PrimaryPartId)
                .Distinct()
                .ToHashSet();

            var existing = _repository.Search(command.PrimaryPartId, null, null, null, null);
            var existingAltIds = existing.Select(x => x.AlternatePartId).ToHashSet();

            // 1. حذف روابطی که دیگر در لیست نیستند
            foreach (var entity in existing.Where(x => !newIds.Contains(x.AlternatePartId)).ToList())
            {
                _repository.Remove(entity);

                if (entity.RelationType == AlternateRelationType.TwoWay)
                {
                    var reverseList = _repository.Search(entity.AlternatePartId, command.PrimaryPartId, null, null, null);
                    foreach (var rev in reverseList)
                        _repository.Remove(rev);
                }
            }

            // 2. اضافه کردن روابط جدید
            foreach (var altId in newIds.Where(id => !existingAltIds.Contains(id)))
            {
                if (!_partRepository.Exists(x => x.Id == altId))
                    continue;

                if (_repository.Exists(command.PrimaryPartId, altId))
                    continue;

                _repository.Create(new PartAlternate(
                    command.PrimaryPartId,
                    altId,
                    relationType,
                    command.AlternateCategory,
                    preference,
                    isApproved,
                    command.EffectiveFrom,
                    command.EffectiveTo,
                    command.Notes));

                if (relationType == AlternateRelationType.TwoWay
                    && !_repository.Exists(altId, command.PrimaryPartId))
                {
                    _repository.Create(new PartAlternate(
                        altId,
                        command.PrimaryPartId,
                        AlternateRelationType.TwoWay,
                        command.AlternateCategory,
                        preference,
                        isApproved,
                        command.EffectiveFrom,
                        command.EffectiveTo,
                        command.Notes));
                }
            }

            // 3. ★★★ آپدیت روابط موجود که هنوز در لیست هستند ★★★
            foreach (var entity in existing.Where(x => newIds.Contains(x.AlternatePartId)))
            {
                entity.Edit(
                    relationType,
                    command.AlternateCategory,
                    preference,
                    isApproved,
                    command.EffectiveFrom,
                    command.EffectiveTo,
                    command.Notes);

                // اگر TwoWay است، رابطه معکوس را هم آپدیت کن (اختیاری ولی توصیه می‌شود)
                if (relationType == AlternateRelationType.TwoWay)
                {
                    var reverseList = _repository.Search(entity.AlternatePartId, command.PrimaryPartId, null, null, null);
                    foreach (var rev in reverseList)
                    {
                        rev.Edit(
                            AlternateRelationType.TwoWay,
                            command.AlternateCategory,
                            preference,
                            isApproved,
                            command.EffectiveFrom,
                            command.EffectiveTo,
                            command.Notes);
                    }
                }
            }

            _repository.SaveChanges();
            return result.Succeeded();
        }


        public OperationResult Edit(EditPartAlternateCommand command)
        {
            var result = new OperationResult();
            var entity = _repository.Get(command.Id);
            if (entity == null)
                return result.Failed("رکورد یافت نشد.");

            entity.Edit(
                command.RelationType,
                command.AlternateCategory,
                command.PreferenceLevel,
                command.IsApproved,
                command.EffectiveFrom,
                command.EffectiveTo,
                command.Notes);

            _repository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Remove(long id)
        {
            var result = new OperationResult();
            var entity = _repository.Get(id);
            if (entity == null)
                return result.Failed("رکورد یافت نشد.");

            _repository.Remove(entity);
            _repository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Approve(long id)
        {
            var result = new OperationResult();
            var entity = _repository.Get(id);
            if (entity == null)
                return result.Failed("رکورد یافت نشد.");

            entity.Approve();
            _repository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult Unapprove(long id)
        {
            var result = new OperationResult();
            var entity = _repository.Get(id);
            if (entity == null)
                return result.Failed("رکورد یافت نشد.");

            entity.Unapprove();
            _repository.SaveChanges();
            return result.Succeeded();
        }

        // ==================== Queries ====================

        public PartAlternateDto GetDetails(long id)
        {
            var entity = _repository.GetWithParts(id);
            return entity == null ? null : MapToDto(entity);
        }

        public List<PartAlternateDto> GetApprovedAlternates(long partId)
        {
            return _repository.GetApprovedAlternates(partId)
                .Select(MapToDto)
                .ToList();
        }

        public List<PartAlternateDto> GetByPrimaryPart(long primaryPartId)
        {
            var entities = _repository.Search(primaryPartId, null, null, null, null);
            return entities.Select(MapToDto).ToList();
        }

        public List<PartAlternateDto> Search(PartAlternateSearchModel searchModel)
        {
            // فیلتر سمت Repository روی Entity — Domain خالص می‌ماند
            var entities = _repository.Search(
                searchModel.PrimaryPartId,
                searchModel.AlternatePartId,
                searchModel.PartNumber,
                searchModel.AlternateCategory,
                searchModel.IsApproved);

            return entities.Select(MapToDto).ToList();
        }

        // ==================== Private Mapper ====================

        private static PartAlternateDto MapToDto(PartAlternate entity)
        {
            return new PartAlternateDto
            {
                Id = entity.Id,
                PrimaryPartId = entity.PrimaryPartId,
                PrimaryPartNumber = entity.PrimaryPart.PartNumber,
                PrimaryPartDescription = entity.PrimaryPart?.Description,
                AlternatePartId = entity.AlternatePartId,
                AlternatePartNumber = entity.AlternatePart.PartNumber,
                AlternatePartDescription = entity.AlternatePart?.Description,
                RelationType = entity.RelationType,
                AlternateCategory = entity.AlternateCategory,
                PreferenceLevel = entity.PreferenceLevel,
                IsApproved = entity.IsApproved,
                EffectiveFrom = entity.EffectiveFrom,
                EffectiveTo = entity.EffectiveTo,
                Notes = entity.Notes,
                CreationDate = entity.CreationDate.ToString()
            };
        }
    }
}