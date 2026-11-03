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
    public class EditModel : PageModel
    {
        private readonly IPartAlternateApplication _application;
        private readonly IPartApplication _partApplication;

        [BindProperty]
        public EditPartAlternatesForPartCommand Command { get; set; } = new();

        public SelectList PartsSelectList { get; set; }
        public string PrimaryPartNumber { get; set; }

        public EditModel(
            IPartAlternateApplication application,
            IPartApplication partApplication)
        {
            _application = application;
            _partApplication = partApplication;
        }

        public IActionResult OnGet(long primaryPartId)
        {
            if (primaryPartId <= 0)
                return RedirectToPage("./Index");

            var list = _application.GetByPrimaryPart(primaryPartId) ?? new();
            var first = list.FirstOrDefault();

            Command = new EditPartAlternatesForPartCommand
            {
                PrimaryPartId = primaryPartId,
                AlternatePartIds = list.Select(x => x.AlternatePartId).Distinct().ToList(),
                RelationType = first?.RelationType ?? AlternateRelationType.TwoWay,
                AlternateCategory = first?.AlternateCategory,
                PreferenceLevel = first?.PreferenceLevel > 0 ? first.PreferenceLevel : 1,
                IsApproved = first?.IsApproved,
                EffectiveFrom = first?.EffectiveFrom,
                EffectiveTo = first?.EffectiveTo,
                Notes = first?.Notes
            };

            PrimaryPartNumber = first?.PrimaryPartNumber ?? primaryPartId.ToString();
            LoadParts();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (Command.PrimaryPartId <= 0)
            {
                ModelState.AddModelError("", "قطعه اصلی نامعتبر است.");
                LoadParts();
                return Page();
            }

            if (Command.AlternatePartIds == null)
                Command.AlternatePartIds = new List<long>();

            var result = _application.SyncForPrimary(Command);
            if (result.IsSucceeded)
            {
                TempData["Success"] = "روابط Alternate به‌روز شد.";
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError("", result.Message);
            var list = _application.GetByPrimaryPart(Command.PrimaryPartId) ?? new();
            PrimaryPartNumber = list.FirstOrDefault()?.PrimaryPartNumber ?? Command.PrimaryPartId.ToString();
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
                "Text",
                Command?.AlternatePartIds);   // selected values
        }
    }
}