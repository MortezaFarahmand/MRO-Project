using _01_MROQuery.Contracts.City;
using BasicDataManagement.Domain.CityAgg;
using BasicDataManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_MROQuery.Query
{
    public class CityQuery : ICityQuery
    {
        private readonly BasicDataContext _context;

        public CityQuery(BasicDataContext context)
        {
            _context = context;
        }

        public List<CityQueryModel> GetAll()
        => _context.Citys.AsNoTracking()
            .OrderBy(x => x.Province)
            .Select(Map)
            .ToList();

        public CityQueryModel GetById(long id)
        => _context.Citys.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(Map)
                .FirstOrDefault();

        public List<CityQueryModel> GetByProvinceId(long provinceId)
        => _context.Citys.AsNoTracking()
                .Where(x => x.ProvinceId == provinceId)
                .Select(Map)
                .ToList();

        private static CityQueryModel Map(City x) => new()
        {
            Id = x.Id,
            Name = x.Name,
            ProvinceName = x.Province?.ToString() ?? string.Empty,
            Display = string.IsNullOrWhiteSpace(x.Name)
                ? "0"
                : $"{x.Province} {x.Name}"
        };
    }
}
