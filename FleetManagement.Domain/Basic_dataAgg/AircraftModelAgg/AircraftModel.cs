using _0_Framework.Domain;
using FleetManagement.Domain.Basic_dataAgg.AircraftTypeAgg;

namespace FleetManagement.Domain.Basic_dataAgg.AircraftModelAgg
{
    public class AircraftModel : EntityBase
    {
        public string Code { get; private set; }              // B737-800, A320-200, ...
        public string Name { get; private set; }
        public long AircraftTypeId { get; private set; }
        public string Manufacturer { get; private set; }      // Boeing, Airbus, Bell, ...
        public string Description { get; private set; }
        public bool IsActive { get; private set; }

        public AircraftType AircraftType { get; private set; }

        private AircraftModel() { }

        public static AircraftModel Create(
            string code,
            string name,
            long aircraftTypeId,
            string manufacturer,
            string description = null)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("کد مدل هواپیما الزامی است");

            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("نام مدل هواپیما الزامی است");

            if (string.IsNullOrWhiteSpace(manufacturer))
                throw new DomainException("سازنده هواپیما الزامی است");

            return new AircraftModel
            {
                Code = code.Trim().ToUpperInvariant(),
                Name = name.Trim(),
                AircraftTypeId = aircraftTypeId,
                Manufacturer = manufacturer.Trim(),
                Description = description?.Trim(),
                IsActive = true
            };
        }

        public void Edit(string name, string manufacturer, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("نام مدل هواپیما الزامی است");

            Name = name.Trim();
            Manufacturer = manufacturer?.Trim();
            Description = description?.Trim();
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}