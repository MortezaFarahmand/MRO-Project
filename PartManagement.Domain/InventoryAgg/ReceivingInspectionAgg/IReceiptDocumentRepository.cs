using _0_Framework.Domain;

namespace PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg
{
    public interface IReceiptDocumentRepository : IRepository<long, ReceiptDocument>
    {
        List<ReceiptDocument> GetByReceipt(long receiptId);
    }
}