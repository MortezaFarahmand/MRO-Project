using _0_Framework.Domain;

namespace FleetManagement.Domain.Basic_dataAgg.AircraftTypeAgg
{
    public class AircraftType : EntityBase
    {
        public string Code { get; private set; }          // B737, A320, B407, ...
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }

        private AircraftType() { }

        public static AircraftType Create(string code, string name, string description = null)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("کد تایپ هواپیما الزامی است");

            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("نام تایپ هواپیما الزامی است");

            return new AircraftType
            {
                Code = code.Trim().ToUpperInvariant(),
                Name = name.Trim(),
                Description = description?.Trim(),
                IsActive = true
            };
        }

        public void Edit(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("نام تایپ هواپیما الزامی است");

            Name = name.Trim();
            Description = description?.Trim();
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}