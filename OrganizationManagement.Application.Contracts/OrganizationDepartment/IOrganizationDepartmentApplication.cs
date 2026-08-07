using _0_Framework.Application;
using System.Collections.Generic;

namespace OrganizationManagement.Application.Contracts.OrganizationDepartment
{
    public interface IOrganizationDepartmentApplication
    {
        OperationResult Create(CreateOrganizationDepartment command);
        OperationResult Edit(EditOrganizationDepartment command);
        EditOrganizationDepartment GetDetails(long id);
        List<OrganizationDepartmentViewModel> GetOrganizationDepartments();
        List<OrganizationDepartmentViewModel> Search(OrganizationDepartmentSearchModel searchModel);
        List<OrganizationDepartmentViewModel> GetDepartmentsByOrganizationId(long id);
    }
}
