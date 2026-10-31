using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartManagement.Application.Contracts.Part.Commands
{
    public class EditPartCommand
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string Specification { get; set; }
        public string Note { get; set; }
    }
}
