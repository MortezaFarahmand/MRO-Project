using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;


namespace OrganizationManagement.Application.Contracts.OrganizationGroup
{
    public class CreateOrganizationGroup
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Description { get; set; }

        //[Required(ErrorMessage=ValidationMessages.IsRequired)]
        //[FileExtentionLimitation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        //[MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        public string Picture { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string NameCode { get; set; }

        public string PictureAlt { get; set; }

        public string PictureTitle { get; set; }

        //[Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get; set; }

        //[Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get; set; }

        //[Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get; set; }
    }
}
