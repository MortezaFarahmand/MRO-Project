namespace FleetManagement.Application.Contracts.Aircraft.DTOs
{
    public class AircraftDto
    {
        public long Id { get; set; }
        public string Registration { get; set; }
        public string SerialNumber { get; set; }
        public long AircraftModelId { get; set; }
        public string AircraftModelName { get; set; }
        public long? OwnerOrganizationId { get; set; }
        public string OwnerOrganizationName { get; set; }
        public long? OperatorOrganizationId { get; set; }
        public string OperatorOrganizationName { get; set; }
        public string Status { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public decimal TotalFlightHours { get; set; }
        public int TotalCycles { get; set; }
        public int TotalLandings { get; set; }
        public string LineNumber { get; set; }
        public string VariableNumber { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
    }
}
