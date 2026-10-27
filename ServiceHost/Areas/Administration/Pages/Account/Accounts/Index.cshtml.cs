using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrganizationManagement.Application.Contracts.Organization;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ServiceHost.Areas.Administration.Pages.Account.Accounts
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public AccountSearchModel SearchModel { get; set; } = new AccountSearchModel();
        public long? currentOrganizationId { get; set; }
        //public bool IsAdmin => currentOrganizationId == 0;

        public List<AccountViewModel> Accounts { get; set; }
        //public SelectList Roles { get; set; }
        public SelectList Organizations { get; set; }

        private readonly IRoleApplication _roleApplication;
        private readonly IAccountApplication _accountApplication;
        private readonly IOrganizationApplication _organizationApplication;
        private readonly IAuthHelper _authHelper;

        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication
            , IOrganizationApplication organizationApplication, IAuthHelper authHelper)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
            _organizationApplication = organizationApplication;
            _authHelper = authHelper;
        }


        public void OnGet()
        {
            Organizations = new SelectList(_organizationApplication.GetOrganizations(), "Id", "NameEn");
            Accounts = _accountApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new RegisterAccount();

            command.isSysAdmin = _authHelper.CurrentAccountIsSystemAdminOrNull();
            command.isOrgAdmin = _authHelper.CurrentAccountIsOrganizationAdmin();
            // اگر System Admin نیست، OrganizationId را روی سازمان خودش قفل کن
            if (command.isSysAdmin != true)
            {
                command.OrganizationId = _authHelper.CurrentAccountOrganizationId();
            }
            command.Roles = _roleApplication.GetRoles(); // مهم: List باید PermissionCodeNames رو پر کرده باشه
            command.Organizations = _organizationApplication.GetOrganizations();
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(RegisterAccount command)
        {
            var result = _accountApplication.Register(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var account = _accountApplication.GetDetails(id);
            account.isSysAdmin = _authHelper.CurrentAccountIsSystemAdminOrNull();
            account.isOrgAdmin = _authHelper.CurrentAccountIsOrganizationAdmin();

            //// اگر System Admin نیست، OrganizationId را روی سازمان خودش قفل کن
            //if (account.isSysAdmin != true)
            //{
            //    account.OrganizationId = _authHelper.CurrentAccountOrganizationId();
            //}
            account.Roles = _roleApplication.GetRoles();
            account.Organizations = _organizationApplication.GetOrganizations();
            return Partial("Edit", account);
        }

        public JsonResult OnPostEdit(EditAccount command)
        {
            var result = _accountApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetChangePassword(long id)
        {
            var command = new ChangePassword { Id = id };
            return Partial("ChangePassword", command);
        }

        public JsonResult OnPostChangePassword(ChangePassword command)
        {
            var result = _accountApplication.ChangePassword(command);
            return new JsonResult(result);
        }

        //public IActionResult OnGetNotActive(long id)
        //{
        //    var result =  _organizationApplication.NotActive(id); 
        //    if(result.IsSucceeded)
        //        return RedirectToPage("./Index");
        //    Message = result.Message;
        //    return RedirectToPage("./Index");
        //}


    }
}
