using _0_Framework.Application;
using _0_Framework.Infrastructure;
using PartManagement.Application.Contracts.Inventory.ReceivingInspection;
using PartManagement.Application.Contracts.Inventory.StockMovement;
using PartManagement.Application.Contracts.Inventory.StockMovement.Commands;
using PartManagement.Application.Contracts.Inventory.StockMovement.DTOs;
using PartManagement.Application.Contracts.Inventory.StockMovement.Queries;
using PartManagement.Domain.InventoryAgg.StockMovementAgg;
using PartManagement.Domain.PartAgg;
using PartManagement.Domain.Stock.StoreAgg;

namespace PartManagement.Application.Inventory
{
    public class StockMovementApplication : IStockMovementApplication
    {
        private readonly IStockMovementRepository _repository;
        private readonly IAuthHelper _authHelper;
        private readonly IDocumentNumberGenerator _documentNumberGenerator;
        private readonly IReceivingInspectionApplication _receivingInspectionApplication;
        private readonly IStoreRepository _storeRepository;
        private readonly IPartRepository _partRepository;

        public StockMovementApplication(
            IStockMovementRepository repository,
            IAuthHelper authHelper,
            IDocumentNumberGenerator documentNumberGenerator,
            IReceivingInspectionApplication receivingInspectionApplication,
            IStoreRepository storeRepository,
            IPartRepository partRepository)
        {
            _repository = repository;
            _authHelper = authHelper;
            _documentNumberGenerator = documentNumberGenerator;
            _receivingInspectionApplication = receivingInspectionApplication;
            _storeRepository = storeRepository;
            _partRepository = partRepository;
        }

        public OperationResult CreateReceipt(CreateReceiptCommand command)
        {
            var result = new OperationResult();

            if (command.PartId <= 0)
                return result.Failed("قطعه را انتخاب کنید.");
            if (command.ToStoreId <= 0)
                return result.Failed("انبار مقصد را انتخاب کنید.");
            if (command.Quantity <= 0)
                return result.Failed("تعداد نامعتبر است.");

            var store = _storeRepository.Get(command.ToStoreId);
            if (store == null)
                return result.Failed("انبار یافت نشد.");

            var movementNumber = _documentNumberGenerator.Next("SM");

            var part = _partRepository.Get(command.PartId);

            var movement = StockMovement.CreateReceipt(
                movementNumber,
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

            // اتصال به بازرسی ورود (بعد از ذخیره موفق)
            var inspectResult = _receivingInspectionApplication.CreateResultForReceipt(
                receiptId: movement.Id,
                receiptLineId: null,                    // مدل تک‌خطی
                inventoryItemId: command.InventoryItemId,
                storeId: command.ToStoreId,
                stockTypeId: null,                      // در صورت دسترسی به Part پر کنید
                partKeyword: part?.Keyword);

            // اگر بازرسی اجباری است و ساخت Result شکست خورد
            var policy = store.ReceivingPolicy;
            if (!inspectResult.IsSucceeded
                && policy != null
                && policy.InspectionMode == Domain.StockAgg.StoreAgg.Enum.InspectionMode.Mandatory)
            {
                // در صورت داشتن UnitOfWork واقعی اینجا Rollback کنید.
                // فعلاً پیام خطا برمی‌گردد؛ movement قبلاً ذخیره شده.
                return result.Failed(inspectResult.Message);
            }

            // اگر بازرسی لازم نبود، پیام اصلی موفقیت
            if (inspectResult.IsSucceeded
                && inspectResult.Message != null
                && inspectResult.Message.Contains("لازم نیست"))
                return result.Succeeded("ورود انبار ثبت شد.");

            if (inspectResult.IsSucceeded)
                return result.Succeeded("ورود انبار ثبت شد. بازرسی ورود در صف قرار گرفت.");

            // Optional / Warning: دریافت OK است؛ فقط هشدار
            return result.Succeeded(
                "ورود انبار ثبت شد. " + (inspectResult.Message ?? "بازرسی ایجاد نشد."));
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
                PartNumber = entity.Part?.PartNumber,
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
