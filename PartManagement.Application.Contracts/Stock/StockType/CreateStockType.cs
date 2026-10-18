using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrganizationManagement.Application.Contracts.Organization;
using System.ComponentModel.DataAnnotations;

namespace PartManagement.Application.Contracts.Stock.StockType
{
    public class CreateStockType
    {
        //[Required(ErrorMessage = ValidationMessages.IsRequired)]
        //public string Name { get; set; }

        //[Required(ErrorMessage = ValidationMessages.IsRequired)]
        //public string Code { get; set; }

        //[Required(ErrorMessage = ValidationMessages.IsRequired)]
        //public long OrganizationId { get; set; }

        //public List<OrganizationViewModel> Organizations { get; set; } = new List<OrganizationViewModel>();

        //public string ColorCode { get; set; }

        //public string CreationDate { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Code { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public long? OrganizationId { get; set; }

        public string ColorCode { get; set; }

        //public string CreationDate { get; set; }

        // فقط داده‌های لازم برای Select – نه موجودیت Organization
        public List<SelectListItem> Organizations { get; set; } = new();

        // پرچم دسترسی
        public bool IsAdmin { get; set; }

        //نکته کلیدی: به‌جای List<OrganizationViewModel> از List<SelectListItem> استفاده کنید.
        //این‌طوری مدل آماده‌ی SelectList هست و ویو مستقیم به asp-items می‌دهدش. OrganizationViewModel و
        //Organization کاملا از ویو دور می‌شوند.
    }
}
