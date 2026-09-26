using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PartManagement.Application.Contracts.Stock.StockType;
using PartManagement.Configuration.Permissions;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Part.Stock.StockType
{
    public class IndexModel : PageModel
    {
        public StockTypeViewModel ViewModel;
        public StockTypeSearchModel SearchModel;
        public List<StockTypeViewModel> StockTypes;

        private readonly IStockTypeApplication _stockTypeApplication;

        public IndexModel(IStockTypeApplication stockTypeApplication)
        {
            _stockTypeApplication = stockTypeApplication;
        }


        //[NeedsPermission(PartPermissions.SearchStockType)]
        public void OnGet(StockTypeViewModel viewModel)
        {
            StockTypes = _stockTypeApplication.GetStockTypes(viewModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateStockType());
        }

        //[NeedsPermission(PartPermissions.CreateStockType)]
        public JsonResult OnPostCreate(CreateStockType command)
        {
            var result = _stockTypeApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var stockType = _stockTypeApplication.GetDetails(id);
            return Partial("Edit", stockType);
        }

        //[NeedsPermission(PartPermissions.EditStockType)]
        public JsonResult OnPostEdit(EditStockType command)
        {
            var result = _stockTypeApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
