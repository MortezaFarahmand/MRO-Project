using _0_Framework.Domain;

namespace PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg
{
    public interface IReceivingInspectionResultRepository : IRepository<long, ReceivingInspectionResult>
    {
        ReceivingInspectionResult GetWithChecks(long id);
        List<ReceivingInspectionResult> GetByReceipt(long receiptId);
        List<ReceivingInspectionResult> GetPendingByStore(long storeId);
    }
}