using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BasicDataManagement.Application.Contracts.Slide
{
    public class CreateSlide
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public IFormFile Picture { get; set; }

        
        public IFormFile PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }

        
        public string Heading { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Title { get; set; }

        
        public string Text { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string BtnText { get; set; }

        
        public string Link { get; set; }
    }
}