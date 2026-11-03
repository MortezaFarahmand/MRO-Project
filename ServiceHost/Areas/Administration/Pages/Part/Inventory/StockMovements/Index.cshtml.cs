using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PartManagement.Application.Contracts.Inventory.StockMovement;
using PartManagement.Application.Contracts.Inventory.StockMovement.Commands;
using PartManagement.Application.Contracts.Inventory.StockMovement.DTOs;
using PartManagement.Application.Contracts.Inventory.StockMovement.Queries;
using PartManagement.Application.Contracts.Part;
using PartManagement.Application.Contracts.Part.Queries;
using PartManagement.Application.Contracts.Stock.Store;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Part.Inventory.StockMovements
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public SearchStockMovementQuery SearchModel { get; set; } = new();

        public List<StockMovementDto> Movements { get; set; } = new();

        private readonly IStockMovementApplication _application;
        private readonly IPartApplication _partApplication;
        private readonly IStoreApplication _storeApplication;

        public IndexModel(IStockMovementApplication application, 
            IPartApplication partApplication, IStoreApplication storeApplication)
        {
            _application = application;
            _partApplication = partApplication;
            _storeApplication = storeApplication;
        }

        public void OnGet()
        {
            Movements = _application.Search(SearchModel) ?? new();
        }

        public IActionResult OnGetCreateReceipt()
        {
            var command = new CreateReceiptCommand
            {
                Parts = _partApplication.Search(new SearchPartsQuery()).Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.PartNumber} - {x.Description}"
                }).ToList(),

                Stores = _storeApplication.Search(new StoreSearchModel()).Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.Code} - {x.Name}"
                }).ToList()
            };
            return Partial("./CreateReceipt", command);
        }

        public JsonResult OnPostCreateReceipt(CreateReceiptCommand command)
            => new JsonResult(_application.CreateReceipt(command));

        public IActionResult OnGetCreateIssue()
        {
            var command = new CreateIssueCommand
            {
                Parts = GetParts(),
                Stores = GetStores()
            };
            return Partial("./CreateIssue", command);
        }

        public JsonResult OnPostCreateIssue(CreateIssueCommand command)
            => new JsonResult(_application.CreateIssue(command));

        public IActionResult OnGetCreateTransfer()
        {
            var command = new CreateTransferCommand
            {
                Parts = GetParts(),
                Stores = GetStores()
            };
            return Partial("./CreateTransfer", command);
        }

        public JsonResult OnPostCreateTransfer(CreateTransferCommand command)
            => new JsonResult(_application.CreateTransfer(command));



        private List<SelectListItem> GetParts()
        {
            return _partApplication.Search(new SearchPartsQuery())
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.PartNumber} - {x.Description}"
                }).ToList();
        }

        private List<SelectListItem> GetStores()
        {
            return _storeApplication.Search(new StoreSearchModel())
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.Code} - {x.Name}"
                }).ToList();
        }
    }
}