using BasicDataManagement.Application.Contracts.ATAChapter.DTOs;
using BasicDataManagement.Application.Contracts.ATAChapter.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Administration.Pages.BasicData.AtaChapters
{
    public class TreeModel : PageModel
    {
        private readonly IAtaChapterApplication _application;
        public TreeModel(IAtaChapterApplication application) => _application = application;

        public AtaChapterTreeDto? Chapter { get; set; }

        public async Task<IActionResult> OnGetAsync(string code)
        {
            Chapter = await _application.GetChapterTreeAsync(code);
            if (Chapter is null) return NotFound();
            return Page();
        }
    }
}
