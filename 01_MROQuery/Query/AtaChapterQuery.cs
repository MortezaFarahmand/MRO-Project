using _01_MROQuery.Contracts.ATAChapter;
using BasicDataManagement.Domain.ATAChapterAgg;
using BasicDataManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_MROQuery.Query
{
    public class AtaChapterQuery : IAtaChapterQuery
    {
        private readonly BasicDataContext _context;

        public AtaChapterQuery(BasicDataContext context)
        {
            _context = context;
        }

        
    public List<AtaChapterQueryModel> GetAll()
        => _context.AtaChapters.AsNoTracking()
            .OrderBy(x => x.DisplayOrder)
            .Select(Map)
            .ToList();

        public AtaChapterQueryModel GetById(int id)
            => _context.AtaChapters.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(Map)
                .FirstOrDefault();

        public List<AtaChapterQueryModel> GetByIds(List<int> ids)
        {
            if (ids == null || ids.Count == 0) return new();
            return _context.AtaChapters.AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .Select(Map)
                .ToList();
        }

        private static AtaChapterQueryModel Map(AtaChapter x) => new()
        {
            Id = x.Id,
            Code = x.Code,
            TitleEn = x.TitleEn,
            TitleFa = x.TitleFa,
            Group = x.Group.ToString(),
            IsActive = x.IsActive,
            DisplayOrder = x.DisplayOrder
        };
    }
}
