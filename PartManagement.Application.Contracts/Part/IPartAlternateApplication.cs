using _0_Framework.Application;
using PartManagement.Application.Contracts.Part.Commands;
using PartManagement.Application.Contracts.Part.DTOs;
using PartManagement.Application.Contracts.Part.Queries;

namespace PartManagement.Application.Contracts.Part
{

    public interface IPartAlternateApplication
    {
        OperationResult Create(CreatePartAlternateCommand command);
        OperationResult SyncForPrimary(EditPartAlternatesForPartCommand command);
        OperationResult Edit(EditPartAlternateCommand command);
        OperationResult Remove(long id);
        OperationResult Approve(long id);
        OperationResult Unapprove(long id);

        PartAlternateDto GetDetails(long id);
        List<PartAlternateDto> Search(PartAlternateSearchModel searchModel);
        List<PartAlternateDto> GetByPrimaryPart(long primaryPartId);
        /// <summary>Alternateهای تأییدشده و معتبر برای یک قطعه (مرتب بر اساس PreferenceLevel)</summary>
        List<PartAlternateDto> GetApprovedAlternates(long partId);
    }
}