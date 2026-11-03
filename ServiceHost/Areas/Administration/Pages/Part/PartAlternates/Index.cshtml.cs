using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PartManagement.Application.Contracts.Part;
using PartManagement.Application.Contracts.Part.DTOs;
using PartManagement.Application.Contracts.Part.Queries;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Part.PartAlternates
{
    public class IndexModel : PageModel
    {
        private readonly IPartAlternateApplication _application;

        public PartAlternateSearchModel SearchModel { get; set; }
        public List<PartAlternateDto> Alternates { get; set; }

        public IndexModel(IPartAlternateApplication application)
        {
            _application = application;
        }

        public void OnGet(PartAlternateSearchModel searchModel)
        {
            SearchModel = searchModel ?? new PartAlternateSearchModel();
            Alternates = _application.Search(SearchModel) ?? new List<PartAlternateDto>();
        }

        public IActionResult OnPostRemove(long id)
        {
            var result = _application.Remove(id);
            if (!result.IsSucceeded)
                TempData["Error"] = result.Message;
            else
                TempData["Success"] = "حذف شد.";
            return RedirectToPage();
        }

        public IActionResult OnPostApprove(long id)
        {
            var result = _application.Approve(id);
            if (!result.IsSucceeded)
                TempData["Error"] = result.Message;
            return RedirectToPage();
        }

        public IActionResult OnPostUnapprove(long id)
        {
            var result = _application.Unapprove(id);
            if (!result.IsSucceeded)
                TempData["Error"] = result.Message;
            return RedirectToPage();
        }
    }
}
