using BasicDataManagement.Application.Contracts.Country;
using System.Collections.Generic;
using _0_Framework.Application;
using BasicDataManagement.Domain.CountryAgg;

namespace BasicDatanManagement.Application
{
    public class CountryApplication : ICountryApplication
    {
        private readonly ICountryRepository _countryRepository;

        public CountryApplication(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }



        public OperationResult Create(CreateCountry command)
        {
            var operation = new OperationResult();
            if (_countryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var country = new Country(command.Name, command.Alpha2Code, command.Alpha3Code, command.UNCode,
                command.DialCode, command.PictureId, command.TailCode, command.MetaDescription, command.Slug);

            _countryRepository.Create(country);
            _countryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCountry command)
        {
            var operation = new OperationResult();
            var country = _countryRepository.Get(command.Id);
            if (country == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if(_countryRepository.Exists(x=>x.Name==command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            country.Edit(command.Name, command.Alpha2Code, command.Alpha3Code, command.UNCode,
                command.DialCode, command.PictureId, command.TailCode, command.MetaDescription, command.Slug);

            _countryRepository.SaveChanges();
            return operation.Succeeded();

        }

        public EditCountry GetDetails(long id)
        {
            return _countryRepository.GetDetails(id);
        }

        public List<CountryViewModel> GetCountrys()
        {
            return _countryRepository.GetCountrys();
        }

        public List<CountryViewModel> Search(CountrySearchModel searchModel)
        {
            return _countryRepository.Search(searchModel);
        }
    }
}
