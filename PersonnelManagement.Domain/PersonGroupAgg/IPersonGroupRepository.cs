using System.Collections.Generic;
using _0_Framework.Domain;
using PersonnelManagement.Application.Contracts.PersonGroup;

namespace PersonnelManagement.Domain.PersonGroupAgg
{
    public interface IPersonGroupRepository : IRepository<long, PersonGroup>
    {
        List<PersonGroupViewModel> GetPersonGroups();
        EditPersonGroup GetDetails(long id);
    }
}
