using _0_Framework.Application;
using PartManagement.Application.Contracts.Part.Commands;
using PartManagement.Application.Contracts.Part.DTOs;
using PartManagement.Application.Contracts.Part.Queries;
namespace PartManagement.Application.Contracts.Part
{
    public interface IPartApplication
    {
        // Commands
        OperationResult Create(CreatePartCommand command);
        OperationResult Edit(EditPartCommand command);
        OperationResult MakeGlobal(MakePartGlobalCommand command);
        OperationResult AddManufacturer(AddPartManufacturerCommand command);
        OperationResult AddChild(AddPartChildCommand command);
        OperationResult AddOrganization(AddPartOrganizationCommand command);

        // Queries
        PartDto GetById(long id);
        PartDto GetByPartNumber(string partNumber);
        List<PartDto> Search(SearchPartsQuery query);
    }
}
