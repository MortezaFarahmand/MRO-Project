using Microsoft.AspNetCore.Mvc.Rendering;

namespace PartManagement.Application.Contracts.Inventory.InventoryItem.Commands
{
    public class CreateSerializedInventoryCommand
    {
        public long PartId { get; set; }
        public string SerialNumber { get; set; }
        public long? StoreId { get; set; }
        public long? OwnerOrganizationId { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string Note { get; set; }

        // برای UI
        public List<SelectListItem> Parts { get; set; }
        public List<SelectListItem> Stores { get; set; }
    }
}
