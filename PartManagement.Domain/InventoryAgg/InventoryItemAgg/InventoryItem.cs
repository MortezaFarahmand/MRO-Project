using _0_Framework.Domain;
using PartManagement.Domain.InventoryAgg.InventoryItemAgg.Enum;
using PartManagement.Domain.PartAgg;

namespace PartManagement.Domain.InventoryAgg.InventoryItemAgg
{
    public class InventoryItem : AggregateRoot
    {
        public long PartId { get; private set; }
        public long? StoreId { get; private set; }
        public long? AircraftId { get; private set; }          // اگر نصب شده باشد

        public string SerialNumber { get; private set; }       // برای Serialized
        public string BatchNumber { get; private set; }        // برای Batch
        public string LotNumber { get; private set; }

        public decimal Quantity { get; private set; }          // برای Consumable / Uncontrolled
        public InventoryStatus Status { get; private set; }

        public DateTime? ManufactureDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }      // Shelf Life
        public DateTime? InstallDate { get; private set; }

        public decimal? FlightHoursAtInstall { get; private set; }
        public int? CyclesAtInstall { get; private set; }

        public string Note { get; private set; }
        public long? OwnerOrganizationId { get; private set; }

        public Part Part { get; private set; }

        private InventoryItem() { }

        public static InventoryItem CreateSerialized(
            long partId,
            string serialNumber,
            long? storeId,
            long? ownerOrganizationId,
            DateTime? manufactureDate,
            DateTime? expireDate,
            long createdBy)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
                throw new DomainException("شماره سریال الزامی است");

            return new InventoryItem
            {
                PartId = partId,
                SerialNumber = serialNumber.Trim().ToUpperInvariant(),
                StoreId = storeId,
                OwnerOrganizationId = ownerOrganizationId,
                ManufactureDate = manufactureDate,
                ExpireDate = expireDate,
                Quantity = 1,
                Status = InventoryStatus.InStock,
                CreatedBy = createdBy
            };
        }

        public static InventoryItem CreateBatch(
            long partId,
            string batchNumber,
            decimal quantity,
            long? storeId,
            long? ownerOrganizationId,
            DateTime? manufactureDate,
            DateTime? expireDate,
            long createdBy)
        {
            if (quantity <= 0)
                throw new DomainException("تعداد باید بزرگ‌تر از صفر باشد");

            return new InventoryItem
            {
                PartId = partId,
                BatchNumber = batchNumber?.Trim().ToUpperInvariant(),
                Quantity = quantity,
                StoreId = storeId,
                OwnerOrganizationId = ownerOrganizationId,
                ManufactureDate = manufactureDate,
                ExpireDate = expireDate,
                Status = InventoryStatus.InStock,
                CreatedBy = createdBy
            };
        }

        public void Allocate()
        {
            if (Status != InventoryStatus.InStock)
                throw new DomainException("فقط قطعات موجود در انبار قابل رزرو هستند");

            Status = InventoryStatus.Allocated;
        }

        public void Install(long aircraftId, decimal? flightHours, int? cycles)
        {
            if (Status != InventoryStatus.InStock && Status != InventoryStatus.Allocated)
                throw new DomainException("وضعیت قطعه برای نصب مناسب نیست");

            AircraftId = aircraftId;
            StoreId = null;
            Status = InventoryStatus.Installed;
            InstallDate = DateTime.UtcNow;
            FlightHoursAtInstall = flightHours;
            CyclesAtInstall = cycles;
        }

        public void ReturnToStock(long storeId)
        {
            AircraftId = null;
            StoreId = storeId;
            Status = InventoryStatus.InStock;
            InstallDate = null;
        }

        public void SendForRepair()
        {
            Status = InventoryStatus.OutForRepair;
        }

        public void Quarantine(string reason)
        {
            Status = InventoryStatus.Quarantined;
            Note = reason;
        }

        public void Scrap(string reason)
        {
            Status = InventoryStatus.Scrapped;
            Note = reason;
        }
    }
}
