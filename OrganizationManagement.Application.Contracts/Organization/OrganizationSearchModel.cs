
namespace OrganizationManagement.Application.Contracts.Organization
{
    public class OrganizationSearchModel
    {
        public string NameEn { get; set; }
        public string NameFa { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionFa { get; set; }
        public long OrganizationGroupId { get; set; }
        //public long OrganizationAviationCodeId { get; set; }
    }
}
