using _0_Framework.Domain;
using OrganizationManagement.Domain.OrganizationAgg;

namespace OrganizationManagement.Domain.OrganizationPictureAgg
{
    public class OrganizationPicture : EntityBase
    {
        public long OrganizationId  { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public bool IsRemoved { get; private set; }
        public Organization Organization { get; private set; }


        public OrganizationPicture(long organizationId, string picture, string pictureAlt, string pictureTitle)
        {
            OrganizationId = organizationId;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            IsRemoved = false;
        }

        public void Edit(long organizationId, string picture, string pictureAlt, string pictureTitle)
        {
            OrganizationId = organizationId;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
        }

        public void Remove()
        {
            IsRemoved=true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }


    }
}
