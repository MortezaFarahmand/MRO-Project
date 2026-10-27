namespace AccountManagement.Domain.RoleAgg
{
    public class Permission
    {
        public long Id { get; private set; }
        public long Code { get; private set; }
        public string Name { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }
        public long? OrganizationId { get; private set; }
        private Permission()
        {

        }

        public Permission(long code)
        {
            Code = code;
        }
        public Permission(long code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
