using BasicDataManagement.Application.Contracts.ATAChapter;
using BasicDataManagement.Application.Contracts.ATAChapter.DTOs;
using BasicDataManagement.Application.Contracts.ATAChapter.Services;
using BasicDataManagement.Domain.ATAChapterAgg;
//using BasicDataManagement.Domain.ATAChapterAgg.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasicDataManagement.Application
{
    public class AtaChapterApplication : IAtaChapterApplication
    {
        private readonly IAtaChapterRepository _repository;

        public AtaChapterApplication(IAtaChapterRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AtaChapterDto>> GetAllChaptersAsync(CancellationToken ct = default)
        {
            var chapters = await _repository.GetAllAsync(ct);

            return chapters.Select(MapToDto).ToList();
        }

        public async Task<AtaChapterDto?> GetByCodeAsync(string code, CancellationToken ct = default)
        {
            var chapter = await _repository.GetByCodeAsync(code, ct);
            return chapter is null ? null : MapToDto(chapter);
        }

        public async Task<List<AtaChapterDto>> GetByGroupAsync(AtaGroup group, CancellationToken ct = default)
        {
            var chapters = await _repository.GetByGroupAsync(group, ct);

            return chapters.Select(MapToDto).ToList();
        }

        public async Task<AtaChapterTreeDto?> GetChapterTreeAsync(string code, CancellationToken ct = default)
        {
            var chapter = await _repository.GetWithTreeAsync(code, ct);
            if (chapter is null) return null;

            return new AtaChapterTreeDto
            {
                Id = chapter.Id,
                Code = chapter.Code,
                TitleEn = chapter.TitleEn,
                TitleFa = chapter.TitleFa,
                Group = chapter.Group.ToString(),
                Sections = chapter.Sections
                    .OrderBy(s => s.Code)
                    .Select(s => new AtaSectionDto
                    {
                        Id = s.Id,
                        Code = s.Code,
                        FullCode = s.FullCode,
                        TitleEn = s.TitleEn,
                        TitleFa = s.TitleFa,
                        Subjects = s.Subjects
                            .OrderBy(sub => sub.Code)
                            .Select(sub => new AtaSubjectDto
                            {
                                Id = sub.Id,
                                Code = sub.Code,
                                FullCode = sub.FullCode,
                                TitleEn = sub.TitleEn,
                                TitleFa = sub.TitleFa,
                                WdmChapterSection = sub.WdmChapterSection,
                                IpcChapterSection = sub.IpcChapterSection
                            }).ToList()
                    }).ToList()
            };
        }

        // ── Mapping دستی (بدون وابستگی به Mapster) ──
        private static AtaChapterDto MapToDto(AtaChapter chapter) => new()
        {
            Id = chapter.Id,
            Code = chapter.Code,
            TitleEn = chapter.TitleEn,
            TitleFa = chapter.TitleFa,
            Group = chapter.Group.ToString(),
            Description = chapter.Description,
            IsActive = chapter.IsActive,
            DisplayOrder = chapter.DisplayOrder
        };

        public async Task<List<MaintenancePageBlockDto>> GetPageBlocksAsync(CancellationToken ct = default)
        {
            var blocks = await _repository.GetPageBlocksAsync(ct);

            return blocks.Select(b => new MaintenancePageBlockDto
            {
                Id = b.Id,
                Code = b.Code,
                TitleEn = b.TitleEn,
                TitleFa = b.TitleFa,
                StartPage = b.StartPage,
                EndPage = b.EndPage,
                DisplayOrder = b.DisplayOrder
            }).ToList();
        }
    }
}
