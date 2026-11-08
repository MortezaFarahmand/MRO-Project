using _0_Framework.Infrastructure;
using BasicDataManagement.Application.Contracts.ATAChapter;
using BasicDataManagement.Domain.ATAChapterAgg;
//using BasicDataManagement.Domain.ATAChapterAgg.Enum;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasicDataManagement.Infrastructure.EFCore.Repository
{
    public class AtaChapterRepository : RepositoryBase<long, AtaChapter>, IAtaChapterRepository
    {
        private readonly BasicDataContext _context;

        public AtaChapterRepository(BasicDataContext context) : base(context) => _context = context;

        public async Task<List<AtaChapter>> GetAllAsync(CancellationToken ct = default)
            => await _context.AtaChapters
                             .AsNoTracking()
                             .OrderBy(x => x.DisplayOrder)
                             .ToListAsync(ct);

        public async Task<AtaChapter?> GetByCodeAsync(string code, CancellationToken ct = default)
            => await _context.AtaChapters
                             .AsNoTracking()
                             .FirstOrDefaultAsync(x => x.Code == code, ct);

        public async Task<List<AtaChapter>> GetByGroupAsync(AtaGroup group, CancellationToken ct = default)
            => await _context.AtaChapters
                             .AsNoTracking()
                             .Where(x => x.Group == group)
                             .OrderBy(x => x.DisplayOrder)
                             .ToListAsync(ct);

        
        public async Task<AtaChapter?> GetWithTreeAsync(string code, CancellationToken ct = default)
            => await _context.AtaChapters
                             .AsNoTracking()
                             .Include(x => x.Sections)
                                 .ThenInclude(s => s.Subjects)
                             .FirstOrDefaultAsync(x => x.Code == code, ct);

        public async Task SeedDefaultDataAsync(CancellationToken ct = default)
        {
            if (await _context.AtaChapters.AnyAsync(ct)) return;

            var chapters = AtaChapterSeedData.GetAll();
            await _context.AtaChapters.AddRangeAsync(chapters, ct);
            await _context.SaveChangesAsync(ct);
        }

        //Get MaintenancePageBlock List
        public async Task<List<MaintenancePageBlock>> GetPageBlocksAsync(CancellationToken ct = default)
        => await _context.MaintenancePageBlocks
                     .AsNoTracking()
                     .OrderBy(x => x.DisplayOrder)
                     .ToListAsync(ct);

    }
}
