using _0_Framework.Application;
using System.Collections.Generic;

namespace BasicDataManagement.Application.Contracts.Airport
{
    public interface IAirportApplication
    {
        OperationResult Create(CreateAirport command);
        OperationResult Edit(EditAirport command);
        EditAirport GetDetails(long id);
        List<AirportViewModel> GetAirports();
        List<AirportViewModel> Search(AirportSearchModel searchModel);
        OperationResult Activate(long id);
        OperationResult Deactivate(long id);
    }
}
