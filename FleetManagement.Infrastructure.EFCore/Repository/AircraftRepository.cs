using _0_Framework.Infrastructure;
using FleetManagement.Domain.AircraftAgg;
using FleetManagement.Domain.AircraftAgg.Enums;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Infrastructure.EFCore.Repository
{
    public class AircraftRepository : RepositoryBase<long, Aircraft>, IAircraftRepository
    {
        private readonly FleetContext _context;

        public AircraftRepository(FleetContext context) : base(context)
        {
            _context = context;
        }

        public Aircraft GetByRegistration(string registration)
        {
            return _context.Aircrafts
                .FirstOrDefault(x => x.Registration == registration.Trim().ToUpper());
        }

        public Aircraft GetBySerialNumber(string serialNumber)
        {
            return _context.Aircrafts
                .FirstOrDefault(x => x.SerialNumber == serialNumber.Trim().ToUpper());
        }

        public bool ExistsRegistration(string registration)
        {
            return _context.Aircrafts
                .Any(x => x.Registration == registration.Trim().ToUpper());
        }

        public bool ExistsSerialNumber(string serialNumber)
        {
            return _context.Aircrafts
                .Any(x => x.SerialNumber == serialNumber.Trim().ToUpper());
        }

        public List<Aircraft> Search(
            string registration,
            string serialNumber,
            long? modelId,
            long? organizationId,
            AircraftStatus? status)
        {
            var query = _context.Aircrafts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(registration))
                query = query.Where(x => x.Registration.Contains(registration.Trim().ToUpper()));

            if (!string.IsNullOrWhiteSpace(serialNumber))
                query = query.Where(x => x.SerialNumber.Contains(serialNumber.Trim().ToUpper()));

            if (modelId.HasValue)
                query = query.Where(x => x.AircraftModelId == modelId.Value);

            if (organizationId.HasValue)
                query = query.Where(x =>
                    x.OwnerOrganizationId == organizationId.Value ||
                    x.OperatorOrganizationId == organizationId.Value);

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            return query
                .OrderByDescending(x => x.Id)
                .ToList();
        }
    }
}