namespace OrganizationManagement.Application.Contracts.OrganizationDepartment
{
    public class OrganizationDepartmentViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ParentDepartmentId { get; set; }
        public string ParentDepartment { get; set; }
        public long OrganizationId { get; set; }
        public string Organization { get; set; }
        public string CreationDate { get; set; }
    }
}
