using _0_Framework.Infrastructure;
using BasicDataManagement.Application.Contracts.Airport;
using BasicDataManagement.Domain.AirportAgg;
using System.Collections.Generic;
using System.Linq;

namespace BasicDataManagement.Infrastructure.EFCore.Repository
{
    public class AirportRepository : RepositoryBase<long, Airport>, IAirportRepository
    {
        private readonly BasicDataContext _context;

        public AirportRepository(BasicDataContext context) : base(context) 
        {
            _context = context;
        }


        public List<AirportViewModel> GetAirports()
        {
            return _context.Airports.Select(x => new AirportViewModel()
            {

                Id = x.Id,
                Ident = x.Ident,
                Type = x.Type,
                Name = x.Name,
                Elevation_ft = x.Elevation_ft,
                Continent = x.Continent,
                Iso_country = x.Iso_country,
                Iso_region = x.Iso_region,
                Municipality = x.Municipality,
                Icao_code = x.Icao_code,
                Iata_code = x.Iata_code,
                Local_code = x.Local_code,
                Home_link = x.Home_link,
                CreationDate = x.CreationDate.ToString(),
                IsActive = x.IsActive

            }).ToList(); // جهت خلاصه کردن دیتا فرودگاههایی که دارای کد ایکائو هستند را فیلتر کردیم

        }

        public EditAirport GetDetails(long id)
        {
            return _context.Airports.Select(x => new EditAirport()
            {
                Id = x.Id,
                Ident = x.Ident,
                Type = x.Type,
                Name = x.Name,
                Latitude_deg = x.Latitude_deg,
                Longitude_deg = x.Longitude_deg,
                Elevation_ft = x.Elevation_ft,
                Continent = x.Continent,
                Iso_country = x.Iso_country,
                Iso_region = x.Iso_region,
                Municipality = x.Municipality,
                Scheduled_service = x.Scheduled_service,
                Icao_code = x.Icao_code,
                Iata_code = x.Iata_code,
                Gps_code = x.Gps_code,
                Local_code = x.Local_code,
                Home_link = x.Home_link,
                Wikipedia_link = x.Wikipedia_link

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<AirportViewModel> Search(AirportSearchModel searchModel)
        {
            var query = _context.Airports.Select(x => new AirportViewModel()
            {
                Id = x.Id,
                Type = x.Type,
                Name = x.Name,
                Elevation_ft = x.Elevation_ft,
                Continent = x.Continent,
                Iso_country = x.Iso_country,
                Iso_region = x.Iso_region,
                Municipality = x.Municipality,
                Icao_code = x.Icao_code,
                Iata_code = x.Iata_code,
                Local_code = x.Local_code,
                IsActive = x.IsActive
                

            });  

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Type))
                query = query.Where(x => x.Type.Contains(searchModel.Type));
            if (searchModel.Elevation_ft > 0 || searchModel.Elevation_ft <= 0)
                query = query.Where(x => x.Elevation_ft.Equals(searchModel.Elevation_ft));
            if (!string.IsNullOrWhiteSpace(searchModel.Continent))
                query = query.Where(x => x.Continent.Contains(searchModel.Continent)); 
            if (!string.IsNullOrWhiteSpace(searchModel.Iso_country))
                query = query.Where(x => x.Iso_country.Contains(searchModel.Iso_country)); 
            if (!string.IsNullOrWhiteSpace(searchModel.Iso_region))
                query = query.Where(x => x.Iso_region.Contains(searchModel.Iso_region)); 
            if (!string.IsNullOrWhiteSpace(searchModel.Municipality))
                query = query.Where(x => x.Municipality.Contains(searchModel.Municipality)); 
            if (!string.IsNullOrWhiteSpace(searchModel.Icao_code))
                query = query.Where(x => x.Icao_code.Contains(searchModel.Icao_code)); 
            if (!string.IsNullOrWhiteSpace(searchModel.Iata_code))
                query = query.Where(x => x.Iata_code.Contains(searchModel.Iata_code)); 
            if (!string.IsNullOrWhiteSpace(searchModel.Local_code))
                query = query.Where(x => x.Local_code.Contains(searchModel.Local_code));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
