using _0_Framework.Application;
using OrganizationManagement.Application.Contracts.OrganizationPosition;
using OrganizationManagement.Domain.OrganizationPositionAgg;
using System.Collections.Generic;

namespace OrganizationManagement.Application
{
    public class OrganizationPositionApplication : IOrganizationPositionApplication
    {
        private readonly IOrganizationPositionRepository _organizationPositionRepository;

        public OrganizationPositionApplication(IOrganizationPositionRepository organizationPositionRepository)
        {
            _organizationPositionRepository = organizationPositionRepository;
        }


        public OperationResult Create(CreateOrganizationPosition command)
        {
            var organization = new OperationResult();
            if(_organizationPositionRepository.Exists(x => x.Name == command.Name &&
                       x.OrganizationDepartmentId == command.OrganizationDepartmentId)) 
                return organization.Failed(ApplicationMessages.DuplicatedRecord);

            var organizationPosition = new OrganizationPosition(command.Name, command.Description,
                command.MetaDescription, command.Slug,command.ParentPositionId, command.OrganizationDepartmentId);

            _organizationPositionRepository.Create(organizationPosition);
            _organizationPositionRepository.SaveChanges();
            return organization.Succeeded();
        }

        public OperationResult Edit(EditOrganizationPosition command)
        {
            var operation = new OperationResult();
            var organizationPosition = _organizationPositionRepository.Get(command.Id);
            if (organizationPosition == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_organizationPositionRepository.Exists(x => x.Name == command.Name 
                    && x.Id != command.Id && x.OrganizationDepartmentId == command.OrganizationDepartmentId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            organizationPosition.Edit(command.Name, command.Description,
                command.MetaDescription, command.Slug, command.ParentPositionId, command.OrganizationDepartmentId);

            _organizationPositionRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Deactive(long id)
        {
            var operation = new OperationResult();
            var organization = _organizationPositionRepository.Get(id);
            if (organization == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            organization.Deactive();

            _organizationPositionRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Active(long id)
        {
            var operation = new OperationResult();
            var organization = _organizationPositionRepository.Get(id);
            if (organization == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            organization.Active();

            _organizationPositionRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditOrganizationPosition GetDetails(long id)
        {
            return _organizationPositionRepository.GetDetails(id);
        }

        public List<OrganizationPositionViewModel> GetOrganizationPositions()
        {
            return _organizationPositionRepository.GetOrganizationPositions();
        }

        public List<OrganizationPositionViewModel> Search(OrganizationPositionSearchModel searchModel)
        {
            return _organizationPositionRepository.Search(searchModel);
        }
    }
}
