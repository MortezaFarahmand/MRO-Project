using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace _0_Framework.Application
{
    public class FileExtentionLimitationAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _validExtentions;

        public FileExtentionLimitationAttribute(string[] validExtentions)
        {
            _validExtentions = validExtentions;
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null) return true;
            var fileExtention = Path.GetExtension(file.FileName);
            //if (!_validExtentions.Contains(fileExtention))
            //    return false;
            //return true;
            return _validExtentions.Contains(fileExtention);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            // Tell unobtrusive validation to parse this element
            context.Attributes.Add("data-val", "true");

            // Use the standard file extensions adapter name ("fileextensions")
            context.Attributes.Add("data-val-fileextensions", ErrorMessage ?? "Invalid file extension.");
            context.Attributes.Add("data-val-fileextensions-extensions", string.Join(",", _validExtentions.Select(e => e.TrimStart('.'))));
        }
    }
}
