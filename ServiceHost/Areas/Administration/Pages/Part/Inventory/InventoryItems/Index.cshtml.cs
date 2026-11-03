using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PartManagement.Application.Contracts.Inventory.InventoryItem;
using PartManagement.Application.Contracts.Inventory.InventoryItem.Commands;
using PartManagement.Application.Contracts.Inventory.InventoryItem.DTOs;
using PartManagement.Application.Contracts.Inventory.InventoryItem.Queries;
using PartManagement.Application.Contracts.Part;
using PartManagement.Application.Contracts.Part.Queries;
using PartManagement.Application.Contracts.Stock.Store;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Part.Inventory.InventoryItems
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public SearchInventoryQuery SearchModel { get; set; } = new();

        public List<InventoryItemDto> InventoryItems { get; set; } = new();

        private readonly IInventoryItemApplication _application;
        private readonly IPartApplication _partApplication;
        private readonly IStoreApplication _storeApplication;

        public IndexModel(IInventoryItemApplication application, IPartApplication partApplication, 
            IStoreApplication storeApplication)
        {
            _application = application;
            _partApplication = partApplication;
            _storeApplication = storeApplication;
        }

        public void OnGet()
        {
            InventoryItems = _application.Search(SearchModel) ?? new();
        }

        public IActionResult OnGetCreateSerialized()
        {
            var command = new CreateSerializedInventoryCommand
            {
                Parts = _partApplication.Search(new SearchPartsQuery())
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.PartNumber} - {x.Description}"
                }).ToList(),

                Stores = _storeApplication.Search(new StoreSearchModel())
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.Code} - {x.Name}"
                }).ToList()
            };

            return Partial("./CreateSerialized", command);
        }

        public JsonResult OnPostCreateSerialized(CreateSerializedInventoryCommand command)
        {
            return new JsonResult(_application.CreateSerialized(command));
        }

        public IActionResult OnGetCreateBatch()
        {
            var command = new CreateBatchInventoryCommand
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
            return Partial("./CreateBatch", command);
        }

        public JsonResult OnPostCreateBatch(CreateBatchInventoryCommand command)
        {
            return new JsonResult(_application.CreateBatch(command));
        }

        public IActionResult OnGetAllocate(long id)
        {
            var result = _application.Allocate(id);
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetReturnToStock(long id, long storeId)
        {
            var result = _application.ReturnToStock(new ReturnToStockCommand { Id = id, StoreId = storeId });
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}