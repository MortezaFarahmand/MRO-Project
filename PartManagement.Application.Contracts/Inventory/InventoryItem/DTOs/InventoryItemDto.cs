using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartManagement.Application.Contracts.Inventory.InventoryItem.DTOs
{
    public class InventoryItemDto
    {
        public long Id { get; set; }
        public long PartId { get; set; }
        public string PartNumber { get; set; }
        public long? StoreId { get; set; }
        public long? AircraftId { get; set; }
        public string SerialNumber { get; set; }
        public string BatchNumber { get; set; }
        public decimal Quantity { get; set; }
        public string Status { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? InstallDate { get; set; }
        public string Note { get; set; }
    }
}
