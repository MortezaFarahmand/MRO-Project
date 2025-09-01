
using _0_Framework.Application;
using System;
using System.ComponentModel.DataAnnotations;

namespace BasicDatanManagement.Application.Contracts.ApprovalAuthority
{
    public class CreateApprovalAuthority
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string NameEn { get;  set; }

        public string NameFa { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Description { get;  set; }

        public string Address { get;  set; }
        public string WebSite { get;  set; }
        public string Remark { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Code { get;  set; }

        public string LogoPicture { get;  set; }
        public string LogoPictureAlt { get;  set; }
        public string LogoPictureTitle { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string CanonicalAddress { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get;  set; }

        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        public long CountryId { get;  set; }

        //public List<CountryViewModel> Countries { get;  set; }

    }
}
