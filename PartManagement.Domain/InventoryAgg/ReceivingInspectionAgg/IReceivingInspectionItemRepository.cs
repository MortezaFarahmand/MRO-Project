using _0_Framework.Domain;

namespace PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg
{
    public interface IReceivingInspectionItemRepository : IRepository<long, ReceivingInspectionItem>
    {
        List<ReceivingInspectionItem> GetActiveOrdered(long? stockTypeId = null, string keyword = null);
    }
}