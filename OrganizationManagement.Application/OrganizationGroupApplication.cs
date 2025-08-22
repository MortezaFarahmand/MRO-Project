using OrganizationManagement.Application.Contracts.OrganizationGroup;
using System.Collections.Generic;
using _0_Framework.Application;
using OrganizationManagement.Domain.OrganizationGroupAgg;

namespace OrganizationManagement.Application
{
    public class OrganizationGroupApplication : IOrganizationGroupApplication
    {
        private readonly IOrganizationGroupRepository _organizationGroupRepository;

        public OrganizationGroupApplication(IOrganizationGroupRepository organizationGroupRepository)
        {
            _organizationGroupRepository = organizationGroupRepository;
        }



        public OperationResult Create(CreateOrganizationGroup command)
        {
            var operation = new OperationResult();
            if (_organizationGroupRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var organizationGroup = new OrganizationGroup(command.Name, command.Description, command.Picture, command.NameCode,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, command.Slug);

            _organizationGroupRepository.Create(organizationGroup);
            _organizationGroupRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditOrganizationGroup command)
        {
            var operation = new OperationResult();
            var organizationGroup = _organizationGroupRepository.Get(command.Id);
            if (organizationGroup == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_organizationGroupRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            organizationGroup.Edit(command.Name, command.Description, command.Picture, command.NameCode,
                command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, command.Slug);

            _organizationGroupRepository.SaveChanges();
            return operation.Succeeded();

        }


        public EditOrganizationGroup GetDetails(long id)
        {
            return _organizationGroupRepository.GetDetails(id);
        }

        public List<OrganizationGroupViewModel> GetOrganizationGroups()
        {
            return _organizationGroupRepository.GetOrganizationGroups();
        }

        public List<OrganizationGroupViewModel> Search(OrganizationGroupSearchModel searchModel)
        {
            return _organizationGroupRepository.Search(searchModel);
        }
    }
}
