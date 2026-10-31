using _0_Framework.Domain;
using PartManagement.Domain.PartAgg.Enum;

namespace PartManagement.Domain.PartAgg
{
    public class PartHierarchy : EntityBase
    {
        public long ParentPartId { get; private set; }
        public long ChildPartId { get; private set; }
        public decimal Quantity { get; private set; }
        public int Level { get; private set; }
        public PartRelationType RelationType { get; private set; }

        public Part ParentPart { get; private set; }
        public Part ChildPart { get; private set; }

        private PartHierarchy() { }

        public static PartHierarchy Create(
            long parentPartId,
            long childPartId,
            decimal quantity,
            int level,
            PartRelationType relationType)
        {
            if (quantity <= 0)
                throw new DomainException("تعداد باید بزرگ‌تر از صفر باشد");

            if (parentPartId == childPartId)
                throw new DomainException("قطعه نمی‌تواند والد یا فرزند خودش باشد");

            return new PartHierarchy
            {
                ParentPartId = parentPartId,
                ChildPartId = childPartId,
                Quantity = quantity,
                Level = level,
                RelationType = relationType
            };
        }

        public void UpdateQuantity(decimal newQuantity)
        {
            if (newQuantity <= 0)
                throw new DomainException("تعداد باید بزرگ‌تر از صفر باشد");

            Quantity = newQuantity;
        }

        public void ChangeRelationType(PartRelationType newRelationType)
        {
            RelationType = newRelationType;
        }
    }
}