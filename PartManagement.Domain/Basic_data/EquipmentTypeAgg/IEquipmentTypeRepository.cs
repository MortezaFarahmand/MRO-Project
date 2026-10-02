using _0_Framework.Domain;
using PartManagement.Application.Contracts.Basic_data.EquipmentType;

namespace PartManagement.Domain.Basic_data.EquipmentTypeAgg
{
    public interface IEquipmentTypeRepository : IRepository<long,EquipmentType>
    {
        List<EquipmentTypeViewModel> GetEquipmentTypes();
        EditEquipmentType Getdetails(long id);
        List<EquipmentTypeViewModel> Search(EquipmentTypeSearchModel searchModel);
    }
}
