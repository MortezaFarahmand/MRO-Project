using FleetManagement.Domain.AircraftAgg.Enums;

namespace FleetManagement.Application.Contracts.Aircraft.Commands
{
    public class CreateAircraftCommand
    {
        public string Registration { get; set; }
        public string SerialNumber { get; set; }
        public long AircraftModelId { get; set; }
        public long? OwnerOrganizationId { get; set; }
        public long? OperatorOrganizationId { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string LineNumber { get; set; }
        public string VariableNumber { get; set; }
        public string Note { get; set; }
    }
}
