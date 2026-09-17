using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrganizationManagement.Application.Contracts.Country;
using OrganizationManagement.Configuration.Permissions;
using System.Collections.Generic;

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


        [NeedsPermission(OrganizationPermissions.ListOrganizationGroup)]
        public void OnGet(OrganizationGroupSearchModel searchModel)
        {
            OrganizationGroups = _organizationGroupApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateOrganizationGroup());
        }

        [NeedsPermission(OrganizationPermissions.CreateOrganizationGroup)]
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

        [NeedsPermission(OrganizationPermissions.EditOrganizationGroup)]
        public JsonResult OnPostEdit(EditOrganizationGroup command)
        {
            var result = _organizationGroupApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
