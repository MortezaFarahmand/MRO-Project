namespace OrganizationManagement.Application.Contracts.OrganizationPosition
{
    public class OrganizationPositionSearchModel
    {
        public string Name { get; set; }
        public long OrganizationDepartmentId { get; set; }
        public long ParentPositionId { get; set; }
        public long OrganizationId { get; set; }
    }
}
