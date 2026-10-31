using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetManagement.Application.Contracts.AircraftModel.Commands
{
    public class CreateAircraftModelCommand
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public long AircraftTypeId { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
    }
}
