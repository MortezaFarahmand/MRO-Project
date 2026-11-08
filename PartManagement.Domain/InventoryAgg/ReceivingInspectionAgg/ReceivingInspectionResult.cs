using _0_Framework.Domain;
using PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg.Enum;

namespace PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg
{
    public class ReceivingInspectionResult : EntityBase
    {
        public long ReceiptId { get; private set; }
        public long? ReceiptLineId { get; private set; }
        public long? InventoryItemId { get; private set; }
        public long StoreId { get; private set; }

        public InspectionStatus Status { get; private set; }
        public long? InspectorUserId { get; private set; }
        public DateTime? InspectedAt { get; private set; }
        public DateTime? DeadlineAt { get; private set; }
        public string Notes { get; private set; }

        public int TotalSteps { get; private set; }
        public int CompletedSteps { get; private set; }
        public int CurrentStepIndex { get; private set; }

        public bool IsOverridden { get; private set; }
        public long? OverrideByUserId { get; private set; }
        public DateTime? OverrideAt { get; private set; }
        public string OverrideReason { get; private set; }

        private readonly List<ReceivingInspectionCheck> _checks = new();
        public IReadOnlyList<ReceivingInspectionCheck> Checks => _checks.AsReadOnly();

        protected ReceivingInspectionResult() { }

        /// <summary>
        /// ساخت نتیجه بازرسی + Stepها از چک‌لیست فعال
        /// </summary>
        public static ReceivingInspectionResult Create(
            long receiptId,
            long? receiptLineId,
            long? inventoryItemId,
            long storeId,
            IReadOnlyList<ReceivingInspectionItem> checklistItems,
            int? deadlineHours)
        {
            if (checklistItems == null || checklistItems.Count == 0)
                throw new InvalidOperationException("چک‌لیست بازرسی خالی است.");

            var ordered = checklistItems
                .Where(x => x != null && x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ToList();

            if (ordered.Count == 0)
                throw new InvalidOperationException("هیچ آیتم فعالی در چک‌لیست نیست.");

            var result = new ReceivingInspectionResult
            {
                ReceiptId = receiptId,
                ReceiptLineId = receiptLineId,
                InventoryItemId = inventoryItemId,
                StoreId = storeId,
                Status = InspectionStatus.Pending,
                TotalSteps = ordered.Count,
                CompletedSteps = 0,
                CurrentStepIndex = 0,
                IsOverridden = false
            };

            if (deadlineHours.HasValue && deadlineHours.Value > 0)
                result.DeadlineAt = DateTime.UtcNow.AddHours(deadlineHours.Value);

            for (var i = 0; i < ordered.Count; i++)
                result._checks.Add(new ReceivingInspectionCheck(0, ordered[i].Id, i));

            return result;
        }

        public void Start(long inspectorUserId)
        {
            if (Status != InspectionStatus.Pending && Status != InspectionStatus.InProgress)
                throw new InvalidOperationException("این بازرسی قابل شروع نیست.");

            InspectorUserId = inspectorUserId;
            Status = InspectionStatus.InProgress;
        }

        public void MoveToStep(int stepIndex)
        {
            if (stepIndex < 0 || stepIndex >= TotalSteps)
                throw new ArgumentOutOfRangeException(nameof(stepIndex));

            CurrentStepIndex = stepIndex;
        }

        public void AnswerCurrentStep(
            CheckOutcome outcome,
            string remark,
            long userId,
            bool allowNotApplicable)
        {
            AnswerStep(CurrentStepIndex, outcome, remark, userId, allowNotApplicable);
        }

        public void AnswerStep(
            int stepIndex,
            CheckOutcome outcome,
            string remark,
            long userId,
            bool allowNotApplicable)
        {
            EnsureNotFinalized();

            var check = _checks.FirstOrDefault(x => x.StepIndex == stepIndex)
                ?? throw new InvalidOperationException("Step یافت نشد.");

            var wasAnswered = check.IsAnswered;
            check.Answer(outcome, remark, userId, allowNotApplicable);

            if (!wasAnswered && check.IsAnswered)
                CompletedSteps++;

            var next = _checks
                .Where(x => !x.IsAnswered)
                .OrderBy(x => x.StepIndex)
                .FirstOrDefault();

            CurrentStepIndex = next?.StepIndex ?? stepIndex;
            Status = InspectionStatus.InProgress;
            InspectorUserId = userId;
        }

        /// <param name="mandatoryItemIds">شناسه آیتم‌های اجباری چک‌لیست</param>
        public bool CanComplete(IReadOnlyCollection<long> mandatoryItemIds)
        {
            mandatoryItemIds ??= Array.Empty<long>();
            return !_checks.Any(c =>
                mandatoryItemIds.Contains(c.InspectionItemId) && !c.IsAnswered);
        }

        public bool HasFailedChecks() =>
            _checks.Any(c => c.Outcome == CheckOutcome.Failed);

        public void Complete(
            InspectionStatus finalStatus,
            long inspectorUserId,
            string notes,
            IReadOnlyCollection<long> mandatoryItemIds)
        {
            if (finalStatus != InspectionStatus.Approved
                && finalStatus != InspectionStatus.Rejected
                && finalStatus != InspectionStatus.NeedsMoreInspection)
                throw new ArgumentException("وضعیت نهایی نامعتبر است.");

            if (finalStatus == InspectionStatus.Approved && !CanComplete(mandatoryItemIds))
                throw new InvalidOperationException("آیتم‌های اجباری باید پاسخ داده شوند.");

            if (finalStatus == InspectionStatus.Approved && HasFailedChecks())
                throw new InvalidOperationException(
                    "به‌خاطر وجود Failed نمی‌توان Approve کرد. Reject، NeedsMore یا بعداً Override.");

            Status = finalStatus;
            InspectorUserId = inspectorUserId;
            InspectedAt = DateTime.UtcNow;
            Notes = string.IsNullOrWhiteSpace(notes) ? null : notes.Trim();
        }

        public void Approve(long inspectorUserId, string notes, IReadOnlyCollection<long> mandatoryItemIds) =>
            Complete(InspectionStatus.Approved, inspectorUserId, notes, mandatoryItemIds);

        public void Reject(long inspectorUserId, string notes, IReadOnlyCollection<long> mandatoryItemIds) =>
            Complete(InspectionStatus.Rejected, inspectorUserId, notes, mandatoryItemIds);

        public void MarkNeedsMoreInspection(
            long inspectorUserId,
            string notes,
            IReadOnlyCollection<long> mandatoryItemIds) =>
            Complete(InspectionStatus.NeedsMoreInspection, inspectorUserId, notes, mandatoryItemIds);

        /// <summary>
        /// خروج از حالت معلق توسط مدیر سازمان/انبار.
        /// رد فنی در سوابق می‌ماند؛ چیدمان و حسابداری می‌تواند ادامه یابد.
        /// </summary>
        public void OverrideRejection(long authorizedUserId, string reason)
        {
            if (Status != InspectionStatus.Rejected
                && Status != InspectionStatus.NeedsMoreInspection)
                throw new InvalidOperationException(
                    "فقط وضعیت Rejected یا NeedsMoreInspection قابل Override است.");

            if (string.IsNullOrWhiteSpace(reason))
                throw new InvalidOperationException("دلیل Override الزامی است.");

            IsOverridden = true;
            OverrideByUserId = authorizedUserId;
            OverrideAt = DateTime.UtcNow;
            OverrideReason = reason.Trim();
            Status = InspectionStatus.Overridden;
        }

        /// <summary>
        /// آیا موجودی می‌تواند وارد چیدمان/فرایند بعدی شود؟
        /// </summary>
        public bool AllowsPlacement =>
            Status == InspectionStatus.Approved
            || Status == InspectionStatus.Overridden
            || Status == InspectionStatus.NotRequired;

        private void EnsureNotFinalized()
        {
            if (Status == InspectionStatus.Approved
                || Status == InspectionStatus.Rejected
                || Status == InspectionStatus.Overridden)
                throw new InvalidOperationException("بازرسی نهایی شده و قابل تغییر نیست.");
        }
    }
}