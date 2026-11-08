using _0_Framework.Domain;
//using BasicDataManagement.Domain.ATAChapterAgg.Enum;
using BasicDataManagement.Application.Contracts.ATAChapter;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasicDataManagement.Domain.ATAChapterAgg
{
    public interface IAtaChapterRepository : IRepository<long, AtaChapter>
    {
        Task<List<AtaChapter>> GetAllAsync(CancellationToken ct = default);
        Task<AtaChapter?> GetByCodeAsync(string code, CancellationToken ct = default);
        Task<List<AtaChapter>> GetByGroupAsync(AtaGroup group, CancellationToken ct = default);
        Task<AtaChapter?> GetWithTreeAsync(string code, CancellationToken ct = default);
        Task SeedDefaultDataAsync(CancellationToken ct = default);

        Task<List<MaintenancePageBlock>> GetPageBlocksAsync(CancellationToken ct = default);
    }
}
