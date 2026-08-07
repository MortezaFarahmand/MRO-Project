using _0_Framework.Domain;
using OrganizationManagement.Domain.OrganizationDepartmentAgg;
using PersonnelManagement.Domain.PersonAgg;
using System.Collections.Generic;

namespace OrganizationManagement.Domain.OrganizationPositionAgg
{
    public class OrganizationPosition : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public bool Activate { get; private set; }
        public long ParentPositionId { get; private set; }
        public long OrganizationDepartmentId { get; private set; }
        public OrganizationDepartment OrganizationDepartment { get; private set; }
        //public List<Person> Persons { get; private set; }

        //public OrganizationPosition()
        //{
        //    Persons = new List<Person>();
        //}

        public OrganizationPosition(string name, string description, string metaDescription, string slug, 
              long parentPositionId, long organizationDepartmentId)
        {
            Name = name;
            Description = description;
            MetaDescription = metaDescription;
            Slug = slug;
            Activate = true;
            ParentPositionId = parentPositionId;
            OrganizationDepartmentId = organizationDepartmentId;
        }

        public void Edit(string name, string description, string metaDescription, string slug,
              long parentPositionId, long organizationDepartmentId)
        {
            Name = name;
            Description = description;
            MetaDescription = metaDescription;
            Slug = slug;
            ParentPositionId = parentPositionId;
            OrganizationDepartmentId = organizationDepartmentId;
        }

        public void Active()
        {
            Activate = true;
        }

        public void Deactive()
        {
            Activate = false;
        }
    }
}
