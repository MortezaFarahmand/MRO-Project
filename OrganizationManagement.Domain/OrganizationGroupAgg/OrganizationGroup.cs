using _0_Framework.Domain;
using BasicDatanManagement.Domain.OrganizationAgg;
using System.Collections.Generic;

namespace BasicDatanManagement.Domain.OrganizationGroupAgg
{
    public class OrganizationGroup : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Picture { get;  private set; }
        public string NameCode { get;  private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public List<Organization> Organizations { get; private set; }

        public OrganizationGroup()
        {
            Organizations = new List<Organization>();
        }

        public OrganizationGroup(string name, string description, string picture, string nameCode, 
            string pictureAlt, string pictureTitle, string keywords, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            NameCode = nameCode;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        public void Edit(string name, string description, string picture, string nameCode
            , string pictureAlt, string pictureTitle, string keywords, string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;
            NameCode = nameCode;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }
    }
}
