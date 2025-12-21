using _0_Framework.Domain;
using BasicDataManagement.Domain.PictureCategoryAgg;
using BasicDataManagement.Domain.ProvinceAgg;
using PersonnelManagement.Domain.PersonAgg;
using System.Collections.Generic;

namespace PersonnelManagement.Domain.PersonPictureAgg
{
    public class PersonPicture : EntityBase
    {
        public string Picture { get; private set; }
        public string Title { get; private set; }
        public bool IsActive { get; private set; }
        public string Text { get; private set; }
        public string Remark { get; private set; }
        public long PictureCategoryId { get; private set; }
        public long PersonId { get; private set; }
        public Person Person { get; private set; }
        public List<PictureCategory> PictureCategories { get; private set; }
        public PersonPicture()
        {
            PictureCategories = new List<PictureCategory>();
        }


        public PersonPicture(string picture, string title, string text, string remark, 
            long pictureCategoryId, long personId)
        {
            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            Title = title;
            Text = text;
            Remark = remark;
            IsActive = true;
            PictureCategoryId = pictureCategoryId;
            PersonId = personId;
        }

        public void Edit(string picture, string title, string text, string remark, 
            long pictureCategoryId, long personId)
        {
            if(!string.IsNullOrWhiteSpace(picture))
               Picture = picture;

            Title = title;
            Text = text;
            Remark = remark;
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
