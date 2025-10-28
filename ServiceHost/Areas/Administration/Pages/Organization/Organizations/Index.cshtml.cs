using System.Collections.Generic;
using BasicDataManagement.Application.Contracts.Entiti;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrganizationManagement.Application.Contracts.Country;
using OrganizationManagement.Application.Contracts.Organization;

namespace ServiceHost.Areas.Administration.Pages.Organization.Organizations
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public OrganizationSearchModel SearchModel;
        public List<OrganizationViewModel> Organizations;
        public SelectList OrganizationGroups;
        public SelectList Entitis;

        private readonly IOrganizationApplication _organizationApplication;
        private readonly IOrganizationGroupApplication _organizationGroupApplication;
        private readonly IEntitiApplication _entitiApplication;

        public IndexModel(IOrganizationApplication organizationApplication, IOrganizationGroupApplication organizationGroupApplication, IEntitiApplication entitiApplication)
        {
            _organizationApplication = organizationApplication;
            _organizationGroupApplication = organizationGroupApplication;
            _entitiApplication = entitiApplication;
        }



        public void OnGet(OrganizationSearchModel searchModel)
        {
            OrganizationGroups = new SelectList(_organizationGroupApplication.GetOrganizationGroups(), "Id", "Name");
            Entitis = new SelectList(_entitiApplication.GetEntitis(), "Id", "Name");
            Organizations = _organizationApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateOrganization();
            {
                command.Groups = _organizationGroupApplication.GetOrganizationGroups();
                command.Entitis = _entitiApplication.GetEntitis();
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
            organization.Groups = _organizationGroupApplication.GetOrganizationGroups();
            organization.Entitis = _entitiApplication.GetEntitis();
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
