using _0_Framework.Domain;
using PartManagement.Domain.PartAgg.Enum;

namespace PartManagement.Domain.PartAgg
{
    public interface IPartAlternateRepository : IRepository<long, PartAlternate>
    {
        bool Exists(long primaryPartId, long alternatePartId);

        /// <summary>Alternateهای تأییدشده و معتبر – فقط Entity</summary>
        List<PartAlternate> GetApprovedAlternates(long partId);

        /// <summary>در صورت نیاز برای Edit/Remove</summary>
        PartAlternate GetWithParts(long id); // Include در Infrastructure

        List<PartAlternate> Search(
        long? primaryPartId,
        long? alternatePartId,
        string partNumber,
        AlternateCategory? alternateCategory,
        bool? isApproved);
    }
}
