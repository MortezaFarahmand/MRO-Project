using BasicDataManagement.Application.Contracts.PictureCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonnelManagement.Application.Contracts.Person;
using PersonnelManagement.Application.Contracts.PersonPicture;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Personnel.PersonPictures
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; }
        public PersonPictureSearchModel SearchModel;
        public List<PersonPictureViewModel> PersonPictures;
        public SelectList Persons;
        public SelectList PictureCategories;


        private readonly IPersonPictureApplication _personPictureApplication;
        private readonly IPersonApplication _personApplication;
        private readonly IPictureCategoryApplication _pictureCategoryApplicaton;

        public IndexModel(IPersonApplication personApplication, 
            IPersonPictureApplication personPictureApplicaton, 
            IPictureCategoryApplication pictureCategoryApplication)
        {
            _personApplication = personApplication;
            _personPictureApplication = personPictureApplicaton;
            _pictureCategoryApplicaton = pictureCategoryApplication;
        }



        public void OnGet(PersonPictureSearchModel searchModel)
        {
            Persons = new SelectList(_personApplication.GetPerson(), "Id", "Name");
            PictureCategories = new SelectList(_pictureCategoryApplicaton.GetPictureCategorys(), "Id", "Name");
            PersonPictures = _personPictureApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreatePersonPicture();
            {
                command.Persons = _personApplication.GetPerson();
                command.PictureCategories = _pictureCategoryApplicaton.GetPictureCategorys();
            }
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreatePersonPicture command)
        {
            var result = _personPictureApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)  
        { 
            var personPictures = _personPictureApplication.GetDetails(id);
            personPictures.Persons = _personApplication.GetPerson();
            personPictures.PictureCategories = _pictureCategoryApplicaton.GetPictureCategorys();

            return Partial("Edit", personPictures);
        }

        public JsonResult OnPostEdit(EditPersonPicture command)
        {
            var result = _personPictureApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRestore(long id) 
        { 
            var result = _personPictureApplication.Restore(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;

            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _personPictureApplication.Remove(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;

            return RedirectToPage("./Index");
        }
    }
}
