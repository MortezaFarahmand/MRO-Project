using PartManagement.Domain.PartAgg.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartManagement.Application.Contracts.Part.DTOs
{
    public class PartAlternateDto
    {
        public long Id { get; set; }
        public long PrimaryPartId { get; set; }
        public string PrimaryPartNumber { get; set; }
        public string? PrimaryPartDescription { get; set; }
        public long AlternatePartId { get; set; }
        public string AlternatePartNumber { get; set; }
        public string? AlternatePartDescription { get; set; }
        public AlternateRelationType? RelationType { get; set; }
        public AlternateCategory? AlternateCategory { get; set; }
        public int? PreferenceLevel { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public string? Notes { get; set; }
        public string CreationDate { get; set; }
    }
}
