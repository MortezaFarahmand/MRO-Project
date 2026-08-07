namespace OrganizationManagement.Application.Contracts.OrganizationPosition
{
    public class OrganizationPositionViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ParentPositionId { get; set; }
        public string ParentPosition { get; set; }
        public long OrganizationDepartmentId { get; set; }
        public string OrganizationDepartment { get; set; }
        public string OrganizationName { get; set; }
        public string CreationDate { get; set; }
        public long OrganizationId { get; set; }
    }
}
