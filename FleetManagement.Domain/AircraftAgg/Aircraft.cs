using _0_Framework.Domain;
using FleetManagement.Domain.AircraftAgg.Enums;
//using FleetManagement.Domain.AircraftAgg.Events;
using FleetManagement.Domain.Basic_dataAgg.AircraftModelAgg;

namespace FleetManagement.Domain.AircraftAgg
{
    public class Aircraft : AggregateRoot
    {
        public string Registration { get; private set; }           // EP-XXX یا N407FC
        public string SerialNumber { get; private set; }           // MSN / Manufacturer Serial Number
        public long AircraftModelId { get; private set; }
        public long? OwnerOrganizationId { get; private set; }
        public long? OperatorOrganizationId { get; private set; }

        public AircraftStatus Status { get; private set; }

        public DateTime? ManufactureDate { get; private set; }
        public DateTime? DeliveryDate { get; private set; }
        public DateTime? RegistrationDate { get; private set; }

        // Life values (پایه – بعداً کامل‌تر می‌شود)
        public decimal TotalFlightHours { get; private set; }
        public int TotalCycles { get; private set; }
        public int TotalLandings { get; private set; }

        public string LineNumber { get; private set; }             // اختیاری
        public string VariableNumber { get; private set; }         // اختیاری
        public string Note { get; private set; }

        public bool IsActive { get; private set; }

        // Navigation
        public AircraftModel AircraftModel { get; private set; }

        private Aircraft() { }

        // ==================== Factory ====================
        public static Aircraft Create(
            string registration,
            string serialNumber,
            long aircraftModelId,
            long? ownerOrganizationId,
            long? operatorOrganizationId,
            DateTime? manufactureDate,
            DateTime? deliveryDate,
            long createdBy)
        {
            if (string.IsNullOrWhiteSpace(registration))
                throw new DomainException("رجیستری هواپیما الزامی است");

            if (string.IsNullOrWhiteSpace(serialNumber))
                throw new DomainException("شماره سریال (MSN) هواپیما الزامی است");

            var aircraft = new Aircraft
            {
                Registration = registration.Trim().ToUpperInvariant(),
                SerialNumber = serialNumber.Trim().ToUpperInvariant(),
                AircraftModelId = aircraftModelId,
                OwnerOrganizationId = ownerOrganizationId,
                OperatorOrganizationId = operatorOrganizationId,
                ManufactureDate = manufactureDate,
                DeliveryDate = deliveryDate,
                Status = AircraftStatus.Active,
                IsActive = true,
                TotalFlightHours = 0,
                TotalCycles = 0,
                TotalLandings = 0,
                CreatedBy = createdBy
            };

            return aircraft;
        }

        public void RaiseCreatedEvent()
        {
            AddDomainEvent(new AircraftCreatedEvent(Id, Registration, SerialNumber));
        }

        // ==================== Behaviors ====================

        public void Edit(
            string registration,
            long? ownerOrganizationId,
            long? operatorOrganizationId,
            DateTime? manufactureDate,
            DateTime? deliveryDate,
            DateTime? registrationDate,
            string lineNumber,
            string variableNumber,
            string note,
            long modifiedBy)
        {
            if (string.IsNullOrWhiteSpace(registration))
                throw new DomainException("رجیستری هواپیما الزامی است");

            Registration = registration.Trim().ToUpperInvariant();
            OwnerOrganizationId = ownerOrganizationId;
            OperatorOrganizationId = operatorOrganizationId;
            ManufactureDate = manufactureDate;
            DeliveryDate = deliveryDate;
            RegistrationDate = registrationDate;
            LineNumber = lineNumber?.Trim();
            VariableNumber = variableNumber?.Trim();
            Note = note?.Trim();

            ModifiedAt = DateTime.UtcNow;
            ModifiedBy = modifiedBy;
        }

        public void ChangeStatus(AircraftStatus newStatus, long modifiedBy)
        {
            if (Status == newStatus)
                return;

            Status = newStatus;
            ModifiedAt = DateTime.UtcNow;
            ModifiedBy = modifiedBy;

            AddDomainEvent(new AircraftStatusChangedEvent(Id, Registration, newStatus));
        }

        public void Activate(long modifiedBy)
        {
            IsActive = true;
            ModifiedAt = DateTime.UtcNow;
            ModifiedBy = modifiedBy;
        }

        public void Deactivate(long modifiedBy)
        {
            IsActive = false;
            ModifiedAt = DateTime.UtcNow;
            ModifiedBy = modifiedBy;
        }

        public void UpdateLifeValues(decimal flightHours, int cycles, int landings, long modifiedBy)
        {
            if (flightHours < 0 || cycles < 0 || landings < 0)
                throw new DomainException("مقادیر Life نمی‌توانند منفی باشند");

            TotalFlightHours = flightHours;
            TotalCycles = cycles;
            TotalLandings = landings;

            ModifiedAt = DateTime.UtcNow;
            ModifiedBy = modifiedBy;
        }

        public void AddFlightHours(decimal hours, int cycles = 0, int landings = 0)
        {
            if (hours < 0 || cycles < 0 || landings < 0)
                throw new DomainException("مقادیر نمی‌توانند منفی باشند");

            TotalFlightHours += hours;
            TotalCycles += cycles;
            TotalLandings += landings;
        }
    }
}