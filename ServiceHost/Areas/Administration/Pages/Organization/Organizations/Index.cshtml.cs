using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrganizationManagement.Application.Contracts.Organization;
using OrganizationManagement.Application.Contracts.OrganizationGroup;

namespace ServiceHost.Areas.Administration.Pages.Organization.Organizations
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public OrganizationSearchModel SearchModel;
        public List<OrganizationViewModel> Organizations;
        public SelectList OrganizationGroups;

        private readonly IOrganizationApplication _organizationApplication;
        private readonly IOrganizationGroupApplication _organizationGroupApplication;

        public IndexModel(IOrganizationApplication organizationApplication, IOrganizationGroupApplication organizationGroupApplication)
        {
            _organizationApplication = organizationApplication;
            _organizationGroupApplication = organizationGroupApplication;
        }



        public void OnGet(OrganizationSearchModel searchModel)
        {
            OrganizationGroups = new SelectList(_organizationGroupApplication.GetOrganizationGroups(), "Id", "Name");
            Organizations = _organizationApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateOrganization();
            {
                command.Groups = _organizationGroupApplication.GetOrganizationGroups();
            }
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateOrganization command)
        {
            var result = _organizationApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var organization = _organizationApplication.GetDetails(id);
            organization.Groups= _organizationGroupApplication.GetOrganizationGroups(); 
            return Partial("Edit", organization);
        }

        public JsonResult OnPostEdit(EditOrganization command)
        {
            var result = _organizationApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetNotActive(long id)
        {
            var result =  _organizationApplication.NotActive(id); 
            if(result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetIsActive(long id)
        {
            var result = _organizationApplication.Active(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
