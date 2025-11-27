using _0_Framework.Domain;
using PersonnelManagement.Domain.PersonAgg;

namespace PersonnelManagement.Domain.PersonPictureAgg
{
    public class PersonPicture : EntityBase
    {
        public string Picture { get; private set; }
        public string Title { get; private set; }
        public bool IsActive { get; private set; }
        public string Slug { get; private set; }
        public string Remark { get; private set; }
        public long PictureCategoryId { get; private set; }
        public long PersonId { get; private set; }
        public Person Person { get; private set; }


        public PersonPicture(string picture, string title, string slug, string remark, 
            long pictureCategoryId, long personId)
        {
            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            Title = title;
            Slug = slug;
            Remark = remark;
            IsActive = false;
            PictureCategoryId = pictureCategoryId;
            PersonId = personId;
        }

        public void Edit(string picture, string title, string slug, string remark,
            long pictureCategoryId, long personId)
        {
            if(!string.IsNullOrWhiteSpace(picture))
               Picture = picture;

            Title = title;
            Slug = slug;
            Remark = remark;
            IsActive = true;
            PictureCategoryId = pictureCategoryId;
            PersonId = personId;
        }

        public void Remove()
        {
            IsActive = false;
        }

        public void Restore()
        {
            IsActive = true;
        }


    }
}
