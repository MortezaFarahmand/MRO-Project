using _0_Framework.Application;
using OrganizationManagement.Application.Contracts.OrganizationDepartment;
using OrganizationManagement.Domain.OrganizationDepartmentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationManagement.Application
{
    public class OrganizationDepartmentApplication : IOrganizationDepartmentApplication
    {
        private readonly IOrganizationDepartmentRepository _organizationDepartmentRepository;

        public OrganizationDepartmentApplication(IOrganizationDepartmentRepository organizationDepartmentRepository)
        {
            _organizationDepartmentRepository = organizationDepartmentRepository;
        }


        public OperationResult Create(CreateOrganizationDepartment command)
        {
            var organization = new OperationResult();
            if(_organizationDepartmentRepository.Exists(x => x.Name == command.Name)) 
                return organization.Failed(ApplicationMessages.DuplicatedRecord);

            var OrganizationDepartment = new OrganizationDepartment(command.Name, command.Description,
                command.MetaDescription, command.Slug, command.ParentDepartmentId, command.OrganizationId);

            _organizationDepartmentRepository.Create(OrganizationDepartment);
            _organizationDepartmentRepository.SaveChanges();
            return organization.Succeeded();
        }

        public OperationResult Edit(EditOrganizationDepartment command)
        {
            var operation = new OperationResult();
            var organizationDepartment = _organizationDepartmentRepository.Get(command.Id);
            if (organizationDepartment == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_organizationDepartmentRepository.Exists(x => x.Name == command.Name 
                    && x.Id != command.Id && x.OrganizationId == command.OrganizationId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            organizationDepartment.Edit(command.Name, command.Description,
                command.MetaDescription, command.Slug, command.ParentDepartmentId, command.OrganizationId);

            _organizationDepartmentRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<OrganizationDepartmentViewModel> GetDepartmentsByOrganizationId(long id)
        {
            return _organizationDepartmentRepository.GetDepartmentsByOrganizationId(id);
        }

        public EditOrganizationDepartment GetDetails(long id)
        {
            return _organizationDepartmentRepository.GetDetails(id);
        }

        public List<OrganizationDepartmentViewModel> GetOrganizationDepartments()
        {
            return _organizationDepartmentRepository.GetOrganizationDepartments();
        }

        public List<OrganizationDepartmentViewModel> Search(OrganizationDepartmentSearchModel searchModel)
        {
            return _organizationDepartmentRepository.Search(searchModel);
        }
    }
}
