using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonnelManagement.Application.Contracts.PersonGroup;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Personnel.PersonGroup
{
    public class IndexModel : PageModel
    {
        public List<PersonGroupViewModel> PersonGroups;

        private readonly IPersonGroupApplication _personGroupApplication;

        public IndexModel(IPersonGroupApplication personGroupApplication)
        {
            this._personGroupApplication = personGroupApplication;
        }


        public void OnGet()
        {
            PersonGroups = _personGroupApplication.GetPersonGroups();
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreatePersonGroup());
        }

        public JsonResult OnPostCreate(CreatePersonGroup command) 
        { 
            var result = _personGroupApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var personGroup = _personGroupApplication.GetDetails(id);
            return Partial("Edit", personGroup);
        }

        public JsonResult OnPostEdit(EditPersonGroup command)
        {
            var result = _personGroupApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
