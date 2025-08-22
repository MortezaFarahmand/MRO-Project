using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrganizationManagement.Application.Contracts.OrganizationGroup;

namespace ServiceHost.Areas.Administration.Pages.Organization.OrganizationGroups
{
    public class IndexModel : PageModel
    {
        public OrganizationGroupSearchModel SearchModel;
        public List<OrganizationGroupViewModel> OrganizationGroups;
        private readonly IOrganizationGroupApplication _organizationGroupApplication;

        public IndexModel(IOrganizationGroupApplication organizationGroupApplication)
        {
            _organizationGroupApplication = organizationGroupApplication;
        }



        public void OnGet(OrganizationGroupSearchModel searchModel)
        {
            OrganizationGroups = _organizationGroupApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateOrganizationGroup());
        }

        public JsonResult OnPostCreate(CreateOrganizationGroup command)
        {
            var result = _organizationGroupApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var organizationGroup = _organizationGroupApplication.GetDetails(id);
            return Partial("Edit", organizationGroup);
        }

        public JsonResult OnPostEdit(EditOrganizationGroup command)
        {
            var result = _organizationGroupApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
