using _0_Framework.Application;
using OrganizationManagement.Application.Contracts.Organization;
using OrganizationManagement.Domain.OrganizationAgg;
using System.Collections.Generic;

namespace OrganizationManagement.Application
{
    public class OrganizationApplication : IOrganizationApplication
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationApplication(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }



        public OperationResult Create(CreateOrganization command)
        {
            var operation = new OperationResult();
            if (_organizationRepository.Exists(x => x.NameEn == command.NameEn || x.NameFa == command.NameFa))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var formedDate = command.FormedDate.ToGeorgianDateTimeEn();
            var organization = new Organization(command.NameEn, command.NameFa, command.DescriptionEn, command.DescriptionFa, command.AddressEn,
                command.AddressFa, command.Tel1, command.Tel2, command.Fax, command.WebSite, command.SocialAddress1,
                command.SocialAddress2, command.MailBox, command.Note, command.Remark, command.OrganizationAviationCodeId,
                command.CompanyRegisterNo, formedDate, command.ParentId, command.LogoPicture, command.LogoPictureAlt,
                command.LogoPictureTitle, command.MetaDescription, slug, command.CanonicalAddress, command.Keywords,
                command.OrganizationGroupId );

            _organizationRepository.Create(organization);
            _organizationRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditOrganization command)
        {
            var operation = new OperationResult();
            var organization = _organizationRepository.Get(command.Id);
            if (organization == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_organizationRepository.Exists(x => (x.NameEn == command.NameEn || x.NameFa == command.NameFa) && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var formedDate = command.FormedDate.ToGeorgianDateTimeEn();
            organization.Edit(command.NameEn, command.NameFa, command.DescriptionEn, command.DescriptionFa, command.AddressEn,
                command.AddressFa, command.Tel1, command.Tel2, command.Fax, command.WebSite, command.SocialAddress1,
                command.SocialAddress2, command.MailBox, command.Note, command.Remark, command.OrganizationAviationCodeId,
                command.CompanyRegisterNo, formedDate, command.ParentId, command.LogoPicture, command.LogoPictureAlt,
                command.LogoPictureTitle, command.MetaDescription, slug, command.CanonicalAddress, command.Keywords,
                command.OrganizationGroupId);
            _organizationRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditOrganization GetDetails(long id)
        {
            return _organizationRepository.GetDetails(id);
        }

        public OperationResult NotActive(long id)
        {
            var operation = new OperationResult();
            var organization = _organizationRepository.Get(id);
            if (organization == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            organization.NotActive();

            _organizationRepository.SaveChanges();
            return operation.Succeeded();
        }
        
        public OperationResult Active(long id)
        {
            var operation = new OperationResult();
            var organization = _organizationRepository.Get(id);
            if (organization == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            organization.Active();

            _organizationRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<OrganizationViewModel> Search(OrganizationSearchModel searchModel)
        {
            return _organizationRepository.Search(searchModel);
        }

        public List<OrganizationViewModel> GetOrganizations()
        {
            return _organizationRepository.GetOrganizations();
        }
    }
}
