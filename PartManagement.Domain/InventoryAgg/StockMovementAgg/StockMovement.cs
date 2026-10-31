using _0_Framework.Domain;
using PartManagement.Domain.InventoryAgg.StockMovementAgg.Enums;
using PartManagement.Domain.PartAgg;

namespace PartManagement.Domain.InventoryAgg.StockMovementAgg
{
    public class StockMovement : AggregateRoot
    {
        public string MovementNumber { get; private set; }     // شماره سند (خودکار یا دستی)
        public StockMovementType MovementType { get; private set; }

        public long PartId { get; private set; }
        public long? InventoryItemId { get; private set; }     // برای Serialized / Batch
        public long? FromStoreId { get; private set; }
        public long? ToStoreId { get; private set; }

        public decimal Quantity { get; private set; }
        public string SerialNumber { get; private set; }
        public string BatchNumber { get; private set; }

        public long? RelatedDocumentId { get; private set; }   // مثلاً WorkOrderId یا PurchaseOrderId
        public string RelatedDocumentType { get; private set; } // "WorkOrder", "PurchaseOrder", ...

        public long? OrganizationId { get; private set; }
        public string Note { get; private set; }
        public DateTime MovementDate { get; private set; }

        // Navigation
        public Part Part { get; private set; }

        private StockMovement() { }

        // ==================== Factory Methods ====================

        public static StockMovement CreateReceipt(
            string movementNumber,
            long partId,
            long toStoreId,
            decimal quantity,
            string serialNumber,
            string batchNumber,
            long? inventoryItemId,
            long? organizationId,
            long? relatedDocumentId,
            string relatedDocumentType,
            string note,
            long createdBy)
        {
            ValidateQuantity(quantity);

            return new StockMovement
            {
                MovementNumber = movementNumber,
                MovementType = StockMovementType.Receipt,
                PartId = partId,
                ToStoreId = toStoreId,
                Quantity = quantity,
                SerialNumber = serialNumber?.Trim().ToUpperInvariant(),
                BatchNumber = batchNumber?.Trim().ToUpperInvariant(),
                InventoryItemId = inventoryItemId,
                OrganizationId = organizationId,
                RelatedDocumentId = relatedDocumentId,
                RelatedDocumentType = relatedDocumentType,
                Note = note,
                MovementDate = DateTime.UtcNow,
                CreatedBy = createdBy
            };
        }

        public static StockMovement CreateIssue(
            string movementNumber,
            long partId,
            long fromStoreId,
            decimal quantity,
            string serialNumber,
            string batchNumber,
            long? inventoryItemId,
            long? organizationId,
            long? relatedDocumentId,
            string relatedDocumentType,
            string note,
            long createdBy)
        {
            ValidateQuantity(quantity);

            return new StockMovement
            {
                MovementNumber = movementNumber,
                MovementType = StockMovementType.Issue,
                PartId = partId,
                FromStoreId = fromStoreId,
                Quantity = quantity,
                SerialNumber = serialNumber?.Trim().ToUpperInvariant(),
                BatchNumber = batchNumber?.Trim().ToUpperInvariant(),
                InventoryItemId = inventoryItemId,
                OrganizationId = organizationId,
                RelatedDocumentId = relatedDocumentId,
                RelatedDocumentType = relatedDocumentType,
                Note = note,
                MovementDate = DateTime.UtcNow,
                CreatedBy = createdBy
            };
        }

        public static StockMovement CreateTransfer(
            string movementNumber,
            long partId,
            long fromStoreId,
            long toStoreId,
            decimal quantity,
            string serialNumber,
            string batchNumber,
            long? inventoryItemId,
            long? organizationId,
            string note,
            long createdBy)
        {
            ValidateQuantity(quantity);

            if (fromStoreId == toStoreId)
                throw new DomainException("انبار مبدأ و مقصد نمی‌توانند یکسان باشند");

            return new StockMovement
            {
                MovementNumber = movementNumber,
                MovementType = StockMovementType.Transfer,
                PartId = partId,
                FromStoreId = fromStoreId,
                ToStoreId = toStoreId,
                Quantity = quantity,
                SerialNumber = serialNumber?.Trim().ToUpperInvariant(),
                BatchNumber = batchNumber?.Trim().ToUpperInvariant(),
                InventoryItemId = inventoryItemId,
                OrganizationId = organizationId,
                Note = note,
                MovementDate = DateTime.UtcNow,
                CreatedBy = createdBy
            };
        }

        private static void ValidateQuantity(decimal quantity)
        {
            if (quantity <= 0)
                throw new DomainException("تعداد باید بزرگ‌تر از صفر باشد");
        }
    }
}
