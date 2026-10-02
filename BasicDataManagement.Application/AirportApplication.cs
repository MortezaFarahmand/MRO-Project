using _0_Framework.Application;
using BasicDataManagement.Application.Contracts.Airport;
using BasicDataManagement.Domain.AirportAgg;
using System.Collections.Generic;

namespace BasicDataManagement.Application
{
    public class AirportApplication : IAirportApplication
    {
        private readonly IAirportRepository _airportRepository;

        public AirportApplication(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }


        public OperationResult Create(CreateAirport command)
        {
            var operation = new OperationResult();
            if(_airportRepository.Exists(x => x.Name == command.Name || x.Iata_code == command.Iata_code 
            || x.Icao_code == command.Icao_code))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            
            var airport = new Airport(command.Ident, command.Type, command.Name, command.Latitude_deg, 
                command.Longitude_deg, command.Elevation_ft, command.Continent, command.Iso_country, 
                command.Iso_region, command.Municipality, command.Scheduled_service, command.Icao_code,
                command.Iata_code, command.Gps_code, command.Local_code, command.Home_link, command.Wikipedia_link,
                command.Keywords);

            _airportRepository.Create(airport);
            _airportRepository.SaveChanges();
            return operation.Succeeded();
        }

       
        public OperationResult Edit(EditAirport command)
        {
            var operation = new OperationResult();
            var airport = _airportRepository.Get(command.Id);
            if (airport == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            if(_airportRepository.Exists(x => (x.Name == command.Name || x.Iata_code == command.Iata_code
            || x.Icao_code == command.Icao_code) && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            airport.Edit(command.Ident, command.Type, command.Name, command.Latitude_deg,
                command.Longitude_deg, command.Elevation_ft, command.Continent, command.Iso_country,
                command.Iso_region, command.Municipality, command.Scheduled_service, command.Icao_code,
                command.Iata_code, command.Gps_code, command.Local_code, command.Home_link, command.Wikipedia_link,
                command.Keywords);
            _airportRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<AirportViewModel> GetAirports()
        {
            return _airportRepository.GetAirports();
        }

        public EditAirport GetDetails(long id)
        {
            return _airportRepository.GetDetails(id);
        }

        public OperationResult Activate(long id)
        {
            var operation = new OperationResult();
            var airport = _airportRepository.Get(id);
            if (airport == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            airport.Active();
            _airportRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Deactivate(long id)
        {
            var operation = new OperationResult();
            var airport = _airportRepository.Get(id);
            if (airport == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            airport.Deactive();
            _airportRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<AirportViewModel> Search(AirportSearchModel searchModel)
        {
            return _airportRepository.Search(searchModel);
        }
    }
}
