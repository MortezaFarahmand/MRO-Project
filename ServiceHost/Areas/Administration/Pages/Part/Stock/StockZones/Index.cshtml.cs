using _0_Framework.Application;
using _01_MROQuery.Contracts.AircraftModel;
using _01_MROQuery.Contracts.ATAChapter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PartManagement.Application.Contracts.Stock.Commands;
using PartManagement.Application.Contracts.Stock.DTOs;
using PartManagement.Application.Contracts.Stock.Store;
using PartManagement.Application.Contracts.Stock.StoreZone;
using PartManagement.Domain.StockAgg.StoreAgg.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ServiceHost.Areas.Administration.Pages.Part.Stock.StockZones
{
    public class IndexModel : PageModel
    {        
        public List<StoreZoneDto> Zones { get; set; } = new();
        public SelectList Stores { get; set; }

        [BindProperty(SupportsGet = true)]
        public long? StoreId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Code { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool? IsActive { get; set; }

        public StoreSearchModel SearchModel { get; set; } = new();
        [TempData]
        public string Message { get; set; }

        private readonly IStoreZoneApplication _zoneApplication;
        private readonly IStoreApplication _storeApplication;
        private readonly IAircraftModelQuery _aircraftModelQuery;
        private readonly IAtaChapterQuery _ataChapterQuery;
        private readonly IAuthHelper _authHelper;

        public IndexModel(
            IStoreZoneApplication zoneApplication,
            IStoreApplication storeApplication,
            IAircraftModelQuery aircraftModelQuery,
            IAtaChapterQuery ataChapterQuery,
            IAuthHelper authHelper)
        {
            _zoneApplication = zoneApplication;
            _storeApplication = storeApplication;
            _aircraftModelQuery = aircraftModelQuery;
            _ataChapterQuery = ataChapterQuery;
            _authHelper = authHelper;
        }




        public void OnGet()
        {
            LoadStores();
            Zones = _zoneApplication.Search(StoreId, Code, IsActive) ?? new();
        }

        public IActionResult OnGetCreate(long? storeId)
        {
            var stores = _storeApplication.Search(new StoreSearchModel())
                 ?? new List<StoreViewModel>();
            var aircraftModels = _aircraftModelQuery.GetAll();
            var ataChapters = _ataChapterQuery.GetAll();

            var command = new CreateStoreZoneCommand
            {
                StoreId = storeId ?? 0,
                Stores = new SelectList(stores, "Id", "Name", storeId),
                AircraftModels = new SelectList(aircraftModels, "Id", "Display"),
                AtaChapters = new SelectList(ataChapters, "Id", "Display"),
                ZoneTypes = EnumSelect<ZoneType>(),
                LocationKinds = EnumSelect<LocationKind>(),
                EnvironmentalClasses = EnumSelect<EnvironmentalClass>()
            };
            //FillCreateLookups(command.StoreId);
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateStoreZoneCommand command)
        {
            var result = _zoneApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {


            var dto = _zoneApplication.GetDetails(id);
            if (dto == null)
                return NotFound();

            var stores = _storeApplication.Search(new StoreSearchModel())
                 ?? new List<StoreViewModel>();
            var aircraftModels = _aircraftModelQuery.GetAll();
            var ataChapters = _ataChapterQuery.GetAll();

            var command = new EditStoreZoneCommand
            {
                Id = dto.Id,
                Name = dto.Name,
                ZoneType = dto.ZoneType,
                AtaChapterId = dto.AtaChapterId,
                KeywordGroup = dto.KeywordGroup,
                AircraftModelId = dto.AircraftModelId,
                LocationKind = dto.LocationKind,
                EnvironmentalClass = dto.EnvironmentalClass,
                MaxWeightKg = dto.MaxWeightKg,
                MaxVolumeM3 = dto.MaxVolumeM3,
                StoreId = dto.StoreId,
                StoreName = dto.StoreName,

                Stores = new SelectList(stores, "Id", "Name", dto.StoreId),
                AircraftModels = new SelectList(aircraftModels, "Id", "Display", dto.AircraftModelId),
                AtaChapters = new SelectList(ataChapters, "Id", "Display", dto.AtaChapterId),
                ZoneTypes = EnumSelect<ZoneType>(dto.ZoneType),
                LocationKinds = EnumSelect<LocationKind>(dto.LocationKind),
                EnvironmentalClasses = EnumSelect<EnvironmentalClass>(dto.EnvironmentalClass)
            };

            
            return Partial("./Edit", command);
        }

        public JsonResult OnPostEdit(EditStoreZoneCommand command)
        {
            var result = _zoneApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetActivate(long id)
        {
            var r = _zoneApplication.Activate(id);
            Message = r.Message;
            return RedirectToPage(new { StoreId });
        }

        public IActionResult OnGetDeactivate(long id)
        {
            var r = _zoneApplication.Deactivate(id);
            Message = r.Message;
            return RedirectToPage(new { StoreId });
        }

        private void LoadStores()
        {
            var list = _storeApplication.Search(new StoreSearchModel()) ?? new List<StoreViewModel>();
            Stores = new SelectList(list, "Id", "Name", StoreId);
        }

        private List<StoreViewModel> GetAllowedStores()
        {
            var model = new StoreSearchModel();
            if (!_authHelper.CurrentAccountIsAdmin())
                model.OrganizationId = _authHelper.CurrentAccountOrganizationId();
            return _storeApplication.Search(model) ?? new();
        }

        //private void FillCreateLookups(long selectedStoreId)
        //{
        //    var list = _storeApplication.Search(new StoreSearchModel()) ?? new List<StoreViewModel>();
        //    ViewData["Stores"] = new SelectList(list, "Id", "Name");
        //    ViewData["ZoneTypes"] = EnumSelect<ZoneType>();
        //    ViewData["LocationKinds"] = EnumSelect<LocationKind>();
        //    ViewData["EnvClasses"] = EnumSelect<EnvironmentalClass>();
        //}

        private static SelectList EnumSelect<T>(object selected = null) where T : struct
        {
            var items = Enum.GetValues(typeof(T)).Cast<T>()
                .Select(e => new { Id = Convert.ToInt32(e), Name = e.ToString() });
            return new SelectList(items, "Id", "Name", selected);
        }
    }
}
