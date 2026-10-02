using _0_Framework.Application;

namespace PartManagement.Application.Contracts.Basic_data.EquipmentType
{
    public interface IEquipmentTypeApplication
    {
        OperationResult Create(CreateEquipmentType command);
        OperationResult Edit(EditEquipmentType command);
        EditEquipmentType GetDetails(long id);
        List<EquipmentTypeViewModel> GetEquipmentTypes();
        List<EquipmentTypeViewModel> Search(EquipmentTypeSearchModel searchModel);
        OperationResult Activate(long id);
        OperationResult Deactivate(long id);
    }
}
