using FleetManagement.Application.Contracts.Aircraft;
using FleetManagement.Application.Contracts.Aircraft.Commands;
using FleetManagement.Application.Contracts.Aircraft.DTOs;
using FleetManagement.Application.Contracts.Aircraft.Queries;
using FleetManagement.Application.Contracts.AircraftModel;
using FleetManagement.Application.Contracts.AircraftModel.Queries;
using FleetManagement.Domain.AircraftAgg.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Fleet.Aircrafts
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public SearchAircraftQuery SearchModel { get; set; } = new();

        public List<AircraftDto> Aircrafts { get; set; } = new();
        public SelectList Models { get; set; }

        private readonly IAircraftApplication _application;
        private readonly IAircraftModelApplication _modelApplication;

        public IndexModel(IAircraftApplication application, IAircraftModelApplication modelApplication)
        {
            _application = application;
            _modelApplication = modelApplication;
        }

        public void OnGet()
        {
            Models = new SelectList(_modelApplication.Search(new SearchAircraftModelQuery { IsActive = true }), "Id", "Name");
            Aircrafts = _application.Search(SearchModel) ?? new();
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateAircraftCommand
            {
                AircraftModels = _modelApplication.Search(new SearchAircraftModelQuery { IsActive = true })
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = $"{x.Code} - {x.Name}"
                    }).ToList()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateAircraftCommand command)
        {
            return new JsonResult(_application.Create(command));
        }

        public IActionResult OnGetEdit(long id)
        {
            var item = _application.GetById(id);
            if (item == null) return NotFound();

            var command = new EditAircraftCommand
            {
                Id = item.Id,
                Registration = item.Registration,
                OwnerOrganizationId = item.OwnerOrganizationId,
                OperatorOrganizationId = item.OperatorOrganizationId,
                ManufactureDate = item.ManufactureDate,
                DeliveryDate = item.DeliveryDate,
                RegistrationDate = item.RegistrationDate,
                LineNumber = item.LineNumber,
                VariableNumber = item.VariableNumber,
                Note = item.Note
            };
            return Partial("./Edit", command);
        }

        public JsonResult OnPostEdit(EditAircraftCommand command)
        {
            return new JsonResult(_application.Edit(command));
        }

        public IActionResult OnGetChangeStatus(long id, AircraftStatus status)
        {
            var result = _application.ChangeStatus(new ChangeAircraftStatusCommand { Id = id, Status = status });
            Message = result.Message;
            return RedirectToPage("./Index");
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