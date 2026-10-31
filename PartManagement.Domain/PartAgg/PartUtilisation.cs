using _0_Framework.Domain;
using PartManagement.Domain.Basic_dataAgg.PrimaryUtilisationAgg;

namespace PartManagement.Domain.PartAgg
{
    public class PartUtilisation : EntityBase
    {
        public long PartId { get; private set; }
        public long PrimaryUtilisationId { get; private set; }
        public bool IsPrimary { get; private set; }
        public string Note { get; private set; }
        // Navigation Properties
        public Part Part { get; private set; }
        public PrimaryUtilisation PrimaryUtilisation { get; private set; }   // ← این باید وجود داشته باشد

        private PartUtilisation() { }

        public static PartUtilisation Create(long partId, long primaryUtilisationId, bool isPrimary, string note)
        {
            return new PartUtilisation
            {
                PartId = partId,
                PrimaryUtilisationId = primaryUtilisationId,
                IsPrimary = isPrimary,
                Note = note
            };
        }

        public void SetAsPrimary() => IsPrimary = true;
        public void SetAsSecondary() => IsPrimary = false;
    }
}