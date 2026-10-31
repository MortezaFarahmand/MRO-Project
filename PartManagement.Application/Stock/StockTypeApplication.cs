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

            if (_stockTypeRepository.Exists(x =>
                (x.Name == command.Name || x.Code == command.Code || x.ColorCode == command.ColorCode)
                && x.OrganizationId == orgId))
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

            if (_stockTypeRepository.Exists(x =>
                x.Id != command.Id &&
                (x.Name == command.Name || x.Code == command.Code) &&
                x.OrganizationId == orgId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            stockType.Edit(command.Name, command.Code, command.ColorCode, orgId);
            _stockTypeRepository.SaveChanges();

            return operation.Succeeded();
        }


        public EditStockType GetDetails(long id)
        {
            var stockType = _stockTypeRepository.Get(id);
            if (stockType == null)
                return null;

            return new EditStockType
            {
                Id = stockType.Id,
                Name = stockType.Name,
                Code = stockType.Code,
                ColorCode = stockType.ColorCode,
                OrganizationId = stockType.OrganizationId,
                CreationDate = stockType.CreationDate.ToString()
            };
        }

        public List<StockTypeViewModel> GetStockTypesBy(long? organizationId)
        {
            var orgId = _authHelper.SetOrganizationId(organizationId);

            var stockTypes = _stockTypeRepository.GetByOrganizationId(orgId);

            return stockTypes.Select(x => new StockTypeViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                ColorCode = x.ColorCode,
                OrganizationId = x.OrganizationId
            }).ToList();
        }

        public List<StockTypeViewModel> Search(StockTypeSearchModel searchModel)
        {
            searchModel ??= new StockTypeSearchModel();

            long? currentOrganizationId = _authHelper.SetOrganizationId(searchModel.OrganizationId);
            searchModel.IsAdmin = _authHelper.CurrentAccountIsAdmin();

            if (!searchModel.IsAdmin)
                searchModel.OrganizationId = currentOrganizationId;

            var stockTypes = _stockTypeRepository.Search(
                searchModel.Name,
                searchModel.OrganizationId);

            return stockTypes.Select(x => new StockTypeViewModel
            {
                Id = x.Id,
                Name = x.Name ?? "-",
                Code = x.Code ?? "-",
                ColorCode = x.ColorCode ?? "-",
                CreationDate = x.CreationDate.ToShortDateString(),
                OrganizationId = x.OrganizationId
            }).ToList();
        }
    }
}
