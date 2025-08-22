
using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace OrganizationManagement.Application.Contracts.Country
{
    public class CreatePerson
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
        public string Picture { get; set; }

        public string TailCode { get; set; }
    }
}
