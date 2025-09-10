using _0_Framework.Application;
using OrganizationManagement.Application.Contracts.ApprovalAuthority;
using OrganizationManagement.Domain.ApprovalAutorityAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationManagement.Application
{
    public class ApprovalAuthorityApplication : IApprovalAuthorityApplication
    {
        private readonly IApprovalAuthorityRepository _approvalAuthorityRepository;

        public ApprovalAuthorityApplication(IApprovalAuthorityRepository approvalAuthorityRepository)
        {
            _approvalAuthorityRepository = approvalAuthorityRepository;
        }



        public OperationResult Create(CreateApprovalAuthority command)
        {
            var operation = new OperationResult();
            if (_approvalAuthorityRepository.Exists(x => x.NameEn == command.NameEn))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var approvalAuthority = new ApprovalAuthority(command.NameEn, command.NameFa, command.Description,
                command.Address, command.WebSite, command.Remark, command.Code, command.LogoPicture, command.LogoPictureAlt,
                command.LogoPictureTitle, command.MetaDescription, slug, command.CanonicalAddress,
                command.Keywords, command.CountryId);
            
            _approvalAuthorityRepository.Create(approvalAuthority);
            _approvalAuthorityRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditApprovalAuthority command)
        {
            var operation = new OperationResult();
            var approvalAuthority = _approvalAuthorityRepository.Get(command.Id);
            if (approvalAuthority == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_approvalAuthorityRepository.Exists(x => x.NameEn == command.NameEn && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            approvalAuthority.Edit(command.NameEn, command.NameFa, command.Description,
                command.Address, command.WebSite, command.Remark, command.Code, command.LogoPicture, command.LogoPictureAlt,
                command.LogoPictureTitle, command.MetaDescription, slug, command.CanonicalAddress,
                command.Keywords, command.CountryId);

            _approvalAuthorityRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ApprovalAuthorityViewModel> GetApprovalAuthoritys()
        {
            return _approvalAuthorityRepository.GetApprovalAutoritys();
        }

        public EditApprovalAuthority GetDetails(long id)
        {
            return _approvalAuthorityRepository.GetDetails(id);
        }

        public List<ApprovalAuthorityViewModel> Search(ApprovalAuthoritySearchModel searchModel)
        {
            return _approvalAuthorityRepository.Search(searchModel);
        }
    }
}
