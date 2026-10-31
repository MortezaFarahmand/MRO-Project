using _0_Framework.Application;
using _0_Framework.Domain;
using PartManagement.Application.Contracts.Part;
using PartManagement.Application.Contracts.Part.Commands;
using PartManagement.Application.Contracts.Part.DTOs;
using PartManagement.Application.Contracts.Part.Queries;
using PartManagement.Domain.PartAgg;

namespace PartManagement.Application
{
    public class PartApplication : IPartApplication
    {
        private readonly IPartRepository _partRepository;
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly IAuthHelper _authHelper;

        public PartApplication(
            IPartRepository partRepository,
            IDomainEventDispatcher dispatcher,
            IAuthHelper authHelper)
        {
            _partRepository = partRepository;
            _dispatcher = dispatcher;
            _authHelper = authHelper;
        }

        // ==================== Commands ====================

        public OperationResult Create(CreatePartCommand command)
        {
            if (_partRepository.Exists(command.PartNumber))
                return new OperationResult().Failed("شماره قطعه تکراری است");

            var part = PartManagement.Domain.PartAgg.Part.Create(
                command.PartNumber,
                command.Description,
                command.ControlType,
                command.IsGlobal,
                command.OwnerOrganizationId,
                _authHelper.CurrentAccountId());

            _partRepository.Create(part);
            _partRepository.SaveChanges();

            part.RaiseCreatedEvent();
            _dispatcher.DispatchEvents(part.DomainEvents);
            part.ClearDomainEvents();

            return new OperationResult().Succeeded("قطعه با موفقیت ثبت شد");
        }

        public OperationResult Edit(EditPartCommand command)
        {
            var part = _partRepository.Get(command.Id);
            if (part == null)
                return new OperationResult().Failed("قطعه یافت نشد");

            part.Edit(command.Description, command.Keyword, command.Specification, command.Note, _authHelper.CurrentAccountId());

            _partRepository.SaveChanges();

            _dispatcher.DispatchEvents(part.DomainEvents);
            part.ClearDomainEvents();

            return new OperationResult().Succeeded();
        }

        public OperationResult MakeGlobal(MakePartGlobalCommand command)
        {
            var part = _partRepository.Get(command.Id);
            if (part == null)
                return new OperationResult().Failed("قطعه یافت نشد");

            part.MakeGlobal(_authHelper.CurrentAccountId());

            _partRepository.SaveChanges();

            _dispatcher.DispatchEvents(part.DomainEvents);
            part.ClearDomainEvents();

            return new OperationResult().Succeeded("قطعه به حالت سراسری تغییر یافت");
        }

        public OperationResult AddManufacturer(AddPartManufacturerCommand command)
        {
            var part = _partRepository.Get(command.PartId);
            if (part == null)
                return new OperationResult().Failed("قطعه یافت نشد");

            part.AddManufacturer(command.ManufacturerId, command.IsPrimary, command.Note);

            _partRepository.SaveChanges();

            _dispatcher.DispatchEvents(part.DomainEvents);
            part.ClearDomainEvents();

            return new OperationResult().Succeeded();
        }

        public OperationResult AddChild(AddPartChildCommand command)
        {
            var part = _partRepository.Get(command.ParentPartId);
            if (part == null)
                return new OperationResult().Failed("قطعه والد یافت نشد");

            part.AddChild(command.ChildPartId, command.Quantity, command.Level, command.RelationType);

            _partRepository.SaveChanges();

            _dispatcher.DispatchEvents(part.DomainEvents);
            part.ClearDomainEvents();

            return new OperationResult().Succeeded();
        }

        public OperationResult AddOrganization(AddPartOrganizationCommand command)
        {
            var part = _partRepository.Get(command.PartId);
            if (part == null)
                return new OperationResult().Failed("قطعه یافت نشد");

            part.AddOrganizationPermission(
                command.OrganizationId,
                command.IsOwner,
                command.CanUse,
                command.CanPurchase,
                command.CanSell,
                command.CanRepair,
                command.CanLease,
                command.CanShip,
                command.Note);

            _partRepository.SaveChanges();

            _dispatcher.DispatchEvents(part.DomainEvents);
            part.ClearDomainEvents();

            return new OperationResult().Succeeded();
        }

        // ==================== Queries ====================

        public PartDto GetById(long id)
        {
            var part = _partRepository.Get(id);
            if (part == null)
                return null;

            return MapToDto(part);
        }

        public PartDto GetByPartNumber(string partNumber)
        {
            var part = _partRepository.GetByPartNumber(partNumber);
            if (part == null)
                return null;

            return MapToDto(part);
        }

        public List<PartDto> Search(SearchPartsQuery query)
        {
            // در مرحله بعد با Query کامل‌تر پیاده‌سازی می‌کنیم
            // فعلاً نسخه ساده:
            var parts = _partRepository.Get(); // یا متد Search که بعداً می‌نویسیم

            return parts.Select(MapToDto).ToList();
        }

        // ==================== Private Mapper ====================

        private PartDto MapToDto(Domain.PartAgg.Part part)
        {
            return new PartDto
            {
                Id = part.Id,
                PartNumber = part.PartNumber,
                Revision = part.Revision,
                Description = part.Description,
                Keyword = part.Keyword,
                Specification = part.Specification,
                ControlType = part.ControlType.ToString(),
                IsGlobal = part.IsGlobal,
                OwnerOrganizationId = part.OwnerOrganizationId,
                Note = part.Note,
                PartTypeId = part.PartTypeId,
                EquipmentTypeId = part.EquipmentTypeId,
                ClassificationId = part.ClassificationId,
                PurchaseUnitId = part.PurchaseUnitId,
                StorageUnitId = part.StorageUnitId,
                StockPolicyId = part.StockPolicyId,
                FloatQuantity = part.FloatQuantity,
                AveragePrice = part.AveragePrice
            };
        }
    }
}