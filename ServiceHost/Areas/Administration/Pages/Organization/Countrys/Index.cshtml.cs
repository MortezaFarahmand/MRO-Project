using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrganizationManagement.Application.Contracts.Country;

namespace ServiceHost.Areas.Administration.Pages.Organization.Countrys
{
    public class IndexModel : PageModel
    {
        public PersonSearchModel SearchModel;
        public List<CountryViewModel> Countrys;
        private readonly IPersonApplication _countryApplication;

        public IndexModel(IPersonApplication countryApplication)
        {
            _countryApplication = countryApplication;
        }



        public void OnGet(PersonSearchModel searchModel)
        {
            Countrys = _countryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreatePerson());
        }

        public JsonResult OnPostCreate(CreatePerson command)
        {
            var result = _countryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var country = _countryApplication.GetDetails(id);
            return Partial("Edit", country);
        }

        public JsonResult OnPostEdit(EditPerson command)
        {
            var result = _countryApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
