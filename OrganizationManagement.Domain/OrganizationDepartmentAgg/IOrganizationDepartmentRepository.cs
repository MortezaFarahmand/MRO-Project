using _0_Framework.Domain;
using OrganizationManagement.Application.Contracts.OrganizationDepartment;
using System.Collections.Generic;


namespace OrganizationManagement.Domain.OrganizationDepartmentAgg
{
    public interface IOrganizationDepartmentRepository : IRepository<long, OrganizationDepartment>
    {
        List<OrganizationDepartmentViewModel> GetOrganizationDepartments();
        EditOrganizationDepartment GetDetails(long id);
        List<OrganizationDepartmentViewModel> Search(OrganizationDepartmentSearchModel searchModel);
        List<OrganizationDepartmentViewModel> GetDepartmentsByOrganizationId(long id);
    }
}
