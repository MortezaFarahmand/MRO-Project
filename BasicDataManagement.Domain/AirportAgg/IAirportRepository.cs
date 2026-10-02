using _0_Framework.Domain;
using BasicDataManagement.Application.Contracts.Airport;
using System.Collections.Generic;

namespace BasicDataManagement.Domain.AirportAgg
{
    public interface IAirportRepository : IRepository<long, Airport>
    {
        List<AirportViewModel> GetAirports();
        EditAirport GetDetails(long id);
        List<AirportViewModel> Search(AirportSearchModel searchModel);
    }
}
