using _0_Framework.Infrastructure;
using BasicDataManagement.Application.Contracts.City;
using BasicDataManagement.Domain.CityAgg;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BasicDataManagement.Infrastructure.EFCore.Repository
{
    public class CityRepository : RepositoryBase<long, City>, ICityRepository
    {
        private readonly BasicDataContext _context;
        public CityRepository(BasicDataContext context) : base(context)
        {
            _context = context;
        }


        public EditCity GetDetails(long id)
        {
            return _context.Citys.Select(x => new EditCity()
            {
                Id = x.Id,
                Name = x.Name,
                DialCode = x.DialCode,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                Keywords = x.Keywords,
                ProvinceId = x.ProvinceId

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CityViewModel> GetCitys()
        {
            return _context.Citys
                .Include(p => p.Province)
                   //.ThenInclude(c => c.Country)
                .Select(x => new CityViewModel()
            {
                Id= x.Id,
                Name = x.Name,
                //Province = x.Province.Name,
                //Countri = x.Countrii.Name
            }).ToList();
        }

        public List<CityViewModel> Search(CitySearchModel searchModel)
        {
            var query = _context.Citys
                .Include(x => x.Province)
                   .ThenInclude(c => c.Country)
                .Select(x => new CityViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                DialCode = x.DialCode,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                Keywords = x.Keywords,
                Province = x.Province.Name,
                ProvinceId = x.ProvinceId,
                    CountryId = x.Province.CountryId,
                    Country = x.Province.Country.Name
                });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if(searchModel.ProvinceId !=0)
                query = query.Where(x => x.ProvinceId == searchModel.ProvinceId);

            if (searchModel.CountryId != 0)
                query = query.Where(x => x.CountryId == searchModel.CountryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
