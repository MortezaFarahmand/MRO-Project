using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrganizationManagement.Application.Contracts.Organization;
using OrganizationManagement.Application.Contracts.OrganizationDepartment;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Organization.OrganizationDepartments
{
    public class IndexModel : PageModel
    {
        public OrganizationDepartmentSearchModel SearchModel;
        public List<OrganizationDepartmentViewModel> OrganizationDepartments;
        public SelectList OrganizationDepartmentss;
        public SelectList Organizations;

        private readonly IOrganizationDepartmentApplication _organizationDepartmentApplication;
        private readonly IOrganizationApplication _organizationApplication;

        public IndexModel(IOrganizationDepartmentApplication organizationDepartmentApplication, IOrganizationApplication organizationApplication)
        {
            _organizationDepartmentApplication = organizationDepartmentApplication;
            _organizationApplication = organizationApplication;
        }



        public void OnGet(OrganizationDepartmentSearchModel searchModel)
        {
            OrganizationDepartmentss = new SelectList(_organizationDepartmentApplication.GetOrganizationDepartments(), "Id", "Name");
            Organizations = new SelectList(_organizationApplication.GetOrganizations(), "Id", "NameEn");
            OrganizationDepartments = _organizationDepartmentApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateOrganizationDepartment();
            {
                command.OrganizationDepartments = _organizationDepartmentApplication.GetOrganizationDepartments();
                command.Organizations = _organizationApplication.GetOrganizations();
            }
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateOrganizationDepartment command)
        {
            var result = _organizationDepartmentApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var organizationDepartment = _organizationDepartmentApplication.GetDetails(id);
            organizationDepartment.OrganizationDepartments = _organizationDepartmentApplication.GetOrganizationDepartments();
            organizationDepartment.Organizations = _organizationApplication.GetOrganizations();

            return Partial("Edit", organizationDepartment);
        }

        public JsonResult OnPostEdit(EditOrganizationDepartment command)
        {
            var result = _organizationDepartmentApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
