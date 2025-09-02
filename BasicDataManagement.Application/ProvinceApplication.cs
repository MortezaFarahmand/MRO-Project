using _0_Framework.Application;
using BasicDataManagement.Application.Contract.Province;
using BasicDataManagement.Domain.ProvinceAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicDataManagement.Application
{
    public class ProvinceApplication : IProvinceApplication
    {
        private readonly IProvinceRepository _provinceRepository;

        public ProvinceApplication(IProvinceRepository provinceRepository)
        {
            _provinceRepository = provinceRepository;
        }


        public OperationResult Create(CreateProvince command)
        {
            var operation = new OperationResult();
            if(_provinceRepository.Exists(x => x.Name == command.Name)) 
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var province = new Province(command.Name, command.DialCode, 
                command.MetaDescription, slug, command.Keywords, command.CountryId);
            _provinceRepository.Create(province);
            _provinceRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditProvince command)
        {
            var operation = new OperationResult();
            var province = _provinceRepository.Get(command.Id);

            if (province == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_provinceRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            province.Edit(command.Name, command.DialCode,
                command.MetaDescription, slug, command.Keywords, command.CountryId);

            _provinceRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditProvince GetDetails(long id)
        {
            return _provinceRepository.GetDetails(id);
        }

        public List<ProvinceViewModel> Search(ProvinceSearchModel searchModel)
        {
            return _provinceRepository.Search(searchModel);
        }
    }
}
