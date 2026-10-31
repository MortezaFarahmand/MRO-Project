using _0_Framework.Infrastructure;
using FleetManagement.Domain.Basic_dataAgg.AircraftTypeAgg;

namespace FleetManagement.Infrastructure.EFCore.Repository
{
    public class AircraftTypeRepository : RepositoryBase<long, AircraftType>, IAircraftTypeRepository
    {
        private readonly FleetContext _context;

        public AircraftTypeRepository(FleetContext context) : base(context)
        {
            _context = context;
        }

        public AircraftType GetByCode(string code)
        {
            return _context.AircraftTypes
                .FirstOrDefault(x => x.Code == code.Trim().ToUpper());
        }

        public bool ExistsCode(string code)
        {
            return _context.AircraftTypes
                .Any(x => x.Code == code.Trim().ToUpper());
        }

        public List<AircraftType> Search(string code, string name, bool? isActive = null)
        {
            var query = _context.AircraftTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(code))
                query = query.Where(x => x.Code.Contains(code.Trim().ToUpper()));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name));

            if (isActive.HasValue)
                query = query.Where(x => x.IsActive == isActive.Value);

            return query.OrderBy(x => x.Code).ToList();
        }
    }
}
