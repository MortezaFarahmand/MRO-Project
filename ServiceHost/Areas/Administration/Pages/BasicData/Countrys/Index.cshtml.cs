using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BasicDataManagement.Application.Contracts.Country;

namespace ServiceHost.Areas.Administration.Pages.BasicData.Countrys
{
    public class IndexModel : PageModel
    {
        public CountrySearchModel SearchModel;
        public List<CountryViewModel> Countrys;
        private readonly ICountryApplication _countryApplication;

        public IndexModel(ICountryApplication countryApplication)
        {
            _countryApplication = countryApplication;
        }



        public void OnGet(CountrySearchModel searchModel)
        {
            Countrys = _countryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateCountry());
        }

        public JsonResult OnPostCreate(CreateCountry command)
        {
            var result = _countryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var country = _countryApplication.GetDetails(id);
            return Partial("Edit", country);
        }

        public JsonResult OnPostEdit(EditCountry command)
        {
            var result = _countryApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
