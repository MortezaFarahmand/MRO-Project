using _0_Framework.Application;
using BasicDataManagement.Application.Contracts.PictureCategory;
using PersonnelManagement.Application.Contracts.Person;
using PersonnelManagement.Application.Contracts.PersonGroup;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Application.Contracts.PersonPicture
{
    public class CreatePersonPicture
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Picture { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
        public string Remark { get; set; }
        //public bool IsActive { get; set; }
        public long PictureCategoryId { get; set; }
        public List<PictureCategoryViewModel> PictureCategories { get; set; }
        public long PersonId { get; set; }
        public List<PersonViewModel> Persons { get; set; }

    }


}
