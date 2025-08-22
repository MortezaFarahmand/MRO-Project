using _0_Framework.Domain;

namespace PersonnelManagement.Domain.PersonGroupAgg
{
    public class PersonGroup : EntityBase
    {
        public string Description { get; private set; }
        public string Remark { get; private set; }
        //public int PersonId { get; private set; }
        public bool IsActive { get; private set; }

        public PersonGroup(string descriotion, string remark)
        {
            Description = descriotion;
            Remark = remark;
        }

        public void Edit(string descriotion, string remark)
        {
            Description = descriotion;
            Remark = remark;
        }
    }


}
