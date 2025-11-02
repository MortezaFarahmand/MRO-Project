using BasicDataManagement.Application.Contracts.Entiti;
using BasicDataManagement.Application.Contracts.PictureCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrganizationManagement.Domain.OrganizationAgg;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.BasicData.PictureCategories
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; }
        public PictureCategorySearchModel SearchModel;
        public List<PictureCategoryViewModel> PictureCategorys;
        public SelectList Entitis;

        private readonly IPictureCategoryApplication _pictureCategoryApplication;
        private readonly IEntitiApplication _entitiApplication;

        public IndexModel(IPictureCategoryApplication pictureCategoryApplication, IEntitiApplication entitiApplication)
        {
            _pictureCategoryApplication = pictureCategoryApplication;
            _entitiApplication = entitiApplication;
        }



        public void OnGet(PictureCategorySearchModel searchModel)
        {
            Entitis = new SelectList(_entitiApplication.GetEntitis(), "Id", "Name");
            PictureCategorys = _pictureCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreatePictureCategory();
            {
                command.Entities = _entitiApplication.GetEntitis();
            }
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreatePictureCategory command)
        {
            var result = _pictureCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var pictureCategory = _pictureCategoryApplication.GetDetails(id);
            pictureCategory.Entities = _entitiApplication.GetEntitis();
            return Partial("Edit", pictureCategory);
        }

        public JsonResult OnPostEdit(EditPictureCategory command)
        {
            var result = _pictureCategoryApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetDeActive(long id)
        {
            var result = _pictureCategoryApplication.DeActive(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetActive(long id)
        {
            var result = _pictureCategoryApplication.Active(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }


    }
}
