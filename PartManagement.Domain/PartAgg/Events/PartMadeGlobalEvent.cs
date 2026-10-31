using _0_Framework.Domain;

namespace PartManagement.Domain.PartAgg.Events
{
    public class PartMadeGlobalEvent : DomainEventBase
    {
        public long PartId { get; }
        public string PartNumber { get; }

        public PartMadeGlobalEvent(long partId, string partNumber)
        {
            PartId = partId;
            PartNumber = partNumber;
        }
    }
}
