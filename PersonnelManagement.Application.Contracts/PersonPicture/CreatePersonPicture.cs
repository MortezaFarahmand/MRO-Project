using _0_Framework.Application;
using BasicDataManagement.Application.Contracts.PictureCategory;
using Microsoft.AspNetCore.Http;
using PersonnelManagement.Application.Contracts.Person;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.Application.Contracts.PersonPicture
{
    public class CreatePersonPicture
    {
        //[Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        [FileExtentionLimitation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Title { get; set; }

        public string Text { get; set; }
        public string Remark { get; set; }

        //public bool IsActive { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public long PictureCategoryId { get; set; }

        public List<PictureCategoryViewModel> PictureCategories { get; set; }
        public long PersonId { get; set; }
        public List<PersonViewModel> Persons { get; set; }

    }


}
