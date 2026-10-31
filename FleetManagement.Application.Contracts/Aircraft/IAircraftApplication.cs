using _0_Framework.Application;
using FleetManagement.Application.Contracts.Aircraft.Commands;
using FleetManagement.Application.Contracts.Aircraft.DTOs;
using FleetManagement.Application.Contracts.Aircraft.Queries;

namespace FleetManagement.Application.Contracts.Aircraft
{
    public interface IAircraftApplication
    {
        // Commands
        OperationResult Create(CreateAircraftCommand command);
        OperationResult Edit(EditAircraftCommand command);
        OperationResult ChangeStatus(ChangeAircraftStatusCommand command);
        OperationResult Activate(long id);
        OperationResult Deactivate(long id);
        OperationResult UpdateLife(UpdateAircraftLifeCommand command);

        //Queries
        AircraftDto GetById(long id);
        AircraftDto GetByRegistration(string registration);
        List<AircraftDto> Search(SearchAircraftQuery query);
    }
}
