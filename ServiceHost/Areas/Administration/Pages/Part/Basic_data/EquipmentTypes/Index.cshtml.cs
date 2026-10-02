using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PartManagement.Application.Contracts.Basic_data.EquipmentType;
using PartManagement.Configuration.Permissions;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Part.Basic_data.EquipmentType
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; }

        public EquipmentTypeViewModel ViewModel;
        public EquipmentTypeSearchModel SearchModel;
        public List<EquipmentTypeViewModel> EquipmentTypes;

        private readonly IEquipmentTypeApplication _equipmentTypeApplication;

        public IndexModel(IEquipmentTypeApplication equipmentTypeApplication)
        {
            _equipmentTypeApplication = equipmentTypeApplication;
        }


        //[NeedsPermission(PartPermissions.SearchEquipmentType)]
        public void OnGet(EquipmentTypeSearchModel searchModel)
        {
            EquipmentTypes = _equipmentTypeApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateEquipmentType());
        }

        //[NeedsPermission(PartPermissions.CreateEquipmentType)]
        public JsonResult OnPostCreate(CreateEquipmentType command)
        {
            var result = _equipmentTypeApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var equipmentType = _equipmentTypeApplication.GetDetails(id);
            return Partial("Edit", equipmentType);
        }

        //[NeedsPermission(PartPermissions.EditEquipmentType)]
        public JsonResult OnPostEdit(EditEquipmentType command)
        {
            var result = _equipmentTypeApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetActive(long id)
        {
            var result = _equipmentTypeApplication.Activate(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetDeactive(long id)
        {
            var result = _equipmentTypeApplication.Deactivate(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
