using PartManagement.Domain.PartAgg.Enum;

namespace PartManagement.Application.Contracts.Part.Commands
{
    public class AddPartChildCommand
    {
        public long ParentPartId { get; set; }
        public long ChildPartId { get; set; }
        public decimal Quantity { get; set; }
        public int Level { get; set; }
        public PartRelationType RelationType { get; set; }
    }
}
