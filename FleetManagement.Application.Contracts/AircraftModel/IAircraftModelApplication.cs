using _0_Framework.Application;
using FleetManagement.Application.Contracts.AircraftModel.Commands;
using FleetManagement.Application.Contracts.AircraftModel.DTOs;
using FleetManagement.Application.Contracts.AircraftModel.Queries;

namespace FleetManagement.Application.Contracts.AircraftModel
{
    public interface IAircraftModelApplication
    {
        OperationResult Create(CreateAircraftModelCommand command);
        OperationResult Edit(EditAircraftModelCommand command);
        OperationResult Activate(long id);
        OperationResult Deactivate(long id);

        AircraftModelDto GetById(long id);
        List<AircraftModelDto> GetByTypeId(long aircraftTypeId);
        List<AircraftModelDto> Search(SearchAircraftModelQuery query);
    }
}
