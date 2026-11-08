using PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg.Enum;

namespace PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg
{
    public class ReceivingInspectionCheck
    {
        public long Id { get; private set; }
        public long ResultId { get; private set; }
        public long InspectionItemId { get; private set; }
        public int StepIndex { get; private set; }
        public CheckOutcome Outcome { get; private set; }
        public string Remark { get; private set; }
        public DateTime? AnsweredAt { get; private set; }
        public long? AnsweredByUserId { get; private set; }

        protected ReceivingInspectionCheck() { }

        public ReceivingInspectionCheck(long resultId, long inspectionItemId, int stepIndex)
        {
            ResultId = resultId;
            InspectionItemId = inspectionItemId;
            StepIndex = stepIndex;
            Outcome = CheckOutcome.Pending;
        }

        public void Answer(CheckOutcome outcome, string remark, long userId, bool allowNotApplicable)
        {
            if (outcome == CheckOutcome.NotApplicable && !allowNotApplicable)
                throw new InvalidOperationException("این آیتم اجازه N/A ندارد.");

            Outcome = outcome;
            Remark = string.IsNullOrWhiteSpace(remark) ? null : remark.Trim();
            AnsweredAt = DateTime.UtcNow;
            AnsweredByUserId = userId;
        }

        public bool IsAnswered => Outcome != CheckOutcome.Pending;
    }
}