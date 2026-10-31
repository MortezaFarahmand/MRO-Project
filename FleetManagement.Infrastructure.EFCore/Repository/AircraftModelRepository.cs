using _0_Framework.Infrastructure;
using FleetManagement.Domain.Basic_dataAgg.AircraftModelAgg;

namespace FleetManagement.Infrastructure.EFCore.Repository
{
    public class AircraftModelRepository : RepositoryBase<long, AircraftModel>, IAircraftModelRepository
    {
        private readonly FleetContext _context;

        public AircraftModelRepository(FleetContext context) : base(context)
        {
            _context = context;
        }

        public AircraftModel GetByCode(string code)
        {
            return _context.AircraftModels
                .FirstOrDefault(x => x.Code == code.Trim().ToUpper());
        }

        public bool ExistsCode(string code)
        {
            return _context.AircraftModels
                .Any(x => x.Code == code.Trim().ToUpper());
        }

        public List<AircraftModel> GetByTypeId(long aircraftTypeId)
        {
            return _context.AircraftModels
                .Where(x => x.AircraftTypeId == aircraftTypeId)
                .OrderBy(x => x.Code)
                .ToList();
        }

        public List<AircraftModel> Search(string code, string name, long? typeId, string manufacturer, bool? isActive = null)
        {
            var query = _context.AircraftModels.AsQueryable();

            if (!string.IsNullOrWhiteSpace(code))
                query = query.Where(x => x.Code.Contains(code.Trim().ToUpper()));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name));

            if (typeId.HasValue)
                query = query.Where(x => x.AircraftTypeId == typeId.Value);

            if (!string.IsNullOrWhiteSpace(manufacturer))
                query = query.Where(x => x.Manufacturer.Contains(manufacturer));

            if (isActive.HasValue)
                query = query.Where(x => x.IsActive == isActive.Value);

            return query.OrderBy(x => x.Code).ToList();
        }
    }
}
