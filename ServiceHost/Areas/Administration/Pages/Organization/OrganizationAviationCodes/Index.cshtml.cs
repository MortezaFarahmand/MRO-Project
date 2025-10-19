using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrganizationManagement.Application.Contracts.OrganizationAviationCode;
using BasicDataManagement.Application.Contracts.Country;

namespace ServiceHost.Areas.Administration.Pages.Organization.OrganizationAviationCodes
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string message { get; set; }
        public OrganizationAviationCodeSearchModel SearchModel;
        public List<OrganizationAviationCodeViewModel> organizationAviationCodes;
        public SelectList Countrys;

        private readonly IOrganizationAviationCodeApplication _organizationAviationCodeApplication;
        private readonly ICountryApplication _countryApplication;

        public IndexModel(IOrganizationAviationCodeApplication OrganizationAviationCodeApplication, ICountryApplication countryApplication)
        {
            _organizationAviationCodeApplication = OrganizationAviationCodeApplication;
            _countryApplication = countryApplication;
        }



        public void OnGet(OrganizationAviationCodeSearchModel searchModel)
        {
            Countrys = new SelectList(_countryApplication.GetCountrys(), "Id", "Name");
            organizationAviationCodes = _organizationAviationCodeApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateOrganizationAviationCode
            {
                Countrys = _countryApplication.GetCountrys()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateOrganizationAviationCode command)
        {
            var result = _organizationAviationCodeApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var organizationAviationCode = _organizationAviationCodeApplication.GetDetails(id);
            organizationAviationCode.Countrys = _countryApplication.GetCountrys();
            return Partial("Edit", organizationAviationCode);
        }

        public JsonResult OnPostEdit(EditOrganizationAviationCode command)
        {
            var result = _organizationAviationCodeApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEnabled(long id)
        {
            var result = _organizationAviationCodeApplication.IsEnable(id);
            if(result.IsSucceeded)
                return RedirectToPage("./Index");

            message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetNotEnabled(long id)
        {
            var result = _organizationAviationCodeApplication.IsDisable(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
