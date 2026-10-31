using _0_Framework.Domain;

namespace PartManagement.Domain.PartAgg.Events
{
    public class PartOrganizationPermissionChangedEvent : DomainEventBase
    {
        public long PartId { get; }
        public long OrganizationId { get; }

        public PartOrganizationPermissionChangedEvent(long partId, long organizationId)
        {
            PartId = partId;
            OrganizationId = organizationId;
        }
    }
}
