using _0_Framework.Domain;
using System.Collections.Generic;

namespace _0_Framework.Infrastructure
{
    public class NullDomainEventDispatcher : IDomainEventDispatcher
    {
        public void DispatchEvents(IEnumerable<IDomainEvent> events)
        {
            // فعلاً کاری انجام نمی‌دهد
        }
    }
}