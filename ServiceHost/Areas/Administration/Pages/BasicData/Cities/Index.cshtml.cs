using BasicDataManagement.Application;
using BasicDataManagement.Application.Contracts.City;
using BasicDataManagement.Application.Contracts.Country;
using BasicDataManagement.Application.Contracts.Province;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.BasicData.Cities
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public CitySearchModel SearchModel;
        public List<CityViewModel> Citys;
        public SelectList Provinces;
        public SelectList Countriii;

        private readonly ICityApplication _cityApplication;
        private readonly IProvinceApplication _provinceApplication;
        private readonly ICountryApplication _countryApplication;

        public IndexModel(ICityApplication cityApplication, IProvinceApplication provinceApplication, ICountryApplication countryApplication)
        {
            _cityApplication = cityApplication;
            _provinceApplication = provinceApplication;
            _countryApplication = countryApplication;
        }



        public void OnGet(CitySearchModel searchModel)
        {
            Provinces = new SelectList(_provinceApplication.GetProvinces(), "Id", "Name");
            Countriii = new SelectList(_countryApplication.GetCountrys(), "Id", "Name");
            Citys = _cityApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateCity();
            {
                command.Provincies = _provinceApplication.GetProvinces();
            }
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateCity command)
        {
            var result = _cityApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var city = _cityApplication.GetDetails(id);
            city.Provincies = _provinceApplication.GetProvinces(); 
            return Partial("Edit", city);
        }

        public JsonResult OnPostEdit(EditCity command)
        {
            var result = _cityApplication.Edit(command);
            return new JsonResult(result);
        }

    }
}
