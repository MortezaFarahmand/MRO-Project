using _0_Framework.Domain;
using PersonnelManagement.Application.Contracts.Person;
using System.Collections.Generic;

namespace PersonnelManagement.Domain.PersonAgg
{
    public interface IPersonRepository : IRepository<long, Person>
    {
        List<PersonViewModel> GetPerson();
        EditPerson GetDetails(long id);
        List<PersonViewModel> Search(PersonSearchModel searchModel);
    }
}
