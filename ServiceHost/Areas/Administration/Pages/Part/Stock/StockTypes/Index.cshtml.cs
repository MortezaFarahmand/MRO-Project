using _0_Framework.Application;
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
        public bool IsAdmin => currentOrganizationId == 0;


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
            currentOrganizationId = _authHelper.CurrentAccountOrganizationId();
            // ۳. اگه کاربر عادیه، سرچ رو قفل کن روی سازمان خودش
            if (currentOrganizationId != 0)
            {
                SearchModel.OrganizationId = currentOrganizationId;
            }

            SearchModel ??= new StockTypeSearchModel();
            StockTypes = _stockTypeApplication.Search(SearchModel, currentOrganizationId);
        }

        public IActionResult OnGetCreate()
        {

            var stockType = _stockTypeApplication.GetStockTypesBy();
            currentOrganizationId = _authHelper.CurrentAccountOrganizationId();
            // ۳. اگه کاربر عادیه، سرچ رو قفل کن روی سازمان خودش
            if (currentOrganizationId != 0)
                stockType.OrganizationId = currentOrganizationId;

            stockType.IsAdmin = this.IsAdmin;
            FillOrganizations(stockType);
            return Partial("./Create", stockType);
        }

        //[NeedsPermission(PartPermissions.CreateStockType)]
        public JsonResult OnPostCreate(CreateStockType command)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new OperationResult().Failed("اطلاعات وارد شده معتبر نیست"));
            }

            currentOrganizationId = _authHelper.CurrentAccountOrganizationId();
            // ۳. اگه کاربر عادیه، سرچ رو قفل کن روی سازمان خودش
            if (currentOrganizationId != 0)
                command.OrganizationId = currentOrganizationId;

            var result = _stockTypeApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var stockType = _stockTypeApplication.GetDetails(id);
            if (stockType == null) return NotFound();

            currentOrganizationId = _authHelper.CurrentAccountOrganizationId();
            // ۳. اگه کاربر عادیه، سرچ رو قفل کن روی سازمان خودش
            if (currentOrganizationId != 0)
                stockType.OrganizationId = currentOrganizationId;

            // پرچم از همین PageModel می‌آید، نه از query string
            stockType.IsAdmin = this.IsAdmin;

            FillOrganizations(stockType);
            return Partial("Edit", stockType);

        }

        //[NeedsPermission(PartPermissions.EditStockType)]
        public JsonResult OnPostEdit(EditStockType command)
        {
            currentOrganizationId = _authHelper.CurrentAccountOrganizationId();
            // ۳. اگه کاربر عادیه، سرچ رو قفل کن روی سازمان خودش
            if (currentOrganizationId != 0)
                command.OrganizationId = currentOrganizationId;

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
