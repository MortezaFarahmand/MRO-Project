using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrganizationManagement.Application.Contracts.Organization;
using OrganizationManagement.Application.Contracts.OrganizationPicture;

namespace ServiceHost.Areas.Administration.Pages.Organization.OrganizationPictures
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public OrganizationPictureSearchModel SearchModel;
        public List<OrganizationPictureViewModel> OrganizationPictures;
        public SelectList Organizations;

        private readonly IOrganizationPictureApplication _organizationPictureApplication;
        private readonly IOrganizationApplication _organizationApplication;

        public IndexModel(IOrganizationPictureApplication organizationPictureApplication, IOrganizationApplication organizationApplication)
        {
            _organizationPictureApplication = organizationPictureApplication;
            _organizationApplication = organizationApplication;
        }



        public void OnGet(OrganizationPictureSearchModel searchModel)
        {
            Organizations = new SelectList(_organizationApplication.GetOrganizations(), "Id", "NameEn");
            OrganizationPictures = _organizationPictureApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateOrganizationPicture
            {
                Organizations = _organizationApplication.GetOrganizations()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateOrganizationPicture command)
        {
            var result = _organizationPictureApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var organizationPicture = _organizationPictureApplication.GetDetails(id);
            organizationPicture.Organizations = _organizationApplication.GetOrganizations(); 
            return Partial("Edit", organizationPicture);
        }

        public JsonResult OnPostEdit(EditOrganizationPicture command)
        {
            var result = _organizationPictureApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _organizationPictureApplication.Remove(id); 
            if(result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _organizationPictureApplication.Restore(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
