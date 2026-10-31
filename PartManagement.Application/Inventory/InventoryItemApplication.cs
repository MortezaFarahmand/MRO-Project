using _0_Framework.Application;
using PartManagement.Application.Contracts.Inventory.InventoryItem;
using PartManagement.Application.Contracts.Inventory.InventoryItem.Commands;
using PartManagement.Application.Contracts.Inventory.InventoryItem.DTOs;
using PartManagement.Application.Contracts.Inventory.InventoryItem.Queries;
using PartManagement.Domain.InventoryAgg.InventoryItemAgg;

namespace PartManagement.Application.Inventory
{
    public class InventoryItemApplication : IInventoryItemApplication
    {
        private readonly IInventoryItemRepository _repository;
        private readonly IAuthHelper _authHelper;

        public InventoryItemApplication(
            IInventoryItemRepository repository,
            IAuthHelper authHelper)
        {
            _repository = repository;
            _authHelper = authHelper;
        }

        public OperationResult CreateSerialized(CreateSerializedInventoryCommand command)
        {
            if (_repository.ExistsSerialNumber(command.SerialNumber))
                return new OperationResult().Failed("شماره سریال تکراری است");

            var item = InventoryItem.CreateSerialized(
                command.PartId,
                command.SerialNumber,
                command.StoreId,
                command.OwnerOrganizationId,
                command.ManufactureDate,
                command.ExpireDate,
                _authHelper.CurrentAccountId());

            _repository.Create(item);
            _repository.SaveChanges();

            return new OperationResult().Succeeded("قطعه سریالی با موفقیت ثبت شد");
        }

        public OperationResult CreateBatch(CreateBatchInventoryCommand command)
        {
            var item = InventoryItem.CreateBatch(
                command.PartId,
                command.BatchNumber,
                command.Quantity,
                command.StoreId,
                command.OwnerOrganizationId,
                command.ManufactureDate,
                command.ExpireDate,
                _authHelper.CurrentAccountId());

            _repository.Create(item);
            _repository.SaveChanges();

            return new OperationResult().Succeeded("موجودی Batch با موفقیت ثبت شد");
        }

        public OperationResult Allocate(long id)
        {
            var item = _repository.Get(id);
            if (item == null)
                return new OperationResult().Failed("آیتم موجودی یافت نشد");

            item.Allocate();
            _repository.SaveChanges();

            return new OperationResult().Succeeded("آیتم رزرو شد");
        }

        public OperationResult Install(InstallInventoryCommand command)
        {
            var item = _repository.Get(command.Id);
            if (item == null)
                return new OperationResult().Failed("آیتم موجودی یافت نشد");

            item.Install(command.AircraftId, command.FlightHours, command.Cycles);
            _repository.SaveChanges();

            return new OperationResult().Succeeded("قطعه روی هواپیما نصب شد");
        }

        public OperationResult ReturnToStock(ReturnToStockCommand command)
        {
            var item = _repository.Get(command.Id);
            if (item == null)
                return new OperationResult().Failed("آیتم موجودی یافت نشد");

            item.ReturnToStock(command.StoreId);
            _repository.SaveChanges();

            return new OperationResult().Succeeded("قطعه به انبار برگشت");
        }

        public OperationResult SendForRepair(long id)
        {
            var item = _repository.Get(id);
            if (item == null)
                return new OperationResult().Failed("آیتم موجودی یافت نشد");

            item.SendForRepair();
            _repository.SaveChanges();

            return new OperationResult().Succeeded("قطعه برای تعمیر ارسال شد");
        }

        public OperationResult Quarantine(QuarantineInventoryCommand command)
        {
            var item = _repository.Get(command.Id);
            if (item == null)
                return new OperationResult().Failed("آیتم موجودی یافت نشد");

            item.Quarantine(command.Reason);
            _repository.SaveChanges();

            return new OperationResult().Succeeded("آیتم به قرنطینه منتقل شد");
        }

        public OperationResult Scrap(ScrapInventoryCommand command)
        {
            var item = _repository.Get(command.Id);
            if (item == null)
                return new OperationResult().Failed("آیتم موجودی یافت نشد");

            item.Scrap(command.Reason);
            _repository.SaveChanges();

            return new OperationResult().Succeeded("آیتم اسقاط شد");
        }

        public InventoryItemDto GetById(long id)
        {
            var item = _repository.Get(id);
            return item == null ? null : MapToDto(item);
        }

        public InventoryItemDto GetBySerialNumber(string serialNumber)
        {
            var item = _repository.GetBySerialNumber(serialNumber);
            return item == null ? null : MapToDto(item);
        }

        public List<InventoryItemDto> Search(SearchInventoryQuery query)
        {
            query ??= new SearchInventoryQuery();

            var list = _repository.Search(
                query.PartId,
                query.StoreId,
                query.SerialNumber,
                query.BatchNumber,
                query.Status);

            return list.Select(MapToDto).ToList();
        }

        private InventoryItemDto MapToDto(InventoryItem item)
        {
            return new InventoryItemDto
            {
                Id = item.Id,
                PartId = item.PartId,
                StoreId = item.StoreId,
                AircraftId = item.AircraftId,
                SerialNumber = item.SerialNumber,
                BatchNumber = item.BatchNumber,
                Quantity = item.Quantity,
                Status = item.Status.ToString(),
                ManufactureDate = item.ManufactureDate,
                ExpireDate = item.ExpireDate,
                InstallDate = item.InstallDate,
                Note = item.Note
            };
        }
    }
}

