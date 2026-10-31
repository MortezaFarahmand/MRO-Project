
using System.Collections.Generic;

namespace _0_Framework.Domain
{
    public interface IDomainEventDispatcher
    {
        void DispatchEvents(IEnumerable<IDomainEvent> events);
    }
}
