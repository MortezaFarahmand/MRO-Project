using _01_MROQuery.Contracts.AircraftModel;
using FleetManagement.Domain.Basic_dataAgg.AircraftModelAgg;
using FleetManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_MROQuery.Query
{
    public class AircraftModelQuery : IAircraftModelQuery
    {
        private readonly FleetContext _context;

        public AircraftModelQuery(FleetContext context)
        {
            _context = context;
        }

        public List<AircraftModelQueryModel> GetAll()
        => _context.AircraftModels.AsNoTracking()
            .OrderBy(x => x.AircraftType)
            .Select(Map)
            .ToList();

        public AircraftModelQueryModel GetById(long id)
        => _context.AircraftModels.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(Map)
                .FirstOrDefault();

        public List<AircraftModelQueryModel> GetByTypeId(long aircraftTypeId)
        => _context.AircraftModels.AsNoTracking()
                .Where(x => x.AircraftTypeId == aircraftTypeId)
                .Select(Map)
                .ToList();

        private static AircraftModelQueryModel Map(AircraftModel x) => new()
        {
            Id = x.Id,
            Code = x.Code,
            Name = x.Name,
            AircraftTypeName = x.AircraftType?.ToString() ?? string.Empty,
            IsActive = x.IsActive,
            Display = string.IsNullOrWhiteSpace(x.Code)
                ? "0"
                : $"{x.AircraftType} {x.Code}"
        };
    }
}