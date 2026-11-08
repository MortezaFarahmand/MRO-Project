namespace PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg.Enum
{
    public enum InspectionStatus
    {
        NotRequired = 0,
        Pending = 1,
        InProgress = 2,
        Approved = 3,
        Rejected = 4,
        NeedsMoreInspection = 5,
        Overridden = 6,   // تأیید مدیریتی علیرغم Fail/Reject
        Failed = 7
    }
}