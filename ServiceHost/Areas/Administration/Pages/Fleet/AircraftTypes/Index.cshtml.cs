using FleetManagement.Application.Contracts.AircraftType;
using FleetManagement.Application.Contracts.AircraftType.Commands;
using FleetManagement.Application.Contracts.AircraftType.DTOs;
using FleetManagement.Application.Contracts.AircraftType.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Fleet.AircraftTypes
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public SearchAircraftTypeQuery SearchModel { get; set; } = new();

        public List<AircraftTypeDto> AircraftTypes { get; set; } = new();

        private readonly IAircraftTypeApplication _application;

        public IndexModel(IAircraftTypeApplication application)
        {
            _application = application;
        }

        public void OnGet()
        {
            AircraftTypes = _application.Search(SearchModel) ?? new();
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateAircraftTypeCommand());
        }

        public JsonResult OnPostCreate(CreateAircraftTypeCommand command)
        {
            return new JsonResult(_application.Create(command));
        }

        public IActionResult OnGetEdit(long id)
        {
            var item = _application.GetById(id);
            if (item == null) return NotFound();

            return Partial("./Edit", new EditAircraftTypeCommand
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description
            });
        }

        public JsonResult OnPostEdit(EditAircraftTypeCommand command)
        {
            return new JsonResult(_application.Edit(command));
        }

        public IActionResult OnGetActivate(long id)
        {
            var result = _application.Activate(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetDeactivate(long id)
        {
            var result = _application.Deactivate(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}