using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using RolesHelper = _0_Framework.Infrastructure.Roles;

namespace ServiceHost.Areas.Administration.Pages.Account.Roles
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreateRole Command { get; set; } = new();

        public SelectList ScopesList { get; set; }

        private readonly IRoleApplication _roleApplication;
        private readonly IAuthHelper _authHelper;
        public CreateModel(IRoleApplication roleApplication, IAuthHelper authHelper)
        {
            _roleApplication = roleApplication;
            _authHelper = authHelper;
        }


        public void OnGet()
        {
            // سقف دسترسی کاربر جاری
            var maxScope = _authHelper.MaxRoleScope;

            // فقط سطوح مجاز (هم‌سطح یا پایین‌تر)
            var allowedScopes = RolesHelper.GetAllowedScopes(maxScope).ToList();

            ScopesList = new SelectList(
                allowedScopes.Select(s => new
                {
                    Value = (long)s,
                    Text = s.ToString()
                }),
                "Value",
                "Text"
            );

            // مقدار پیش‌فرض (اختیاری)
            // Command.Scope = RoleScope.Site;
        }

        //public IActionResult OnGetCreate()
        //{
        //    var command = new CreateRole();
        //    return Partial("./Create", command);
        //}

        public IActionResult OnPost(CreateRole command)
        {
            var result = _roleApplication.Create(command);
            return RedirectToPage("Index");
        }
    }
}
