namespace PartManagement.Application.Contracts.Part.Commands
{
    public class AddPartManufacturerCommand
    {
        public long PartId { get; set; }
        public long ManufacturerId { get; set; }
        public bool IsPrimary { get; set; }
        public string Note { get; set; }
    }
}
