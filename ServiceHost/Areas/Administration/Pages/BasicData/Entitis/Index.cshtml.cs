using BasicDataManagement.Application;
using BasicDataManagement.Application.Contract.Entiti;
using BasicDataManagement.Domain.EntitiAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonnelManagement.Domain.PersonGroupAgg;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.BasicData.Entitis
{
    public class IndexModel : PageModel
    {
        public List<EntitiViewModel> Entities;

        private readonly IEntitiApplication _entitiApplication;

        public IndexModel(IEntitiApplication entitiApplication)
        {
            _entitiApplication = entitiApplication;
        }

        public void OnGet()
        {
            Entities = _entitiApplication.GetEntitis();
        }


        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateEntiti());
        }

        public JsonResult OnPostCreate(CreateEntiti command)
        {
            var result = _entitiApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var entiti = _entitiApplication.GetDetails(id);
            return Partial("Edit", entiti);
        }

        public JsonResult OnPostEdit(EditEntiti command)
        {
            var result = _entitiApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
