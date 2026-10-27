using _0_Framework.Application;
using PartManagement.Application.Contracts.Stock.StockType;
using PartManagement.Domain.Stock.StockTypeAgg;

namespace PartManagement.Application.StockApplications
{
    public class StockTypeApplication : IStockTypeApplication
    {
        private readonly IStockTypeRepository _stockTypeRepository;
        private readonly IAuthHelper _authHelper;


        public StockTypeApplication(IStockTypeRepository stockTypeRepository, IAuthHelper authHelper)
        {
            _stockTypeRepository = stockTypeRepository;
            _authHelper = authHelper;
        }


        public OperationResult Create(CreateStockType command)
        {
            var operation = new OperationResult();

            var orgId = _authHelper.SetOrganizationId(command.OrganizationId);

            if (_stockTypeRepository.Exists(x => (x.Name == command.Name || x.Code == command.Code
                  || x.ColorCode == command.ColorCode) && x.OrganizationId == command.OrganizationId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var stockType = new StockType(command.Name, command.Code, command.ColorCode, orgId);
            _stockTypeRepository.Create(stockType);
            _stockTypeRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditStockType command)
        {
            var operation = new OperationResult();

            var orgId = _authHelper.SetOrganizationId(command.OrganizationId);

            var stockType = _stockTypeRepository.Get(command.Id);

            if (stockType == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_stockTypeRepository.Exists(x => x.Id == command.Id && x.Name == command.Name
                && x.Code == command.Code && x.OrganizationId == command.OrganizationId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);


            stockType.Edit(command.Name, command.Code, command.ColorCode, orgId);
            _stockTypeRepository.SaveChanges();
            return operation.Succeeded();
        }


        public EditStockType GetDetails(long id)
        {
            return _stockTypeRepository.Getdetails(id);
        }

        public List<StockTypeViewModel> GetStockTypesBy(long? organizationId)
        {
            var searchModel = new StockTypeSearchModel();
            searchModel.OrganizationId = _authHelper.SetOrganizationId(organizationId);
            // برای کاربر عادی، سازمان خودش را در دراپ‌داون انتخاب کن
            searchModel.IsAdmin = _authHelper.CurrentAccountIsAdmin();
            //if (!searchModel.IsAdmin)
            //{ searchModel.OrganizationId = currentOrganizationId; }

            return _stockTypeRepository.GetStockTypesBy(searchModel.OrganizationId);
        }

        //public CreateStockType GetStockTypesBy()
        //{
        //    return new CreateStockType();
        //}

        public List<StockTypeViewModel> Search(StockTypeSearchModel searchModel)
        {
            searchModel ??= new StockTypeSearchModel();
            long? currentOrganizationId = _authHelper.SetOrganizationId(searchModel.OrganizationId);
            // برای کاربر عادی، سازمان خودش را در دراپ‌داون انتخاب کن
            searchModel.IsAdmin = _authHelper.CurrentAccountIsAdmin();
            if (!searchModel.IsAdmin) 
            { searchModel.OrganizationId = currentOrganizationId; }

            return _stockTypeRepository.Search(searchModel);
        }
    }
}
