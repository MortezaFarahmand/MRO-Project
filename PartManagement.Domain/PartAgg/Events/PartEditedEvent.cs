using _0_Framework.Domain;

namespace PartManagement.Domain.PartAgg.Events
{
    public class PartEditedEvent : DomainEventBase
    {
        public long PartId { get; }
        public string PartNumber { get; }

        public PartEditedEvent(long partId, string partNumber)
        {
            PartId = partId;
            PartNumber = partNumber;
        }
    }
}
