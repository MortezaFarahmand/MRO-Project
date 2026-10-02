using _0_Framework.Domain;

namespace PartManagement.Domain.Basic_data.EquipmentTypeAgg
{
    public class EquipmentType : EntityBase
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }
        public string ColorCode { get; private set; }
        public string Picture { get; private set; }
        public bool IsActive { get; private set; }

        public EquipmentType() 
        { 

        }

        public EquipmentType(string name, string code, string description, string colorCode, string picture)
        {
            Name = name;
            Code = code;
            Description = description;
            ColorCode = colorCode;
            Picture = picture;
            IsActive = true;
        }

        public void Edit(string name, string code, string description, string colorCode, string picture)
        {
            Name = name;
            Code = code;
            Description = description;
            ColorCode = colorCode;
            Picture = picture;
        }

        public void Active()
        {
            IsActive = true;
        }

        public void Deactive()
        {
            IsActive = false;
        }
    }
}
