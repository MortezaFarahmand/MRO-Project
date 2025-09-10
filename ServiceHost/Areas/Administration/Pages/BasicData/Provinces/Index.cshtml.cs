using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BasicDataManagement.Application.Contract.Province;
using BasicDataManagement.Application.Contracts.Country;

namespace ServiceHost.Areas.Administration.Pages.BasicData.Provinces
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ProvinceSearchModel SearchModel;
        public List<ProvinceViewModel> Provinces;
        public SelectList Countrys;

        private readonly IProvinceApplication _provinceApplication;
        private readonly ICountryApplication _countryApplication;

        public IndexModel(IProvinceApplication provinceApplication, ICountryApplication countryApplication)
        {
            _provinceApplication = provinceApplication;
            _countryApplication = countryApplication;
        }



        public void OnGet(ProvinceSearchModel searchModel)
        {
            Countrys = new SelectList(_countryApplication.GetCountrys(), "Id", "Name");
            Provinces = _provinceApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProvince();
            {
                command.Countries = _countryApplication.GetCountrys();
            }
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateProvince command)
        {
            var result = _provinceApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var province = _provinceApplication.GetDetails(id);
            province.Countries = _countryApplication.GetCountrys(); 
            return Partial("Edit", province);
        }

        public JsonResult OnPostEdit(EditProvince command)
        {
            var result = _provinceApplication.Edit(command);
            return new JsonResult(result);
        }

    }
}
