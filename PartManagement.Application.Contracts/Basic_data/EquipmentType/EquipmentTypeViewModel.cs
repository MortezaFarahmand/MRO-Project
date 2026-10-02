namespace PartManagement.Application.Contracts.Basic_data.EquipmentType
{
    public class EquipmentTypeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ColorCode { get; set; }
        public string Picture { get; set; }
        public bool IsActive { get; set; }
        public string CreationDate { get; set; }
    }
}
