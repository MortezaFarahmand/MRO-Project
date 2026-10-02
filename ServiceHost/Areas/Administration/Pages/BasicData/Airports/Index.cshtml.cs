using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BasicDataManagement.Application.Contracts.Airport;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.BasicData.Airports
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<AirportViewModel> Airports;
        public AirportSearchModel SearchModel;

        private readonly IAirportApplication _airportApplication;

        public IndexModel(IAirportApplication airportApplication)
        {
            _airportApplication = airportApplication;
        }


        public void OnGet(AirportSearchModel searchModel)
        {
            Airports = _airportApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateAirport();
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateAirport command)
        {
            var result = _airportApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var airport = _airportApplication.GetDetails(id);
            return Partial("Edit", airport);
        }

        public JsonResult OnPostEdit(EditAirport command)
        {
            var result = _airportApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetActive(long id)
        {
            var result = _airportApplication.Activate(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetDeactive(long id)
        {
            var result = _airportApplication.Deactivate(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
