using _0_Framework.Domain;
using PersonnelManagement.Domain.PersonAgg;
using System.Collections.Generic;

namespace PersonnelManagement.Domain.PersonGroupAgg
{
    public class PersonGroup : EntityBase
    {
        public string Description { get; private set; }
        public string Remark { get; private set; }
        public List<Person> Persons { get; private set; }
        public bool IsActive { get; private set; }


        public PersonGroup(string description, string remark, bool isActive)
        {
            Description = description;
            Remark = remark;
            IsActive = true;
        }

        public void Edit(string description, string remark, bool isActive)
        {
            Description = description;
            Remark = remark;
            IsActive = isActive;
        }

    }


}
