using _0_Framework.Application;
using _01_MROQuery.Contracts.ATAChapter;
using BasicDataManagement.Domain.ATAChapterAgg;
using PartManagement.Application.Contracts.Stock.Commands;
using PartManagement.Application.Contracts.Stock.DTOs;
using PartManagement.Application.Contracts.Stock.StoreZone;
using PartManagement.Domain.Stock.StoreAgg;
using PartManagement.Domain.StockAgg.StoreAgg;
using System.Security.Cryptography.X509Certificates;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PartManagement.Application.Stock
{
    public class StoreZoneApplication : IStoreZoneApplication
    {
        private readonly IStoreZoneRepository _zoneRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IAtaChapterQuery _ataChapterQuery;
        private readonly IAuthHelper _authHelper;

        public StoreZoneApplication(
            IStoreZoneRepository zoneRepository,
            IStoreRepository storeRepository,
            IAtaChapterQuery ataChapterQuery,
            IAuthHelper authHelper)
        {
            _zoneRepository = zoneRepository;
            _storeRepository = storeRepository;
            _ataChapterQuery = ataChapterQuery;
            _authHelper = authHelper;
        }

        private bool IsAdmin => _authHelper.CurrentAccountIsAdmin();
        private long? CurrentOrgId => _authHelper.CurrentAccountOrganizationId();

        private OperationResult EnsureStoreAccess(long storeId)
        {
            var result = new OperationResult();
            var store = _storeRepository.Get(storeId);
            if (store == null)
                return result.Failed("انبار یافت نشد.");

            if (!IsAdmin && store.OrganizationId != CurrentOrgId)
                return result.Failed("به این انبار دسترسی ندارید.");

            return result.Succeeded();
        }

        private OperationResult EnsureZoneAccess(long zoneId, out StoreZone zone)
        {
            var result = new OperationResult();
            zone = _zoneRepository.Get(zoneId);
            if (zone == null)
                return result.Failed("Zone یافت نشد.");

            return EnsureStoreAccess(zone.StoreId);
        }

        public OperationResult Create(CreateStoreZoneCommand command)
        {
            var result = new OperationResult();

            var access = EnsureStoreAccess(command.StoreId);
            if (!access.IsSucceeded)
                return access;

            if (command.StoreId <= 0)
                return result.Failed("انبار را انتخاب کنید.");
            if (string.IsNullOrWhiteSpace(command.Code))
                return result.Failed("کد Zone الزامی است.");
            if (string.IsNullOrWhiteSpace(command.Name))
                return result.Failed("نام Zone الزامی است.");

            var store = _storeRepository.Get(command.StoreId);
            if (store == null)
                return result.Failed("انبار یافت نشد.");

            var code = command.Code.Trim();
            if (_zoneRepository.Exists(x => x.StoreId == command.StoreId && x.Code == code))
                return result.Failed("کد Zone در این انبار تکراری است.");

            try
            {
                var entity = new StoreZone(
                    command.StoreId,
                    code,
                    command.Name,
                    command.ZoneType,
                    command.AtaChapterId,
                    command.KeywordGroup,
                    command.AircraftModelId,
                    command.LocationKind,
                    command.EnvironmentalClass,
                    command.MaxWeightKg,
                    command.MaxVolumeM3);

                _zoneRepository.Create(entity);
                _zoneRepository.SaveChanges();
                return result.Succeeded("Zone ثبت شد.");
            }
            catch (System.Exception ex)
            {
                return result.Failed(ex.Message);
            }
        }

        public OperationResult Edit(EditStoreZoneCommand command)
        {
            var result = new OperationResult();
            //var entity = _zoneRepository.Get(command.Id);
            //if (entity == null)
            //    return result.Failed("Zone یافت نشد.");
            var access = EnsureZoneAccess(command.Id, out var entity);
            if (!access.IsSucceeded)
                return access;

            if (string.IsNullOrWhiteSpace(command.Name))
                return result.Failed("نام Zone الزامی است.");

            try
            {
                entity.Edit(
                    command.Name,
                    command.ZoneType,
                    command.AtaChapterId,
                    command.KeywordGroup,
                    command.AircraftModelId,
                    command.LocationKind,
                    command.EnvironmentalClass,
                    command.MaxWeightKg,
                    command.MaxVolumeM3);

                _zoneRepository.SaveChanges();
                return new OperationResult().Succeeded("ویرایش شد.");
            }
            catch (System.Exception ex)
            {
                return result.Failed(ex.Message);
            }
        }

        public OperationResult Activate(long id)
        {
            //var result = new OperationResult();
            //var entity = _zoneRepository.Get(id);
            //if (entity == null) return result.Failed("یافت نشد.");
            var access = EnsureZoneAccess(id, out var entity);
            if (!access.IsSucceeded)
                return access;

            entity.Activate();
            _zoneRepository.SaveChanges();
            //return result.Succeeded();
            return new OperationResult().Succeeded();
        }

        public OperationResult Deactivate(long id)
        {
            //var result = new OperationResult();
            //var entity = _zoneRepository.Get(id);
            //if (entity == null) return result.Failed("یافت نشد.");
            var access = EnsureZoneAccess(id, out var entity);
            if (!access.IsSucceeded)
                return access;

            entity.Deactivate();
            _zoneRepository.SaveChanges();
            //return result.Succeeded();
            return new OperationResult().Succeeded();
        }

        public StoreZoneDto GetDetails(long id)
        {
            var zone = _zoneRepository.Get(id);
            if (zone == null) return null;

            var store = _storeRepository.Get(zone.StoreId);
            if (store == null) return null;

            if (!IsAdmin && store.OrganizationId != CurrentOrgId)
                return null; // UI → NotFound / Forbid

            return Map(zone);
        }

        public List<StoreZoneDto> GetByStore(long storeId)
        => MapList(_zoneRepository.GetByStore(storeId));

        public List<StoreZoneDto> Search(long? storeId, string code, bool? isActive)
        {
            var query = _zoneRepository.Get().AsQueryable();

            if (!IsAdmin)
            {
                var orgId = CurrentOrgId;
                var allowedStoreIds = _storeRepository.Get()
                    .Where(s => s.OrganizationId == orgId)
                    .Select(s => s.Id)
                    .ToList();

                query = query.Where(z => allowedStoreIds.Contains(z.StoreId));
            }

            if (storeId.HasValue && storeId > 0)
                query = query.Where(x => x.StoreId == storeId.Value);

            if (!string.IsNullOrWhiteSpace(code))
                query = query.Where(x => x.Code.Contains(code.Trim()));

            if (isActive.HasValue)
                query = query.Where(x => x.IsActive == isActive.Value);

            return MapList(query.OrderBy(z => z.StoreId).ThenBy(z => z.Code).ToList());
        }

        private List<StoreZoneDto> MapList(List<StoreZone> zones)
        {
            var storeIds = zones.Select(z => z.StoreId).Distinct().ToList();

            var storeNames = _storeRepository.Get()
                .Where(s => storeIds.Contains(s.Id))
                .ToDictionary(s => s.Id, s => s.Name);

            var ataIds = zones
                .Where(z => z.AtaChapterId.HasValue)
                .Select(z => z.AtaChapterId.Value)
                .Distinct()
                .ToList();

            var ataMap = _ataChapterQuery.GetByIds(ataIds)
                .ToDictionary(a => (long)a.Id, a => a.Display);

            return zones.Select(z => Map(z, storeNames, ataMap)).ToList();
        }

        private StoreZoneDto Map(StoreZone x,
            IReadOnlyDictionary<long, string> storeNames = null,
            IReadOnlyDictionary<long, string> ataMap = null)
        {
            string? storeName = null;
            if (storeNames != null)
                storeNames.TryGetValue(x.StoreId, out storeName);
            else
                storeName = _storeRepository.Get(x.StoreId)?.Name;

            string? ataDisplay = null;
            if (x.AtaChapterId.HasValue)
            {
                if (ataMap != null && ataMap.TryGetValue(x.AtaChapterId.Value, out var a))
                    ataDisplay = a;
                else if (_ataChapterQuery != null)
                    ataDisplay = _ataChapterQuery.GetById(x.AtaChapterId.Value)?.Display;
            }

            return new StoreZoneDto
            {
                Id = x.Id,
                StoreId = x.StoreId,
                StoreName = storeName,
                Code = x.Code,
                Name = x.Name,
                ZoneType = x.ZoneType,
                ZoneTypeTitle = x.ZoneType.ToString(),
                AtaChapterId = x.AtaChapterId,
                AtaChapterDisplay = ataDisplay,
                KeywordGroup = x.KeywordGroup,
                AircraftModelId = x.AircraftModelId,
                LocationKind = x.LocationKind,
                EnvironmentalClass = x.EnvironmentalClass,
                MaxWeightKg = x.MaxWeightKg,
                MaxVolumeM3 = x.MaxVolumeM3,
                IsActive = x.IsActive
            };
        }
    }
}
