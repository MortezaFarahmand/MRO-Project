using BasicDataManagement.Application.Contracts.ATAChapter.DTOs;
using BasicDataManagement.Application.Contracts.ATAChapter.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceHost.Areas.Administration.Pages.BasicData.AtaChapters
{
    public class IndexModel : PageModel
    {
        private readonly IAtaChapterApplication _application;
        public IndexModel(IAtaChapterApplication application) => _application = application;

        public List<AtaChapterDto> Chapters { get; set; } = new();

        public async Task OnGetAsync()
        {
            Chapters = await _application.GetAllChaptersAsync();
        }
    }
}
