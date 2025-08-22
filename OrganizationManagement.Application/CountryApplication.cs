using OrganizationManagement.Application.Contracts.Country;
using System.Collections.Generic;
using _0_Framework.Application;
using OrganizationManagement.Domain.CountryAgg;

namespace OrganizationManagement.Application
{
    public class CountryApplication : IPersonApplication
    {
        private readonly IPersonRepository _countryRepository;

        public CountryApplication(IPersonRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }



        public OperationResult Create(CreatePerson command)
        {
            var operation = new OperationResult();
            if (_countryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var country = new Country(command.Name, command.Alpha2Code, command.Alpha3Code, command.UNCode,
                command.DialCode, command.Picture, command.TailCode);

            _countryRepository.Create(country);
            _countryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditPerson command)
        {
            var operation = new OperationResult();
            var country = _countryRepository.Get(command.Id);
            if (country == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if(_countryRepository.Exists(x=>x.Name==command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            country.Edit(command.Name, command.Alpha2Code, command.Alpha3Code, command.UNCode,
                command.DialCode, command.Picture, command.TailCode);

            _countryRepository.SaveChanges();
            return operation.Succeeded();

        }

        public EditPerson GetDetails(long id)
        {
            return _countryRepository.GetDetails(id);
        }

        public List<CountryViewModel> GetCountrys()
        {
            return _countryRepository.GetCountrys();
        }

        public List<CountryViewModel> Search(PersonSearchModel searchModel)
        {
            return _countryRepository.Search(searchModel);
        }
    }
}
