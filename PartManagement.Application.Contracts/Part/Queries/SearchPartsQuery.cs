namespace PartManagement.Application.Contracts.Part.Queries
{
    public class SearchPartsQuery
    {
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public bool? IsGlobal { get; set; }
        public long? OwnerOrganizationId { get; set; }
        public long? PartTypeId { get; set; }

        // برای صفحه‌بندی (اختیاری)
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}