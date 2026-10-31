using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Domain
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
