using _0_Framework.Domain;

namespace PersonnelManagement.Domain.PersonGroupAgg
{
    public class PersonGroup : EntityBase
    {
        public string Description { get; private set; }
        public string Remark { get; private set; }
        //public int PersonId { get; private set; }
        public bool IsActive { get; private set; }

        public PersonGroup(string description, string remark, bool isActive)
        {
            Description = description;
            Remark = remark;
            IsActive = isActive;
        }

        public void Edit(string description, string remark, bool isActive)
        {
            Description = description;
            Remark = remark;
            IsActive = isActive;
        }

    }


}
