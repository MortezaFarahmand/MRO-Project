using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrganizationManagement.Application.Contracts.Organization;
using OrganizationManagement.Application.Contracts.OrganizationDepartment;
using OrganizationManagement.Application.Contracts.OrganizationPosition;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Organization.OrganizationPositions
{
    public class IndexModel : PageModel
    {
        public OrganizationPositionSearchModel SearchModel;
        public List<OrganizationPositionViewModel> OrganizationPositions;
        public SelectList OrganizationPositionss;
        public SelectList Organizations;
        public SelectList OrganizationDepartments;

        private readonly IOrganizationPositionApplication _organizationPositionApplication;
        private readonly IOrganizationApplication _organizationApplication;
        private readonly IOrganizationDepartmentApplication _organizationDepartmentApplication;

        public IndexModel(IOrganizationPositionApplication organizationPositionApplication, 
            IOrganizationApplication organizationApplication,IOrganizationDepartmentApplication organizationDepartmentApplication)
        {
            _organizationPositionApplication = organizationPositionApplication;
            _organizationApplication = organizationApplication;
            _organizationDepartmentApplication = organizationDepartmentApplication; 
        }



        public void OnGet(OrganizationPositionSearchModel searchModel, long id)
        {
            OrganizationPositionss = new SelectList(_organizationPositionApplication.GetOrganizationPositions(), "Id", "Name");
            Organizations = new SelectList(_organizationApplication.GetOrganizations(), "Id", "NameEn");
            OrganizationDepartments = new SelectList(_organizationDepartmentApplication.GetDepartmentsByOrganizationId(id), "Id", "Name");
            OrganizationPositions = _organizationPositionApplication.Search(searchModel);
        }

        public JsonResult GetOrganizations()
        {
            Organizations = new SelectList(_organizationApplication.GetOrganizations());
            return new JsonResult(Organizations);
        }

        public IActionResult GetDepartmentByOrganizationIds(long id)
        {
            OrganizationDepartments = new SelectList(_organizationDepartmentApplication.GetDepartmentsByOrganizationId(id));
            return new JsonResult(OrganizationDepartments);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateOrganizationPosition();
            {
                command.OrganizationPositions = _organizationPositionApplication.GetOrganizationPositions();
                command.Organizations = _organizationApplication.GetOrganizations();
                command.OrganizationDepartments = _organizationDepartmentApplication.GetOrganizationDepartments();
            }
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateOrganizationPosition command)
        {
            var result = _organizationPositionApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var organizationPosition = _organizationPositionApplication.GetDetails(id);
            organizationPosition.OrganizationPositions = _organizationPositionApplication.GetOrganizationPositions();
            organizationPosition.Organizations = _organizationApplication.GetOrganizations();
            organizationPosition.OrganizationDepartments = _organizationDepartmentApplication.GetOrganizationDepartments();

            return Partial("Edit", organizationPosition);
        }

        public JsonResult OnPostEdit(EditOrganizationPosition command)
        {
            var result = _organizationPositionApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
