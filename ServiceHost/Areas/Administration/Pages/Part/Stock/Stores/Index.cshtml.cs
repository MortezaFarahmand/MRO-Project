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

namespace ServiceHost.Areas.Administration.Pages.Part.Stock.Stores
{
    //[Authorize(Roles = "1, 3")]
    public class IndexModel : PageModel
    {

        [TempData]
        public string Message { get; set; }
        public StoreSearchModel SearchModel;
        public List<StoreViewModel> Stores;
        public SelectList StockTypes;
        public SelectList Organizations;
        public long? currentOrganizationId { get; set; }
        public bool IsAdmin => currentOrganizationId == 0;

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

            currentOrganizationId = _authHelper.CurrentAccountOrganizationId();
            // ۳. اگه کاربر عادیه، سرچ رو قفل کن روی سازمان خودش
            if (currentOrganizationId != 0)
            {
                SearchModel.OrganizationId = currentOrganizationId;
            }
            StockTypes = new SelectList(_stockTypeApplication.GetStockTypesBy(currentOrganizationId), "Id", "Name");
            SearchModel ??= new StoreSearchModel();
            Stores = _storeApplication.Search(SearchModel, currentOrganizationId);
            long? organizationId = _authHelper.CurrentAccountOrganizationId();
            //Stores = _storeApplication.Search(searchModel, organizationId);
        }

        public IActionResult OnGetCreate()
        {
            long? organizationId = _authHelper.CurrentAccountOrganizationId();
            var command = new CreateStore();
            {
                command.StockTypes = _stockTypeApplication.GetStockTypesBy(organizationId);
                command.Organizations = _organizationApplication.GetOrganizations();
            }
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
