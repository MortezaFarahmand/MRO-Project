using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrganizationManagement.Application.Contracts.ApprovalAuthority;
using BasicDataManagement.Application.Contracts.Country;

namespace ServiceHost.Areas.Administration.Pages.Organization.ApprovalAuthorities
{
    public class IndexModel : PageModel
    {
        public ApprovalAuthoritySearchModel SearchModel;
        public List<ApprovalAuthorityViewModel> ApprovalAuthorities;
        public SelectList Countrys;

        private readonly IApprovalAuthorityApplication _approvalAuthorityApplication;
        private readonly ICountryApplication _countryApplication;

        public IndexModel(IApprovalAuthorityApplication approvalAuthorityApplication, ICountryApplication countryApplication)
        {
            _approvalAuthorityApplication = approvalAuthorityApplication;
            _countryApplication = countryApplication;
        }



        public void OnGet(ApprovalAuthoritySearchModel searchModel)
        {
            Countrys = new SelectList(_countryApplication.GetCountrys(), "Id", "Name");
            ApprovalAuthorities = _approvalAuthorityApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateApprovalAuthority();
            {
                //command.Countries = _countryApplication.GetCountrys();
            }
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateApprovalAuthority command)
        {
            var result = _approvalAuthorityApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var approvalAuthority = _approvalAuthorityApplication.GetDetails(id);
            //approvalAuthority.Countries = _countryApplication.GetCountrys();
            return Partial("Edit", approvalAuthority);
        }

        public JsonResult OnPostEdit(EditApprovalAuthority command)
        {
            var result = _approvalAuthorityApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
