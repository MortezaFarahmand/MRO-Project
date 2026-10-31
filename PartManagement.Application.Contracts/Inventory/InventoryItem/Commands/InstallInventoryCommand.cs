namespace PartManagement.Application.Contracts.Inventory.InventoryItem.Commands
{
    public class InstallInventoryCommand
    {
        public long Id { get; set; }
        public long AircraftId { get; set; }
        public decimal? FlightHours { get; set; }
        public int? Cycles { get; set; }
    }
}
