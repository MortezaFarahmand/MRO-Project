
using _0_Framework.Infrastructure;
using BasicDataManagement.Application.Contracts.Country;
using BasicDataManagement.Domain.CountryAgg;
using System.Collections.Generic;
using System.Linq;

namespace BasicDataManagement.Infrastructure.EFCore.Repository
{
    public class CountryRepository : RepositoryBase<long, Country> , ICountryRepository
    {
        private readonly BasicDataContext _context;
        public CountryRepository(BasicDataContext context) : base(context)
        {
            _context = context;
        }


        public EditCountry GetDetails(long id)
        {
            return _context.Countrys.Select(x => new EditCountry()
            {
                Id = x.Id,
                Name = x.Name,
                Alpha2Code =x.Alpha2Code,
                Alpha3Code = x.Alpha3Code,
                UNCode = x.UNCode,
                TailCode = x.TailCode,
                DialCode = x.DialCode,
                PictureId =x.PictureId,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CountryViewModel> GetCountrys()
        {
            return _context.Countrys.Select(x => new CountryViewModel() 
            {

                Id = x.Id,
                Name = x.Name,
                TailCode = x.TailCode,
                Alpha2Code = x.Alpha2Code,
                Alpha3Code = x.Alpha3Code,
                UNCode = x.UNCode,
                DialCode= x.DialCode,
                PictureId=x.PictureId,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                CreationDate = x.CreationDate.ToString()

            }).ToList();
        }

        public List<CountryViewModel> Search(CountrySearchModel searchModel)
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
                PictureId=x.PictureId,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,

            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

           if (!string.IsNullOrWhiteSpace(searchModel.TailCode))
                query = query.Where(x => x.TailCode.Contains(searchModel.TailCode));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
