using _01_MROQuery.Contracts.Common;
using _01_MROQuery.Contracts.Station;
using BasicDataManagement.Domain.AirportAgg;
using BasicDataManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_MROQuery.Query
{
    public class StationQuery : IStationQuery
    {
        private readonly BasicDataContext _context;

        public StationQuery(BasicDataContext context)
        {
            _context = context;
        }

        public List<StationQueryModel> GetAll()
        => _context.Airports.AsNoTracking()
            .OrderBy(x => x.Iso_country)
            .Select(Map)
            .ToList();

        public StationQueryModel GetById(long id)
        => _context.Airports.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(Map)
                .FirstOrDefault();

        public List<StationQueryModel> GetByCountryId(string isoCountry)
        => _context.Airports.AsNoTracking()
                .Where(x => x.Iso_country == isoCountry)
                .Select(Map)
                .ToList();


        public List<LookupItemDto> Search(string term, int take = 30)
        {
            term = (term ?? "").Trim();
            var q = _context.Airports.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
            {
                q = q.Where(x =>
                    (x.Name != null && x.Name.Contains(term)) ||
                    (x.Iata_code != null && x.Iata_code.Contains(term)) ||
                    (x.Ident != null && x.Ident.Contains(term)));
            }

            return q.OrderBy(x => x.Name)
                .Take(take <= 0 ? 30 : take)
                .Select(x => new LookupItemDto
                {
                    Id = x.Id,
                    Text = (x.Iata_code ?? x.Ident ?? "") + " - " + (x.Name ?? "")
                })
                .ToList();
        }

        public LookupItemDto GetLookupById(long id)
        {
            var x = _context.Airports.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (x == null) return null;
            return new LookupItemDto
            {
                Id = x.Id,
                Text = (x.Iata_code ?? x.Ident ?? "") + " - " + (x.Name ?? "")
            };
        }

        private static StationQueryModel Map(Airport x) => new()
        {
            Id = x.Id,
            Ident = x.Ident,
            Name = x.Name,
            Iata_code = x.Iata_code,
            Icao_code = x.Icao_code,
            Local_code = x.Local_code,
            IsActive = x.IsActive,
            Display = string.IsNullOrWhiteSpace(x.Name)
                ? x.Ident
                : $"{x.Icao_code} {x.Name}"
        };

        
    }

}
