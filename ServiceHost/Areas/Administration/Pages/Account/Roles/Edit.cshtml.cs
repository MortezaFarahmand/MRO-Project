using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Account.Roles
{
    public class EditModel : PageModel
    {
        public EditRole Command;
        public SelectList ScopesList { get; set; }
        public List<SelectListItem> Permissions = new List<SelectListItem>();

        private readonly IRoleApplication _roleApplication;
        private readonly IEnumerable<IPermissionExposer> _exposers;
        private readonly IAuthHelper _authHelper;

        public EditModel(IRoleApplication roleApplication,
            IAuthHelper authHelper, IEnumerable<IPermissionExposer> exposer)
        {
            _roleApplication = roleApplication;
            _exposers = exposer;
            _authHelper = authHelper;
        }


        public void OnGet(long id)
        {
            Command = _roleApplication.GetDetails(id);

            if (Command == null)
            {
                RedirectToPage("Index");
                return;
            }

            // پر کردن دراپ‌داون Scope بر اساس سطح مجاز کاربر جاری
            // AllowedScopes از سمت Application آمده است
            ScopesList = new SelectList(
                Command.AllowedScopes.Select(s => new
                {
                    Value = (long)s,
                    Text = s.ToString()
                }),
                "Value",
                "Text",
                (long)Command.Scope          // مقدار انتخاب‌شده فعلی
            );

            var permissions = new List<PermissionDto>();
            foreach (var exposer in _exposers)
            {
                var exposedPermissions = exposer.Expose();
                foreach (var (key, value) in exposedPermissions)
                {
                    var group = new SelectListGroup { Name = key };
                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString())
                        {
                            Group = group,
                            Selected = Command.MappedPermissions?.Any(x => x.Code == permission.Code) == true
                        };
                        Permissions.Add(item);
                    }
                }
            }
        }

        public IActionResult OnPost(EditRole command)
        {
            var result = _roleApplication.Edit(command);
            return RedirectToPage("Index");
        }

    }
}
