using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartManagement.Domain.PartAgg.Events
{
    public class PartCreatedEvent : DomainEventBase
    {
        public long PartId { get; }
        public string PartNumber { get; }
        public string Description { get; }
        public bool IsGlobal { get; }
        public long? OwnerOrganizationId { get; }

        public PartCreatedEvent(long partId, string partNumber, string description, bool isGlobal, long? ownerOrganizationId)
        {
            PartId = partId;
            PartNumber = partNumber;
            Description = description;
            IsGlobal = isGlobal;
            OwnerOrganizationId = ownerOrganizationId;
        }
    }
}
