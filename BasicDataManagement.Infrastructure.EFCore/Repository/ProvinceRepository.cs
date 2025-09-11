using _0_Framework.Infrastructure;
using BasicDataManagement.Application.Contracts.Province;
using BasicDataManagement.Domain.ProvinceAgg;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BasicDataManagement.Infrastructure.EFCore.Repository
{
    public class ProvinceRepository : RepositoryBase<long, Province>, IProvinceRepository
    {
        private readonly BasicDataContext _context;
        public ProvinceRepository(BasicDataContext context) : base(context)
        {
            _context = context;
        }


        public EditProvince GetDetails(long id)
        {
            return _context.Provinces.Select(x => new EditProvince()
            {
                Id = x.Id,
                Name = x.Name,
                DialCode = x.DialCode,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                Keywords = x.Keywords,
                CountryId = x.CountryId

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProvinceViewModel> GetProvinces()
        {
            return _context.Provinces.Select(x => new ProvinceViewModel()
            {
                Id= x.Id,
                Name = x.Name
            }).ToList();
        }

        public List<ProvinceViewModel> Search(ProvinceSearchModel searchModel)
        {
            var query = _context.Provinces
                .Include(x => x.Country)
                .Select(x => new ProvinceViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                DialCode = x.DialCode,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                Keywords = x.Keywords,
                Country = x.Country.Name,
                CountryId = x.CountryId
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if(searchModel.CountryId !=0)
                query = query.Where(x => x.CountryId == searchModel.CountryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
