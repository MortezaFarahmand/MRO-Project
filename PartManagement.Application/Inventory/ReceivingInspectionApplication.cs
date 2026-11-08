using _0_Framework.Application;
using PartManagement.Application.Contracts.Inventory.ReceivingInspection;
using PartManagement.Application.Contracts.Inventory.ReceivingInspection.Commands;
using PartManagement.Application.Contracts.Inventory.ReceivingInspection.DTOs;
using PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg;
using PartManagement.Domain.Stock.StoreAgg;


namespace PartManagement.Application.Inventory
{
    public class ReceivingInspectionApplication : IReceivingInspectionApplication
    {
        private readonly IReceivingInspectionItemRepository _itemRepository;
        private readonly IReceivingInspectionResultRepository _resultRepository;
        private readonly IStoreRepository _storeRepository;

        public ReceivingInspectionApplication(
            IReceivingInspectionItemRepository itemRepository,
            IReceivingInspectionResultRepository resultRepository,
            IStoreRepository storeRepository)
        {
            _itemRepository = itemRepository;
            _resultRepository = resultRepository;
            _storeRepository = storeRepository;
        }

        // ==================== Checklist CRUD ====================

        public OperationResult CreateItem(CreateInspectionItemCommand command)
        {
            var result = new OperationResult();

            if (string.IsNullOrWhiteSpace(command.Code))
                return result.Failed("کد الزامی است.");
            if (string.IsNullOrWhiteSpace(command.Title))
                return result.Failed("عنوان الزامی است.");

            var exists = _itemRepository.Exists(x => x.Code == command.Code.Trim());
            if (exists)
                return result.Failed("کد تکراری است.");

            var entity = new ReceivingInspectionItem(
                command.Code,
                command.Title,
                command.Description,
                command.HelpText,
                command.DisplayOrder,
                command.IsMandatory,
                command.AllowNotApplicable,
                command.ApplicableKeyword,
                command.StockTypeId);

            _itemRepository.Create(entity);
            _itemRepository.SaveChanges();
            return result.Succeeded("آیتم چک‌لیست ثبت شد.");
        }

        public OperationResult EditItem(EditInspectionItemCommand command)
        {
            var result = new OperationResult();
            var entity = _itemRepository.Get(command.Id);
            if (entity == null)
                return result.Failed("یافت نشد.");

            entity.Edit(
                command.Title,
                command.Description,
                command.HelpText,
                command.DisplayOrder,
                command.IsMandatory,
                command.AllowNotApplicable,
                command.ApplicableKeyword,
                command.StockTypeId);

            _itemRepository.SaveChanges();
            return result.Succeeded("ویرایش شد.");
        }

        public OperationResult ActivateItem(long id)
        {
            var result = new OperationResult();
            var entity = _itemRepository.Get(id);
            if (entity == null) return result.Failed("یافت نشد.");
            entity.Activate();
            _itemRepository.SaveChanges();
            return result.Succeeded();
        }

        public OperationResult DeactivateItem(long id)
        {
            var result = new OperationResult();
            var entity = _itemRepository.Get(id);
            if (entity == null) return result.Failed("یافت نشد.");
            entity.Deactivate();
            _itemRepository.SaveChanges();
            return result.Succeeded();
        }

        public InspectionItemDto GetItemDetails(long id)
        {
            var entity = _itemRepository.Get(id);
            return entity == null ? null : MapItem(entity);
        }

        public List<InspectionItemDto> SearchItems(string code = null, bool? isActive = null)
        {
            var query = _itemRepository.Get().AsQueryable();

            if (!string.IsNullOrWhiteSpace(code))
                query = query.Where(x => x.Code.Contains(code.Trim()));

            if (isActive.HasValue)
                query = query.Where(x => x.IsActive == isActive.Value);

            return query
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Code)
                .Select(MapItem)
                .ToList();
        }

        // ==================== ایجاد بعد از Receive ====================

        public OperationResult CreateResultForReceipt(
            long receiptId,
            long? receiptLineId,
            long? inventoryItemId,
            long storeId,
            long? stockTypeId,
            string partKeyword)
        {
            var result = new OperationResult();

            var store = _storeRepository.Get(storeId);
            if (store == null)
                return result.Failed("انبار یافت نشد.");

            // اگر متد را روی Store گذاشته‌اید:
            // store.EnsureDefaultPolicies();
            var policy = store.ReceivingPolicy;
            if (policy == null || !policy.RequiresInspection)
                return result.Succeeded("بازرسی برای این انبار لازم نیست.");

            var items = _itemRepository.GetActiveOrdered(stockTypeId, partKeyword);
            if (items == null || items.Count == 0)
                return result.Failed("چک‌لیست فعالی برای بازرسی تعریف نشده است.");

            var entity = ReceivingInspectionResult.Create(
                receiptId,
                receiptLineId,
                inventoryItemId,
                storeId,
                items,
                policy.InspectionDeadlineHours);

            _resultRepository.Create(entity);


            _resultRepository.SaveChanges();
            return result.Succeeded("بازرسی ورود ایجاد شد.");
        }

        // ==================== Wizard ====================

        public ReceivingInspectionWizardDto GetWizard(long resultId)
        {
            var entity = _resultRepository.GetWithChecks(resultId);
            if (entity == null) return null;

            var itemIds = entity.Checks.Select(c => c.InspectionItemId).Distinct().ToList();
            var items = _itemRepository.Get()
                .Where(x => itemIds.Contains(x.Id))
                .ToDictionary(x => x.Id, x => x);

            return new ReceivingInspectionWizardDto
            {
                ResultId = entity.Id,
                ReceiptId = entity.ReceiptId,
                InventoryItemId = entity.InventoryItemId,
                StoreId = entity.StoreId,
                Status = entity.Status,
                CurrentStepIndex = entity.CurrentStepIndex,
                TotalSteps = entity.TotalSteps,
                CompletedSteps = entity.CompletedSteps,
                AllowsPlacement = entity.AllowsPlacement,
                IsOverridden = entity.IsOverridden,
                OverrideReason = entity.OverrideReason,
                Steps = entity.Checks
                    .OrderBy(c => c.StepIndex)
                    .Select(c =>
                    {
                        items.TryGetValue(c.InspectionItemId, out var item);
                        return new WizardStepDto
                        {
                            Index = c.StepIndex,
                            InspectionItemId = c.InspectionItemId,
                            Code = item?.Code,
                            Title = item?.Title,
                            HelpText = item?.HelpText,
                            IsMandatory = item?.IsMandatory ?? true,
                            AllowNotApplicable = item?.AllowNotApplicable ?? false,
                            Outcome = c.Outcome,
                            Remark = c.Remark
                        };
                    }).ToList()
            };
        }

        public OperationResult Start(StartInspectionCommand command)
        {
            var result = new OperationResult();
            var entity = _resultRepository.GetWithChecks(command.ResultId);
            if (entity == null) return result.Failed("بازرسی یافت نشد.");

            try
            {
                entity.Start(command.InspectorUserId);
                _resultRepository.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception ex)
            {
                return result.Failed(ex.Message);
            }
        }

        public OperationResult AnswerStep(AnswerWizardStepCommand command)
        {
            var result = new OperationResult();
            var entity = _resultRepository.GetWithChecks(command.ResultId);
            if (entity == null) return result.Failed("بازرسی یافت نشد.");

            var check = entity.Checks.FirstOrDefault(c => c.StepIndex == command.StepIndex);
            if (check == null) return result.Failed("Step نامعتبر است.");

            var item = _itemRepository.Get(check.InspectionItemId);
            var allowNa = item?.AllowNotApplicable ?? false;

            try
            {
                entity.AnswerStep(
                    command.StepIndex,
                    command.Outcome,
                    command.Remark,
                    command.UserId,
                    allowNa);

                _resultRepository.SaveChanges();
                return result.Succeeded();
            }
            catch (Exception ex)
            {
                return result.Failed(ex.Message);
            }
        }

        public OperationResult Complete(CompleteInspectionCommand command)
        {
            var result = new OperationResult();
            var entity = _resultRepository.GetWithChecks(command.ResultId);
            if (entity == null) return result.Failed("بازرسی یافت نشد.");

            var itemIds = entity.Checks.Select(c => c.InspectionItemId).ToList();
            var mandatoryIds = _itemRepository.Get()
                .Where(x => itemIds.Contains(x.Id) && x.IsMandatory)
                .Select(x => x.Id)
                .ToList();

            try
            {
                entity.Complete(
                    command.FinalStatus,
                    command.InspectorUserId,
                    command.Notes,
                    mandatoryIds);

                // همگام‌سازی وضعیت روی موجودی (اگر Repository را تزریق کرده‌اید)
                // ApplyToInventory(entity);

                _resultRepository.SaveChanges();
                return result.Succeeded("بازرسی نهایی شد.");
            }
            catch (Exception ex)
            {
                return result.Failed(ex.Message);
            }
        }

        public OperationResult Override(OverrideInspectionCommand command)
        {
            var result = new OperationResult();
            var entity = _resultRepository.GetWithChecks(command.ResultId);
            if (entity == null) return result.Failed("بازرسی یافت نشد.");

            try
            {
                entity.OverrideRejection(command.AuthorizedUserId, command.Reason);
                // ApplyToInventory(entity);
                _resultRepository.SaveChanges();
                return result.Succeeded("Override انجام شد؛ چیدمان مجاز است.");
            }
            catch (Exception ex)
            {
                return result.Failed(ex.Message);
            }
        }

        public List<ReceivingInspectionResultDto> GetPendingByStore(long storeId)
        {
            return _resultRepository.GetPendingByStore(storeId)
                .Select(MapResult)
                .ToList();
        }

        public List<ReceivingInspectionResultDto> GetByReceipt(long receiptId)
        {
            return _resultRepository.GetByReceipt(receiptId)
                .Select(MapResult)
                .ToList();
        }

        // ==================== Mappers ====================

        private static InspectionItemDto MapItem(ReceivingInspectionItem x) => new()
        {
            Id = x.Id,
            Code = x.Code,
            Title = x.Title,
            Description = x.Description,
            HelpText = x.HelpText,
            DisplayOrder = x.DisplayOrder,
            IsActive = x.IsActive,
            IsMandatory = x.IsMandatory,
            AllowNotApplicable = x.AllowNotApplicable,
            ApplicableKeyword = x.ApplicableKeyword,
            StockTypeId = x.StockTypeId
        };

        private static ReceivingInspectionResultDto MapResult(ReceivingInspectionResult x) => new()
        {
            Id = x.Id,
            ReceiptId = x.ReceiptId,
            InventoryItemId = x.InventoryItemId,
            StoreId = x.StoreId,
            Status = x.Status,
            StatusTitle = x.Status.ToString(),
            TotalSteps = x.TotalSteps,
            CompletedSteps = x.CompletedSteps,
            DeadlineAt = x.DeadlineAt,
            InspectedAt = x.InspectedAt,
            IsOverridden = x.IsOverridden,
            OverrideReason = x.OverrideReason,
            AllowsPlacement = x.AllowsPlacement,
            Notes = x.Notes
        };
    }
}
