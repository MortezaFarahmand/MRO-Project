using _0_Framework.Domain;
namespace PartManagement.Domain.Basic_dataAgg.ClassificationAgg
{
    public class Classification : EntityBase
    {
        public string Code { get; private set; }          // مثلاً ATA Chapter
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }

        protected Classification() { }

        public Classification(string code, string name, string description)
        {
            Code = code;
            Name = name;
            Description = description;
            IsActive = true;
        }

        public void Edit(string code, string name, string description)
        {
            Code = code;
            Name = name;
            Description = description;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}
