using FleetManagement.Domain.AircraftAgg;
using FleetManagement.Domain.AircraftAgg.Enums;

namespace FleetManagement.Application.Contracts.Aircraft.Queries
{
    public class SearchAircraftQuery
    {
        public string Registration { get; set; }
        public string SerialNumber { get; set; }
        public long? AircraftModelId { get; set; }
        public long? OrganizationId { get; set; }
        public AircraftStatus? Status { get; set; }

        // اختیاری – برای صفحه‌بندی
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}