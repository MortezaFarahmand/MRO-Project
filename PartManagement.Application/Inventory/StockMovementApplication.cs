using _0_Framework.Application;
using PartManagement.Application.Contracts.Inventory.StockMovement;
using PartManagement.Application.Contracts.Inventory.StockMovement.Commands;
using PartManagement.Application.Contracts.Inventory.StockMovement.DTOs;
using PartManagement.Application.Contracts.Inventory.StockMovement.Queries;
using PartManagement.Domain.InventoryAgg.StockMovementAgg;

namespace PartManagement.Application.Inventory
{
    public class StockMovementApplication : IStockMovementApplication
    {
        private readonly IStockMovementRepository _repository;
        private readonly IAuthHelper _authHelper;

        public StockMovementApplication(IStockMovementRepository repository, IAuthHelper authHelper)
        {
            _repository = repository;
            _authHelper = authHelper;
        }

        public OperationResult CreateReceipt(CreateReceiptCommand command)
        {
            var movement = StockMovement.CreateReceipt(
                command.MovementNumber,
                command.PartId,
                command.ToStoreId,
                command.Quantity,
                command.SerialNumber,
                command.BatchNumber,
                command.InventoryItemId,
                command.OrganizationId,
                command.RelatedDocumentId,
                command.RelatedDocumentType,
                command.Note,
                _authHelper.CurrentAccountId());

            _repository.Create(movement);
            _repository.SaveChanges();
            return new OperationResult().Succeeded("ورود انبار ثبت شد");
        }

        public OperationResult CreateIssue(CreateIssueCommand command)
        {
            var movement = StockMovement.CreateIssue(
                command.MovementNumber,
                command.PartId,
                command.FromStoreId,
                command.Quantity,
                command.SerialNumber,
                command.BatchNumber,
                command.InventoryItemId,
                command.OrganizationId,
                command.RelatedDocumentId,
                command.RelatedDocumentType,
                command.Note,
                _authHelper.CurrentAccountId());

            _repository.Create(movement);
            _repository.SaveChanges();
            return new OperationResult().Succeeded("خروج انبار ثبت شد");
        }

        public OperationResult CreateTransfer(CreateTransferCommand command)
        {
            var movement = StockMovement.CreateTransfer(
                command.MovementNumber,
                command.PartId,
                command.FromStoreId,
                command.ToStoreId,
                command.Quantity,
                command.SerialNumber,
                command.BatchNumber,
                command.InventoryItemId,
                command.OrganizationId,
                command.Note,
                _authHelper.CurrentAccountId());

            _repository.Create(movement);
            _repository.SaveChanges();
            return new OperationResult().Succeeded("انتقال انبار ثبت شد");
        }

        public StockMovementDto GetById(long id)
        {
            var entity = _repository.Get(id);
            return entity == null ? null : MapToDto(entity);
        }

        public List<StockMovementDto> GetByPartId(long partId)
        {
            return _repository.GetByPartId(partId).Select(MapToDto).ToList();
        }

        public List<StockMovementDto> Search(SearchStockMovementQuery query)
        {
            var list = _repository.Search(
                query.MovementNumber,
                query.PartId,
                query.StoreId,
                query.MovementType,
                query.FromDate,
                query.ToDate);

            return list.Select(MapToDto).ToList();
        }

        private StockMovementDto MapToDto(StockMovement entity)
        {
            return new StockMovementDto
            {
                Id = entity.Id,
                MovementNumber = entity.MovementNumber,
                MovementType = entity.MovementType.ToString(),
                PartId = entity.PartId,
                FromStoreId = entity.FromStoreId,
                ToStoreId = entity.ToStoreId,
                Quantity = entity.Quantity,
                SerialNumber = entity.SerialNumber,
                BatchNumber = entity.BatchNumber,
                MovementDate = entity.MovementDate,
                Note = entity.Note
            };
        }
    }
}
