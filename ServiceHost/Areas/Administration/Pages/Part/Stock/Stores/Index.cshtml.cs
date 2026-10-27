using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BasicDataManagement.Application.Contracts.Entiti;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrganizationManagement.Application.Contracts.Country;
using OrganizationManagement.Application.Contracts.Organization;
using OrganizationManagement.Configuration.Permissions;
using PartManagement.Application.Contracts.Stock.StockType;
using PartManagement.Application.Contracts.Stock.Store;
using PartManagement.Configuration.Permissions;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Part.Stock.Stores
{
    //[Authorize(Roles = "1, 3")]
    public class IndexModel : PageModel
    {

        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public StoreSearchModel SearchModel { get; set; } = new();

        public List<StoreViewModel> Stores;
        public SelectList StockTypes { get; set; }
        public List<StockTypeViewModel> StockTypess { get; set; } = new();
        public SelectList Organizations;
        public long? currentOrganizationId { get; set; }
        //public bool IsAdmin => currentOrganizationId == 0;

        private readonly IStoreApplication _storeApplication;
        private readonly IStockTypeApplication _stockTypeApplication;
        private readonly IOrganizationApplication _organizationApplication;
        private readonly IAuthHelper _authHelper;

        public IndexModel(IStoreApplication storeApplication, IStockTypeApplication stockTypeApplication
            , IOrganizationApplication organizationApplication, IAuthHelper authHelper)
        {
            _storeApplication = storeApplication;
            _stockTypeApplication = stockTypeApplication;
            _organizationApplication = organizationApplication;
            _authHelper = authHelper;
        }


        //[NeedsPermission(PartPermissions.ListStore)]
        public void OnGet()
        {
            Organizations = new SelectList(_organizationApplication.GetOrganizations(), "Id", "NameEn");
            StockTypes = new SelectList(_stockTypeApplication.GetStockTypesBy(SearchModel.OrganizationId), "Id", "Name");
            Stores = _storeApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            //برای کاربر عادی، سازمان از حساب کاربری تعیین می‌شود. برای ادمین، سازمان باید
            // در فرم انتخاب شود؛ بنابراین تا زمان انتخاب سازمان، فهرست StockTypeها خالی می‌ماند.
            var command = new CreateStore
            {
                OrganizationId = _authHelper.CurrentAccountOrganizationId(),
                Organizations = _organizationApplication.GetOrganizations()
                                ?? new List<OrganizationViewModel>(),
                IsAdmin = _authHelper.CurrentAccountIsAdmin(),
                StockTypeIds = new List<long>(),
                StockTypes = new List<StockTypeViewModel>(),
            };

            if (command.OrganizationId.HasValue)
            {
                command.StockTypes =
                    _stockTypeApplication.GetStockTypesBy(command.OrganizationId)
                    ?? new List<StockTypeViewModel>();
            }

            command.StockTypeIds = new List<long>();
            command.Organizations ??= new List<OrganizationViewModel>();

            
            return Partial("./Create", command);
        }

        //[NeedsPermission(PartPermissions.CreateStore)]
        public JsonResult OnPostCreate(CreateStore command)
        {
            var result = _storeApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            long? organizationId = _authHelper.CurrentAccountOrganizationId();
            var store = _storeApplication.GetDetails(id);
            store.StockTypes = _stockTypeApplication.GetStockTypesBy(organizationId);
            store.Organizations = _organizationApplication.GetOrganizations();
            return Partial("Edit", store);
        }

        //[NeedsPermission(PartPermissions.EditStore)] 
        public JsonResult OnPostEdit(EditStore command)
        {
            var result = _storeApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetStockTypes(long organizationId)
        {
            var isAdmin = _authHelper.CurrentAccountIsAdmin();
            var accountOrganizationId = _authHelper.CurrentAccountOrganizationId();

            // امنیت سمت سرور: کاربر عادی حق درخواست نوع‌های سازمان دیگر را ندارد.
            if (!isAdmin && organizationId != accountOrganizationId)
                return Forbid();

            var stockTypes = _stockTypeApplication.GetStockTypesBy(organizationId);

            return new JsonResult(stockTypes.Select(x => new StockTypeViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code
            }));
        }

        public IActionResult OnGetDeactive(long id)
        {
            var result = _storeApplication.Deactivate(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetIsActive(long id)
        {
            var result = _storeApplication.Activate(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");
            Message = result.Message;
            return RedirectToPage("./Index");
        }


    }
}
