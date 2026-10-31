using _0_Framework.Domain;

namespace PartManagement.Domain.PartAgg
{
    public class PartManufacturer : EntityBase
    {
        public long PartId { get; private set; }
        public long ManufacturerId { get; private set; }
        public bool IsPrimary { get; private set; }
        public string Note { get; private set; }

        private PartManufacturer() { }

        public static PartManufacturer Create(long partId, long manufacturerId, bool isPrimary, string note)
        {
            return new PartManufacturer
            {
                PartId = partId,
                ManufacturerId = manufacturerId,
                IsPrimary = isPrimary,
                Note = note
            };
        }

        public void SetAsPrimary() => IsPrimary = true;
        public void SetAsSecondary() => IsPrimary = false;
    }
}