using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BasicDataManagement.Application.Contracts.Entiti;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrganizationManagement.Application.Contracts.Country;
using OrganizationManagement.Application.Contracts.Organization;
using OrganizationManagement.Configuration.Permissions;
using OrganizationManagement.Domain.OrganizationAgg;
using PartManagement.Application.Contracts.Stock.Commands;
using PartManagement.Application.Contracts.Stock.DTOs;
using PartManagement.Application.Contracts.Stock.StockType;
using PartManagement.Application.Contracts.Stock.Store;
using PartManagement.Application.Contracts.Stock.StoreZone;
using PartManagement.Configuration.Permissions;
using PartManagement.Domain.Stock.StoreAgg;
using PartManagement.Domain.StockAgg.StoreAgg;
using PartManagement.Domain.StockAgg.StoreAgg.Enum;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Part.Stock.Stores
{
    //[Authorize(Roles = "1, 3")]
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public StoreSearchModel SearchModel { get; set; } = new();

        public List<StoreViewModel> Stores { get; set; }
        public SelectList StockTypes { get; set; }
        public SelectList Organizations { get; set; }

        private readonly IStoreApplication _storeApplication;
        private readonly IStockTypeApplication _stockTypeApplication;
        private readonly IOrganizationApplication _organizationApplication;
        private readonly IAuthHelper _authHelper;
        private readonly IStoreZoneApplication _storeZoneApplication;

        public IndexModel(
            IStoreApplication storeApplication,
            IStockTypeApplication stockTypeApplication,
            IOrganizationApplication organizationApplication,
            IAuthHelper authHelper,
            IStoreZoneApplication storeZoneApplication)
        {
            _storeApplication = storeApplication;
            _stockTypeApplication = stockTypeApplication;
            _organizationApplication = organizationApplication;
            _authHelper = authHelper;
            _storeZoneApplication = storeZoneApplication;
        }

        public void OnGet()
        {
            Organizations = new SelectList(_organizationApplication.GetOrganizations(), "Id", "NameEn");
            StockTypes = new SelectList(
                _stockTypeApplication.GetStockTypesBy(SearchModel.OrganizationId), "Id", "Name");
            Stores = _storeApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            var organizations = _organizationApplication.GetOrganizations()
                .Select(o => new OrganizationSimpleDto { Id = o.Id, NameEn = o.NameEn }).ToList();

            var command = new CreateStore
            {
                OrganizationId = _authHelper.CurrentAccountOrganizationId(),
                Organizations = organizations,
                IsAdmin = _authHelper.CurrentAccountIsAdmin(),
                StockTypeIds = new List<long>(),
                StockTypes = new List<StockTypeViewModel>()
            };

            if (command.OrganizationId.HasValue)
            {
                command.StockTypes =
                    _stockTypeApplication.GetStockTypesBy(command.OrganizationId)
                    ?? new List<StockTypeViewModel>();
            }

            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateStore command)
        {
            var result = _storeApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            long? organizationId = _authHelper.CurrentAccountOrganizationId();
            var store = _storeApplication.GetDetails(id);
            store.StockTypes = _stockTypeApplication.GetStockTypesBy(organizationId);
            store.Organizations = _organizationApplication.GetOrganizations()
                .Select(o => new OrganizationSimpleDto { Id = o.Id, NameEn = o.NameEn }).ToList();
            return Partial("Edit", store);
        }

        public JsonResult OnPostEdit(EditStore command)
        {
            var result = _storeApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetStockTypes(long organizationId)
        {
            var isAdmin = _authHelper.CurrentAccountIsAdmin();
            var accountOrganizationId = _authHelper.CurrentAccountOrganizationId();

            if (!isAdmin && organizationId != accountOrganizationId)
                return Forbid();

            var stockTypes = _stockTypeApplication.GetStockTypesBy(organizationId);
            return new JsonResult(stockTypes.Select(x => new StockTypeViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code
            }));
        }

        public IActionResult OnGetDeactive(long id)
        {
            var result = _storeApplication.Deactivate(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetIsActive(long id)
        {
            var result = _storeApplication.Activate(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        // ==================== Policies (مودال) ====================

        public IActionResult OnGetPolicies(long id)
        {
            var store = _storeApplication.GetDetails(id);
            if (store == null)
                return NotFound();

            var receiving = new UpdateStoreReceivingPolicyCommand
            {
                StoreId = id,
                InspectionMode = store.ReceivingInspectionMode ?? InspectionMode.None,
                InspectionDeadlineHours = store.InspectionDeadlineHours,
                AllowOneStepReceiveAndInspect = store.AllowOneStepReceiveAndInspect ?? false,
                DefaultReceivingHoldZoneId = store.DefaultReceivingHoldZoneId
            };

            var placement = new UpdateStorePlacementPolicyCommand
            {
                StoreId = id,
                Mode = store.PlacementMode ?? PlacementPolicyMode.Off,
                UseAtaChapter = store.UseAtaChapter ?? false,
                UseKeywordGroup = store.UseKeywordGroup ?? false,
                UseStockType = store.UseStockType ?? false,
                UseAircraftModel = store.UseAircraftModel ?? false,
                UsePartPhysical = store.UsePartPhysical ?? false,
                UseLocationKind = store.UseLocationKind ?? false,
                UseEnvironmentalClass = store.UseEnvironmentalClass ?? false
            };

            var zones = (_storeZoneApplication.GetByStore(id) ?? new List<StoreZoneDto>())
                .Where(z => z.IsActive &&
                            (z.ZoneType == ZoneType.ReceivingHold || z.ZoneType == ZoneType.Quarantine))
                .ToList();

            var model = new StorePoliciesViewModel
            {
                StoreId = id,
                StoreName = store.Name,
                Receiving = receiving,
                Placement = placement,
                HoldZones = zones
            };

            return Partial("./Policies", model);
        }

        public JsonResult OnPostReceivingPolicy(UpdateStoreReceivingPolicyCommand command)
        {
            var result = _storeApplication.UpdateReceivingPolicy(command);
            return new JsonResult(result);
        }

        public JsonResult OnPostPlacementPolicy(UpdateStorePlacementPolicyCommand command)
        {
            var result = _storeApplication.UpdatePlacementPolicy(command);
            return new JsonResult(result);
        }
    }

    public class StorePoliciesViewModel
    {
        public long StoreId { get; set; }
        public string StoreName { get; set; }
        public UpdateStoreReceivingPolicyCommand Receiving { get; set; }
        public UpdateStorePlacementPolicyCommand Placement { get; set; }
        public List<StoreZoneDto> HoldZones { get; set; } = new();
    }
}
