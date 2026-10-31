using _0_Framework.Domain;

namespace PartManagement.Domain.Basic_data.EquipmentTypeAgg
{
    public interface IEquipmentTypeRepository : IRepository<long, EquipmentType>
    {
        List<EquipmentType> GetAll();
        List<EquipmentType> Search(string name, string code);
    }
}

//using _0_Framework.Domain;
//using PartManagement.Application.Contracts.Basic_data.EquipmentType;

//namespace PartManagement.Domain.Basic_data.EquipmentTypeAgg
//{
//    public interface IEquipmentTypeRepository : IRepository<long, EquipmentType>
//    {
//        List<EquipmentTypeViewModel> GetEquipmentTypes();
//        EditEquipmentType Getdetails(long id);
//        List<EquipmentTypeViewModel> Search(EquipmentTypeSearchModel searchModel);
//    }
//}
