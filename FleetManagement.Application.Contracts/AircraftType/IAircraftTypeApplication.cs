using _0_Framework.Application;
using FleetManagement.Application.Contracts.AircraftType.Commands;
using FleetManagement.Application.Contracts.AircraftType.DTOs;
using FleetManagement.Application.Contracts.AircraftType.Queries;

namespace FleetManagement.Application.Contracts.AircraftType
{
    public interface IAircraftTypeApplication
    {
        OperationResult Create(CreateAircraftTypeCommand command);
        OperationResult Edit(EditAircraftTypeCommand command);
        OperationResult Activate(long id);
        OperationResult Deactivate(long id);

        AircraftTypeDto GetById(long id);
        List<AircraftTypeDto> Search(SearchAircraftTypeQuery query);
        List<AircraftTypeDto> GetAllActive();
    }
}