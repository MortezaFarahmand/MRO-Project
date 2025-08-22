using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OrganizationManagement.Application.Contracts.OrganizationAviationCode;
using OrganizationManagement.Domain.OrganizationAviationCodeAgg;
using System.Collections.Generic;
using System.Linq;

namespace OrganizationManagement.Infrastructure.EFCore.Repository
{
    public class OrganizationAviationCodeRepository : RepositoryBase<long, OrganizationAviationCode>, IOrganizationAviationCodeRepository
    {
        private readonly OrganizationContext _context;
        public OrganizationAviationCodeRepository(OrganizationContext context) : base(context)
        {
            _context = context;
        }


        public EditOrganizationAviationCode GetDetails(long id)
        {
            return _context.OrganizationAviationCodes.Select(x=>new EditOrganizationAviationCode
            {
                Id=x.Id,
                ICAO=x.ICAO,
                IATA=x.IATA,
                CallSign=x.CallSign,
                CivilAutority=x.CivilAutority,
                Comment=x.Comment,
                CountryId=x.CountryId,
                Description=x.Description,
            }).FirstOrDefault(x=>x.Id == id);
        }

        public List<OrganizationAviationCodeViewModel> GetOrganizationAviationCodes()
        {
            return _context.OrganizationAviationCodes.Select(x => new OrganizationAviationCodeViewModel
            {
                Id = x.Id,
                ICAO = x.ICAO,
                IsEnabled = x.IsEnabled
            }).ToList();
        }

        public List<OrganizationAviationCodeViewModel> Search(OrganizationAviationCodeSearchModel searchModel)
        {
           var query = _context.OrganizationAviationCodes.Include(x=>x.Countries).Select(x => new OrganizationAviationCodeViewModel
            {
                Id = x.Id,
                ICAO = x.ICAO,
                IATA = x.IATA,
                CivilAutority = x.CivilAutority,
                CountryId = x.CountryId,
                Country = x.Countries.Name,
                Description = x.Description,
                IsEnabled = x.IsEnabled
            });

            if (!string.IsNullOrWhiteSpace(searchModel.ICAO))
                query = query.Where(x => x.ICAO.Contains(searchModel.ICAO));

            if (!string.IsNullOrWhiteSpace(searchModel.CivilAutority))
                query = query.Where(x => x.CivilAutority.Contains(searchModel.CivilAutority));

            if (!string.IsNullOrWhiteSpace(searchModel.Description))
                query=query.Where(x => x.Description.Contains(searchModel.Description));

            if (searchModel.CountryId != 0)
                query = query.Where(x=> x.CountryId == searchModel.CountryId);

            return query.OrderByDescending(x => x.Id).ToList();

        }
    }
}
