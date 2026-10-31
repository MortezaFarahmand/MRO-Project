namespace FleetManagement.Application.Contracts.Aircraft.Commands
{
    public class EditAircraftCommand
    {
        public long Id { get; set; }
        public string Registration { get; set; }
        public long? OwnerOrganizationId { get; set; }
        public long? OperatorOrganizationId { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string LineNumber { get; set; }
        public string VariableNumber { get; set; }
        public string Note { get; set; }
    }
}
