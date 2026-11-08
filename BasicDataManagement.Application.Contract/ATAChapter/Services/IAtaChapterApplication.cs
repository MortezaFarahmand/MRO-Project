using BasicDataManagement.Application.Contracts.ATAChapter.DTOs;
////using BasicDataManagement.Domain.ATAChapterAgg.Enum;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BasicDataManagement.Application.Contracts.ATAChapter.Services;
public interface IAtaChapterApplication
{
    Task<List<AtaChapterDto>> GetAllChaptersAsync(CancellationToken ct = default);
    Task<AtaChapterDto?> GetByCodeAsync(string code, CancellationToken ct = default);
    Task<List<AtaChapterDto>> GetByGroupAsync(AtaGroup group, CancellationToken ct = default);
    Task<AtaChapterTreeDto?> GetChapterTreeAsync(string code, CancellationToken ct = default);

    Task<List<MaintenancePageBlockDto>> GetPageBlocksAsync(CancellationToken ct = default);
}