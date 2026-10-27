using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrganizationManagement.Application.Contracts.Organization;
using PartManagement.Application.Contracts.Stock.StockType;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Part.Stock.StockType
{
    public class IndexModel : PageModel
    {
        public StockTypeViewModel ViewModel;

        [BindProperty(SupportsGet = true)]
        public StockTypeSearchModel SearchModel { get; set; }

        public long? currentOrganizationId { get; set; }
        //public bool IsAdmin;


        public List<StockTypeViewModel> StockTypes;
        public SelectList Organizations { get; set; }


        private readonly IStockTypeApplication _stockTypeApplication;
        private readonly IAuthHelper _authHelper;
        private readonly IOrganizationApplication _organizationApplication;

        public IndexModel(IStockTypeApplication stockTypeApplication, IAuthHelper authHelper
            , IOrganizationApplication organizationApplication)
        {
            _stockTypeApplication = stockTypeApplication;
            _authHelper = authHelper;
            _organizationApplication = organizationApplication;
        }


        //[NeedsPermission(PartPermissions.SearchStockType)]
        public void OnGet()
        {
            Organizations = new SelectList(_organizationApplication.GetOrganizations(), "Id", "NameEn");
            StockTypes = _stockTypeApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            var stockType = new CreateStockType(); 
            currentOrganizationId = _authHelper.CurrentAccountOrganizationId();
            // ۳. اگه کاربر عادیه، لیست رو قفل کن روی سازمان خودش
            stockType.OrganizationId = _authHelper.SetOrganizationId(currentOrganizationId);
            stockType.IsAdmin = _authHelper.CurrentAccountIsAdmin();
            FillOrganizations(stockType);
            return Partial("./Create", stockType);
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
            if (stockType == null) return NotFound();
            currentOrganizationId = _authHelper.CurrentAccountOrganizationId();
            //  اگه کاربر عادیه، لیست رو قفل کن روی سازمان خودش
            // البته فقط برای کاربر غیرادمین، سازمان را به سازمان کاربر محدود می‌کنیم
            if (!_authHelper.CurrentAccountIsAdmin())
            {
                stockType.OrganizationId = _authHelper.CurrentAccountOrganizationId().GetValueOrDefault();
            }
            stockType.IsAdmin = _authHelper.CurrentAccountIsAdmin();
            FillOrganizations(stockType);
            return Partial("Edit", stockType);

        }

        //[NeedsPermission(PartPermissions.EditStockType)]
        public JsonResult OnPostEdit(EditStockType command)
        {
            var result = _stockTypeApplication.Edit(command);
            return new JsonResult(result);
        }

        private void FillOrganizations(CreateStockType model)
        {
            model.Organizations = _organizationApplication.GetOrganizations()
                .Select(o => new SelectListItem(o.NameEn, o.Id.ToString()))
                .ToList();
        }
    }
}
