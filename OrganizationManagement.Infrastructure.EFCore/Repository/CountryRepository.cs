
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using OrganizationManagement.Application.Contracts.Country;
using OrganizationManagement.Domain.CountryAgg;

namespace OrganizationManagement.Infrastructure.EFCore.Repository
{
    public class CountryRepository : RepositoryBase<long, Country> , IPersonRepository
    {
        private readonly OrganizationContext _context;

        public CountryRepository(OrganizationContext context) : base(context )
        {
            _context = context;
        }



        public EditPerson GetDetails(long id)
        {
            return _context.Countrys.Select(x => new EditPerson()
            {
                Id = x.Id,
                Name = x.Name,
                Alpha2Code =x.Alpha2Code,
                Alpha3Code = x.Alpha3Code,
                UNCode = x.UNCode,
                TailCode = x.TailCode,
                DialCode = x.DialCode,
                Picture =x.Picture
                
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CountryViewModel> GetCountrys()
        {
            return _context.Countrys.Select(x => new CountryViewModel() {

                Id = x.Id,
                Name = x.Name,
                //TailCode = x.TailCode

            }).ToList();
        }

        public List<CountryViewModel> Search(PersonSearchModel searchModel)
        {
            var query = _context.Countrys.Select(x => new CountryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Alpha2Code = x.Alpha2Code,
                Alpha3Code = x.Alpha3Code,
                UNCode = x.UNCode,
                TailCode = x.TailCode,
                DialCode = x.DialCode,
                CreationDate = x.CreationDate.ToString()

            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

           if (!string.IsNullOrWhiteSpace(searchModel.TailCode))
                query = query.Where(x => x.TailCode.Contains(searchModel.TailCode));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
