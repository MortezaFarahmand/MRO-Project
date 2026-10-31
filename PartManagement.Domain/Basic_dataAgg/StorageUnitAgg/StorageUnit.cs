using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartManagement.Domain.Basic_dataAgg.StorageUnitAgg
{
    public class StorageUnit : EntityBase
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }

        protected StorageUnit() { }

        public StorageUnit(string code, string name, string description)
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
