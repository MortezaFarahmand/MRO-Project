using _0_Framework.Domain;

namespace PartManagement.Domain.PartAgg.Events
{
    public class PartHierarchyChangedEvent : DomainEventBase
    {
        public long PartId { get; }

        public PartHierarchyChangedEvent(long partId)
        {
            PartId = partId;
        }
    }
}
