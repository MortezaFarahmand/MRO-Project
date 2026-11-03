using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PartManagement.Application.Contracts.Part;
using PartManagement.Application.Contracts.Part.Commands;
using PartManagement.Application.Contracts.Part.DTOs;
using PartManagement.Application.Contracts.Part.Queries;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Part.Parts
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public SearchPartsQuery SearchModel { get; set; } = new();

        public List<PartDto> Parts { get; set; } = new();

        private readonly IPartApplication _partApplication;

        public IndexModel(IPartApplication partApplication)
        {
            _partApplication = partApplication;
        }

        public void OnGet()
        {
            Parts = _partApplication.Search(SearchModel) ?? new();
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreatePartCommand());
        }

        public JsonResult OnPostCreate(CreatePartCommand command)
        {
            var result = _partApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var part = _partApplication.GetById(id);
            if (part == null)
                return NotFound();

            var command = new EditPartCommand
            {
                Id = part.Id,
                Description = part.Description,
                Keyword = part.Keyword,
                Specification = part.Specification,
                Note = part.Note
            };

            return Partial("./Edit", command);
        }

        public JsonResult OnPostEdit(EditPartCommand command)
        {
            var result = _partApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetMakeGlobal(long id)
        {
            var result = _partApplication.MakeGlobal(new MakePartGlobalCommand { Id = id });
            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetSearchParts(string term)
        {
            var model = new SearchPartsQuery
            {
                PartNumber = string.IsNullOrWhiteSpace(term) ? null : term.Trim()
            };

            // اگر SearchModel شما نام/پراپرتی دیگری دارد همان را پر کنید
            var parts = _partApplication.Search(model)
                .Take(40)
                .Select(x => new
                {
                    id = x.Id,
                    text = x.PartNumber + (string.IsNullOrWhiteSpace(x.Description) ? "" : " | " + x.Description)
                });

            return new JsonResult(parts);
        }

        //اگر جستجو فقط با یک رشته است:
        public IActionResult OnGetSearchPart(string term)
        {
            var list = _partApplication.Search(new SearchPartsQuery { PartNumber = term })
                .Take(40)
                .Select(x => new { id = x.Id, text = x.PartNumber });
            return new JsonResult(list);
        }

    }
}