
//using _0_Framework.Application;
//using System.ComponentModel.DataAnnotations;

//namespace OrganizationManagement.Application.Contracts.Person
//{
//    public class CreatePerson
//    {
//        [Required(ErrorMessage = ValidationMessages.IsRequired)]
//        public string NameEn { get;  set; }
//        public string NameFa { get;  set; }
//        [Required(ErrorMessage = ValidationMessages.IsRequired)]
//        public string FamilyEn { get;  set; }
//        public string FamilyFa { get;  set; }
//        public string FatherName { get;  set; }
//        public string Birthday { get;  set; }
//        public string PassportNo { get;  set; }
//        public string NationalCode { get;  set; }
//        public string Attachment { get;  set; }
//        //[Required(ErrorMessage=ValidationMessages.IsRequired)]
//        //[FileExtentionLimitation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
//        //[MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
//        public string Picture { get;  set; }
//        public string PictureAlt { get;  set; }
//        public string IDCartNo { get;  set; }
//        public string Address { get;  set; }
//        public long OrganizationId { get;  set; }
//    }
//}
