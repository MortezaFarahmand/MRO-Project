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

        private readonly IStoreApplication _storeApplication;
        private readonly IStockTypeApplication _stockTypeApplication;
        private readonly IOrganizationApplication _organizationApplication;

        public IndexModel(IStoreApplication storeApplication, IStockTypeApplication stockTypeApplication, IOrganizationApplication organizationApplication)
        {
            _storeApplication = storeApplication;
            _stockTypeApplication = stockTypeApplication;
            _organizationApplication = organizationApplication;
        }


        //[NeedsPermission(PartPermissions.ListStore)]
        public void OnGet(StoreSearchModel searchModel)
        {
            StockTypes = new SelectList(_stockTypeApplication.GetStockTypes(), "Id", "Name");
            Organizations = new SelectList(_organizationApplication.GetOrganizations(), "Id", "NameEn");
            Stores = _storeApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateStore();
            {
                command.StockTypes = _stockTypeApplication.GetStockTypes();
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
            var store = _storeApplication.GetDetails(id);
            store.StockTypes = _stockTypeApplication.GetStockTypes();
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
            var result =  _storeApplication.Deactivate(id); 
            if(result.IsSucceeded)
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
