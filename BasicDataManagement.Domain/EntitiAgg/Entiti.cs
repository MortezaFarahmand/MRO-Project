using _0_Framework.Domain;

namespace BasicDataManagement.Domain.EntitiAgg
{
    public class Entiti : EntityBase
    {
        public string Name { get; private set; }
        public string Title { get; private set; }
        public bool IsActive { get; private set; }
        public string Remark { get; private set; }

        public Entiti(string name, string title, string remark)
        {
            Name = name;
            Title = title;
            Remark = remark;
            
            IsActive = true;
        }

        public void Edit(string name, string title, string remark, bool isActive)
        {
            Name = name;
            Title = title;
            Remark = remark;

            IsActive = isActive;
        }
    }
}
