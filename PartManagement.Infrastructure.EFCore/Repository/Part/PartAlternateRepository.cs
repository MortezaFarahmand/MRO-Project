using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PartManagement.Application.Contracts.Part.DTOs;
using PartManagement.Application.Contracts.Part.Queries;
using PartManagement.Domain.PartAgg;
using PartManagement.Domain.PartAgg.Enum;
namespace PartManagement.Infrastructure.EFCore.Repository
{
    public class PartAlternateRepository : RepositoryBase<long, PartAlternate>, IPartAlternateRepository
    {
        private readonly PartContext _context;

        public PartAlternateRepository(PartContext context) : base(context)
        {
            _context = context;
        }

        public bool Exists(long primaryPartId, long alternatePartId)
        {
            return _context.PartAlternates
                .Any(x => x.PrimaryPartId == primaryPartId && x.AlternatePartId == alternatePartId);
        }

        public PartAlternateDto GetDetails(long id)
        {
            return _context.PartAlternates
                .Include(x => x.PrimaryPart)
                .Include(x => x.AlternatePart)
                .Where(x => x.Id == id)
                .Select(x => new PartAlternateDto
                {
                    Id = x.Id,
                    PrimaryPartId = x.PrimaryPartId,
                    PrimaryPartNumber = x.PrimaryPart.PartNumber,
                    PrimaryPartDescription = x.PrimaryPart.Description,
                    AlternatePartId = x.AlternatePartId,
                    AlternatePartNumber = x.AlternatePart.PartNumber,
                    AlternatePartDescription = x.AlternatePart.Description,
                    RelationType = x.RelationType,
                    AlternateCategory = x.AlternateCategory,
                    PreferenceLevel = x.PreferenceLevel,
                    IsApproved = x.IsApproved,
                    EffectiveFrom = x.EffectiveFrom,
                    EffectiveTo = x.EffectiveTo,
                    Notes = x.Notes,
                    CreationDate = x.CreationDate.ToString()
                })
                .FirstOrDefault();
        }

        public PartAlternate GetWithParts(long id)
        {
            return _context.PartAlternates
                .Include(x => x.PrimaryPart)
                .Include(x => x.AlternatePart)
                .FirstOrDefault(x => x.Id == id);
        }

        //فقط تأییدشده‌های قطعی//
        public List<PartAlternate> GetApprovedAlternates(long partId)
        {
            var now = DateTime.UtcNow;
            return _context.PartAlternates
                .Include(x => x.PrimaryPart)
                .Include(x => x.AlternatePart)
                    .Where(x => x.PrimaryPartId == partId &&
                        x.IsApproved.HasValue && x.IsApproved == true)
                    .Where(x =>
                        (!x.EffectiveFrom.HasValue || x.EffectiveFrom <= now) &&
                        (!x.EffectiveTo.HasValue || x.EffectiveTo >= now))
                .OrderBy(x => x.PreferenceLevel)
                .ThenBy(x => x.Id)
                .ToList();
        }

        public List<PartAlternate> Search(
            long? primaryPartId,
            long? alternatePartId,
            string partNumber,
            AlternateCategory? alternateCategory,
            bool? isApproved)
        {
            var query = _context.PartAlternates
                .Include(x => x.PrimaryPart)
                .Include(x => x.AlternatePart)
                .AsQueryable();

            if (primaryPartId.HasValue)
                query = query.Where(x => x.PrimaryPartId == primaryPartId.Value);

            if (alternatePartId.HasValue)
                query = query.Where(x => x.AlternatePartId == alternatePartId.Value);

            if (!string.IsNullOrWhiteSpace(partNumber))
            {
                var pn = partNumber.Trim();
                query = query.Where(x =>
                    x.PrimaryPart.PartNumber.Contains(pn) ||
                    x.AlternatePart.PartNumber.Contains(pn));
            }

            if (alternateCategory != null)
                query = query.Where(x => x.AlternateCategory == alternateCategory);

            if (isApproved.HasValue)
                query = query.Where(x => x.IsApproved == isApproved.Value);

            return query
                .OrderBy(x => x.PreferenceLevel)
                .ThenBy(x => x.Id)
                .ToList();
        }


    }
}