namespace OrganizationManagement.Application.Contracts.OrganizationDepartment
{
    public class OrganizationDepartmentSearchModel
    {
        public string Name { get; set; }
        public long OrganizationId { get; set; }
        public long ParentDepartmentId { get; set; }
    }
}
