using _0_Framework.Domain;

namespace PartManagement.Domain.InventoryAgg.ReceivingInspectionAgg
{
    public class ReceivingInspectionItem : EntityBase
    {
        public string Code { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string HelpText { get; private set; }
        public int DisplayOrder { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsMandatory { get; private set; }
        public bool AllowNotApplicable { get; private set; }
        public string ApplicableKeyword { get; private set; }
        public long? StockTypeId { get; private set; }

        protected ReceivingInspectionItem() { }

        public ReceivingInspectionItem(
            string code,
            string title,
            string description,
            string helpText,
            int displayOrder,
            bool isMandatory,
            bool allowNotApplicable,
            string applicableKeyword = null,
            long? stockTypeId = null)
        {
            Code = code?.Trim();
            Title = title?.Trim();
            Description = description;
            HelpText = helpText;
            DisplayOrder = displayOrder;
            IsMandatory = isMandatory;
            AllowNotApplicable = allowNotApplicable;
            ApplicableKeyword = string.IsNullOrWhiteSpace(applicableKeyword) ? null : applicableKeyword.Trim();
            StockTypeId = stockTypeId;
            IsActive = true;
        }

        public void Edit(
            string title,
            string description,
            string helpText,
            int displayOrder,
            bool isMandatory,
            bool allowNotApplicable,
            string applicableKeyword,
            long? stockTypeId)
        {
            Title = title?.Trim();
            Description = description;
            HelpText = helpText;
            DisplayOrder = displayOrder;
            IsMandatory = isMandatory;
            AllowNotApplicable = allowNotApplicable;
            ApplicableKeyword = string.IsNullOrWhiteSpace(applicableKeyword) ? null : applicableKeyword.Trim();
            StockTypeId = stockTypeId;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}