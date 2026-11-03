using FleetManagement.Application.Contracts.AircraftModel;
using FleetManagement.Application.Contracts.AircraftModel.Commands;
using FleetManagement.Application.Contracts.AircraftModel.DTOs;
using FleetManagement.Application.Contracts.AircraftModel.Queries;
using FleetManagement.Application.Contracts.AircraftType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Fleet.AircraftModels
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public SearchAircraftModelQuery SearchModel { get; set; } = new();

        public List<AircraftModelDto> AircraftModels { get; set; } = new();
        public SelectList AircraftTypes { get; set; }

        private readonly IAircraftModelApplication _application;
        private readonly IAircraftTypeApplication _typeApplication;

        public IndexModel(IAircraftModelApplication application, IAircraftTypeApplication typeApplication)
        {
            _application = application;
            _typeApplication = typeApplication;
        }

        public void OnGet()
        {
            AircraftTypes = new SelectList(_typeApplication.GetAllActive(), "Id", "Name");
            AircraftModels = _application.Search(SearchModel) ?? new();
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateAircraftModelCommand
            {
                AircraftTypes = _typeApplication.GetAllActive()
                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.Code} - {x.Name}" })
                    .ToList()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateAircraftModelCommand command)
        {
            return new JsonResult(_application.Create(command));
        }

        public IActionResult OnGetEdit(long id)
        {
            var item = _application.GetById(id);
            if (item == null) return NotFound();

            var command = new EditAircraftModelCommand
            {
                Id = item.Id,
                Name = item.Name,
                Manufacturer = item.Manufacturer,
                Description = item.Description
            };
            return Partial("./Edit", command);
        }

        public JsonResult OnPostEdit(EditAircraftModelCommand command)
        {
            return new JsonResult(_application.Edit(command));
        }

        public IActionResult OnGetActivate(long id)
        {
            Message = _application.Activate(id).Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetDeactivate(long id)
        {
            Message = _application.Deactivate(id).Message;
            return RedirectToPage("./Index");
        }
    }
}