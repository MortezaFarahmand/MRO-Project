using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartManagement.Application.Contracts.Stock.DTOs
{
    public class StationSimpleDto
    {
        public long Id { get; set; }
        public string Ident { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
        public string Icao_code { get; set; }
        public string Iata_code { get; set; }
        public string Local_code { get; set; }
        public bool IsActive { get; set; }
        public string Display { get; set; }
    }
}
