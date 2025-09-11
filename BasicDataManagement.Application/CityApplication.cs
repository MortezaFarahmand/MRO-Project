using _0_Framework.Application;
using BasicDataManagement.Application.Contracts.City;
using BasicDataManagement.Domain.CityAgg;
using System.Collections.Generic;

namespace BasicDataManagement.Application
{
    public class CityApplication : ICityApplication
    {
        private readonly ICityRepository _cityRepository;

        public CityApplication(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }


        public OperationResult Create(CreateCity command)
        {
            var operation = new OperationResult();
            if(_cityRepository.Exists(x => x.Name == command.Name)) 
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var city = new City(command.Name, command.DialCode, 
                command.MetaDescription, slug, command.Keywords, command.ProvinceId);
            _cityRepository.Create(city);
            _cityRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditCity command)
        {
            var operation = new OperationResult();
            var city = _cityRepository.Get(command.Id);

            if (city == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_cityRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            city.Edit(command.Name, command.DialCode,
                command.MetaDescription, slug, command.Keywords, command.ProvinceId);

            _cityRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditCity GetDetails(long id)
        {
            return _cityRepository.GetDetails(id);
        }

        public List<CityViewModel> GetCitys()
        {
            return _cityRepository.GetCitys();
        }

        public List<CityViewModel> Search(CitySearchModel searchModel)
        {
            return _cityRepository.Search(searchModel);
        }
    }
}
