using System;

namespace _0_Framework.Infrastructure
{
    public class NeedsPermissionAttribute : Attribute
    {
        public long Permission { get; set; }

        public NeedsPermissionAttribute(long permission)
        {
            Permission = permission;
        }
    }
}
