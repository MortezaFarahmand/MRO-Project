using BasicDataManagement.Application.Contracts.City;
using BasicDataManagement.Application.Contracts.Country;
using BasicDataManagement.Application.Contracts.Province;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonnelManagement.Application.Contracts.Person;
using PersonnelManagement.Application.Contracts.PersonGroup;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Personnel.Person
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; }
        public PersonSearchModel SearchModel;
        public List<PersonViewModel> Persons;
        public SelectList PersonGroups;
        public SelectList Countries;
        public SelectList Provinces;
        public SelectList Cities;


        private readonly IPersonApplication _personApplication;
        private readonly IPersonGroupApplication _personGroupApplicaton;
        private readonly ICountryApplication _countryApplicaton;
        private readonly IProvinceApplication _provinceApplicaton;
        private readonly ICityApplication _cityApplicaton;

        public IndexModel(IPersonApplication personApplication, IPersonGroupApplication personGroupApplicaton
            , ICountryApplication countryApplication, IProvinceApplication provinceApplication,
            ICityApplication cityApplication)
        {
            _personApplication = personApplication;
            _personGroupApplicaton = personGroupApplicaton;
            _countryApplicaton = countryApplication;
            _provinceApplicaton = provinceApplication;
            _cityApplicaton = cityApplication;
        }



        public void OnGet(PersonSearchModel searchModel)
        {
            PersonGroups = new SelectList(_personGroupApplicaton.GetPersonGroups(), "Id", "Description");
            Countries = new SelectList(_countryApplicaton.GetCountrys(), "Id", "Name");
            Provinces = new SelectList(_provinceApplicaton.GetProvinces(), "Id", "Name");
            Cities = new SelectList(_cityApplicaton.GetCitys(), "Id", "Name");
            Persons = _personApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreatePerson();
            {
                command.Groups = _personGroupApplicaton.GetPersonGroups();
                command.Countrys = _countryApplicaton.GetCountrys();
                command.Provinces = _provinceApplicaton.GetProvinces();
                command.Cities = _cityApplicaton.GetCitys();
            }
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreatePerson command)
        {
            var result = _personApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)  
        { 
            var persons = _personApplication.GetDetails(id);
            persons.Groups = _personGroupApplicaton.GetPersonGroups();
            persons.Countrys = _countryApplicaton.GetCountrys();
            persons.Provinces = _provinceApplicaton.GetProvinces();
            persons.Cities = _cityApplicaton.GetCitys();

            return Partial("Edit", persons);
        }

        public JsonResult OnPostEdit(EditPerson command)
        {
            var result = _personApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetActive(long id) 
        { 
            var result = _personApplication.Active(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;
            //return new JsonResult(result);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetDeActive(long id)
        {
            var result = _personApplication.DeActive(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;
            //return new JsonResult(result);
            return RedirectToPage("./Index");
        }
    }
}
