using _0_Framework.Domain;
using BasicDataManagement.Application.Contract.Entiti;
using System.Collections.Generic;

namespace BasicDataManagement.Domain.EntitiAgg
{
    public interface IEntitiRepository : IRepository<long, Entiti>
    {
        List<EntitiViewModel> GetEntitis();
        EditEntiti GetDetails(long id);
    }
}
