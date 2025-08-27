using _0_Framework.Application;
using BasicDataManagement.Application.Contract.Entiti;
using BasicDataManagement.Domain.EntitiAgg;
using System.Collections.Generic;

namespace BasicDataManagement.Application
{
    public class EntitiApplication : IEntitiApplication
    {
        private readonly IEntitiRepository _entitiRepository;

        public EntitiApplication(IEntitiRepository entitiRepository)
        {
            _entitiRepository = entitiRepository;
        }


        public OperationResult Create(CreateEntiti command)
        {
            var operation  = new OperationResult();
            if(_entitiRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var entiti = new Entiti(command.Name, command.Title, command.Remark);
            _entitiRepository.Create(entiti);
            _entitiRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditEntiti command)
        {
            var operation = new OperationResult();
            var entiti = _entitiRepository.Get(command.Id);
            if(entiti == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_entitiRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            entiti.Edit(command.Name, command.Title, command.Remark, command.IsActive);
            _entitiRepository.Create(entiti);
            _entitiRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditEntiti GetDetails(long id)
        {
            return _entitiRepository.GetDetails(id);
        }

        public List<EntitiViewModel> GetEntitis()
        {
            return _entitiRepository.GetEntitis();
        }
    }
}
