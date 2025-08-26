using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Application.Contracts.PersonGroup
{
    public class PersonGroupViewModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public bool IsActive    { get; set; }
    }
}
