using _0_Framework.Infrastructure;
using PersonnelManagement.Application.Contracts.PersonGroup;
using PersonnelManagement.Domain.PersonGroupAgg;
using PersonnelManagement.Infrastructure.EFCore;
using System.Collections.Generic;
using System.Linq;

namespace PersonnelManagement.Infrastructure.EFCore.Repository
{
    public class PersonGroupRepository : RepositoryBase<long, PersonGroup>, IPersonGroupRepository
    {
        private readonly PersonnelContext _context;

        public PersonGroupRepository(PersonnelContext context) : base(context)  
        {
            _context = context;
        }


        public EditPersonGroup GetDetails(long id)
        {
            return _context.PersonGroups.Select(x => new EditPersonGroup()
            {
                Id = x.Id,
                Description = x.Description,
                Remark = x.Remark,
                IsActive = x.IsActive

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<PersonGroupViewModel> GetPersonGroups()
        {
            return _context.PersonGroups.Select(x => new PersonGroupViewModel()
            {

                Id = x.Id,
                Description = x.Description,
                Remark = x.Remark,
                IsActive = x.IsActive

            }).ToList();
        }

    }
}
