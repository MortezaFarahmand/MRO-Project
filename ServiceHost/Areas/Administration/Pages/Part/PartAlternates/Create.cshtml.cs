using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PartManagement.Application.Contracts.Part;
using PartManagement.Application.Contracts.Part.Commands;
using PartManagement.Application.Contracts.Part.DTOs;
using PartManagement.Application.Contracts.Part.Queries;
using PartManagement.Domain.PartAgg.Enum;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Part.PartAlternates
{
    public class CreateModel : PageModel
        {
            private readonly IPartAlternateApplication _alternateApplication;
            private readonly IPartApplication _partApplication;

        [BindProperty]
        public CreatePartAlternateCommand Command { get; set; } = new();

            public SelectList PartsSelectList { get; set; }

            public CreateModel(
                IPartAlternateApplication alternateApplication,
                IPartApplication partApplication)
            {
                _alternateApplication = alternateApplication;
                _partApplication = partApplication;
            }

            public void OnGet()
            {
                Command = new CreatePartAlternateCommand
                {
                    RelationType = AlternateRelationType.TwoWay,
                    PreferenceLevel = 1,
                    IsApproved = true
                };
                LoadParts();
            }

            public IActionResult OnPost()
            {
                if (Command.PrimaryPartId <= 0)
                    ModelState.AddModelError("", "قطعه اصلی را انتخاب کنید.");

                if (Command.AlternatePartIds == null || !Command.AlternatePartIds.Any())
                    ModelState.AddModelError("", "حداقل یک جایگزین انتخاب کنید.");

                if (!ModelState.IsValid)
                {
                    LoadParts();
                    return Page();
                }

                var result = _alternateApplication.Create(Command);
                if (result.IsSucceeded)
                {
                    TempData["Success"] = "ثبت شد.";
                    return RedirectToPage("./Index");
                }

                ModelState.AddModelError("", result.Message);
                LoadParts();
                return Page();
            }

        private void LoadParts()
        {
            var parts = _partApplication.Search(new SearchPartsQuery()) ?? new List<PartDto>();
            PartsSelectList = new SelectList(
                parts.Select(x => new
                {
                    Id = x.Id,
                    Text = x.PartNumber + (string.IsNullOrWhiteSpace(x.Description) ? "" : " | " + x.Description)
                }),
                "Id",
                "Text");
        }
    }
    }
