using _0_Framework.Application;
using PartManagement.Application.Contracts.Stock.StockType;
using PartManagement.Domain.Stock.StockTypeAgg;

namespace PartManagement.Application.StockApplications
{
    public class StockTypeApplication : IStockTypeApplication
    {
        private readonly IStockTypeRepository _stockTypeRepository;

        public StockTypeApplication(IStockTypeRepository stockTypeRepository)
        {
            _stockTypeRepository = stockTypeRepository;
        }


        public OperationResult Create(CreateStockType command)
        {
            var operation = new OperationResult();
            if(_stockTypeRepository.Exists(x => x.Name == command.Name || x.Code == command.Code || x.ColorCode == command.ColorCode))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var stockType = new StockType(command.Name, command.Code, command.ColorCode);
            _stockTypeRepository.Create(stockType);
            _stockTypeRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditStockType command)
        {
            var operation = new OperationResult();
            var stockType = _stockTypeRepository.Get(command.Id);

            if (stockType == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_stockTypeRepository.Exists(x => x.Id == command.Id && x.Name == command.Name && x.Code == command.Code))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);


            stockType.Edit(command.Name, command.Code, command.ColorCode);
            _stockTypeRepository.SaveChanges();
            return operation.Succeeded();
        }


        public EditStockType GetDetails(long id)
        {
            return _stockTypeRepository.Getdetails(id);
        }

        public List<StockTypeViewModel> GetStockTypes(StockTypeViewModel viewModel)
        {
            return _stockTypeRepository.GetStockTypes();
        }

        public List<StockTypeViewModel> Search(StockTypeSearchModel searchModel)
        {
            return _stockTypeRepository.Search(searchModel);
        }
    }
}
