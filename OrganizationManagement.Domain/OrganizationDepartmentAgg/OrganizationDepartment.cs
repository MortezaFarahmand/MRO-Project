using _0_Framework.Domain;
using OrganizationManagement.Domain.OrganizationAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NuGet.Common.NuGetEventSource;

namespace OrganizationManagement.Domain.OrganizationDepartmentAgg
{
    public class OrganizationDepartment : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public long ParentDepartmentId { get; private set; }
        public long OrganizationId { get; private set; }
        public Organization Organization { get; private set; }

        //public List<OrganizationPosition> OrganizationPositions { get; private set; }

        //public OrganizationDepartment()
        //{
        //    OrganizationPositions = new List<OrganizationPositions>();
        //}

        public OrganizationDepartment(string name, string description, string metaDescription, string slug, 
            long parentDepartmentId, long organizationId)
        {
            Name = name;
            Description = description;
            MetaDescription = metaDescription;
            Slug = slug;
            ParentDepartmentId = parentDepartmentId;
            OrganizationId = organizationId;
        }

        public void Edit(string name, string description, string metaDescription, string slug,
            long parentDepartmentId, long organizationId)
        {
            Name = name;
            Description = description;
            MetaDescription = metaDescription;
            Slug = slug;
            ParentDepartmentId = parentDepartmentId;
            OrganizationId = organizationId;
        }
    }
}
