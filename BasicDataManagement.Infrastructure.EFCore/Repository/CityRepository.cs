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
            return _context.Citys.Select(x => new CityViewModel()
            {
                Id= x.Id,
                Name = x.Name
            }).ToList();
        }

        public List<CityViewModel> Search(CitySearchModel searchModel)
        {
            var query = _context.Citys
                .Include(x => x.Province)
                .Select(x => new CityViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                DialCode = x.DialCode,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                Keywords = x.Keywords,
                Province = x.Province.Name,
                ProvinceId = x.ProvinceId
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if(searchModel.ProvinceId !=0)
                query = query.Where(x => x.ProvinceId == searchModel.ProvinceId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
