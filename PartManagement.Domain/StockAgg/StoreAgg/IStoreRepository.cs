using _0_Framework.Domain;
//using PartManagement.Application.Contracts.Stock.Store;

namespace PartManagement.Domain.Stock.StoreAgg
{
    public interface IStoreRepository : IRepository<long, Store>
    {
        Store GetByOrganizationId(long organizationId);
        List<Store> Search(string name, string code, long? organizationId, bool? isActive = null);
    }
}
//namespace PartManagement.Domain.Stock.StoreAgg
//{
//    public interface IStoreRepository : IRepository<long, Store>
//    {

//        EditStore GetDetails(long id);
//Store GetStoresBy(long organizationId);

//        List<StoreViewModel> Search(StoreSearchModel searchModel);
//    }
//}
