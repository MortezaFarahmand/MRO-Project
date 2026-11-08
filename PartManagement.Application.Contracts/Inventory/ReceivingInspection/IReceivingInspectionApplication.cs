using _0_Framework.Application;
using PartManagement.Application.Contracts.Inventory.ReceivingInspection.Commands;
using PartManagement.Application.Contracts.Inventory.ReceivingInspection.DTOs;

namespace PartManagement.Application.Contracts.Inventory.ReceivingInspection
{
    public interface IReceivingInspectionApplication
    {
        // Checklist
        OperationResult CreateItem(CreateInspectionItemCommand command);
        OperationResult EditItem(EditInspectionItemCommand command);
        OperationResult ActivateItem(long id);
        OperationResult DeactivateItem(long id);
        InspectionItemDto GetItemDetails(long id);
        List<InspectionItemDto> SearchItems(string code = null, bool? isActive = null);

        // بعد از Receive توسط سرویس دریافت صدا زده می‌شود
        OperationResult CreateResultForReceipt(
            long receiptId,
            long? receiptLineId,
            long? inventoryItemId,
            long storeId,
            long? stockTypeId,
            string partKeyword);

        // Wizard
        ReceivingInspectionWizardDto GetWizard(long resultId);
        OperationResult Start(StartInspectionCommand command);
        OperationResult AnswerStep(AnswerWizardStepCommand command);
        OperationResult Complete(CompleteInspectionCommand command);
        OperationResult Override(OverrideInspectionCommand command);

        List<ReceivingInspectionResultDto> GetPendingByStore(long storeId);
        List<ReceivingInspectionResultDto> GetByReceipt(long receiptId);
    }
}
