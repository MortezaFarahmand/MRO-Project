namespace OrganizationManagement.Application.Contracts.ApprovalAuthority
{
    public class ApprovalAuthorityViewModel
    {
        public long Id { get; set; }
        public string NameEn { get; set; }
        public string NameFa { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public string Code { get; set; }
        public string LogoPicture { get; set; }
        public long CountryId { get; set; }
        public string Country { get; set; }
    }
}
