
using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace BasicDataManagement.Application.Contracts.Country
{
    public class CreateCountry
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }

        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public string UNCode { get; set; }
        public string DialCode { get; set; }

        //[Required(ErrorMessage=ValidationMessages.IsRequired)]
        //[FileExtentionLimitation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
        //[MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        public long PictureId { get; set; }

        public string TailCode { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
    }
}
