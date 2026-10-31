namespace PartManagement.Application.Contracts.Part.Commands
{
    public class AddPartOrganizationCommand
    {
        public long PartId { get; set; }
        public long OrganizationId { get; set; }
        public bool IsOwner { get; set; }
        public bool CanUse { get; set; } = true;
        public bool CanPurchase { get; set; }
        public bool CanSell { get; set; }
        public bool CanRepair { get; set; }
        public bool CanLease { get; set; }
        public bool CanShip { get; set; }
        public string Note { get; set; }
    }
}
