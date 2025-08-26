using _0_Framework.Application;
using PersonnelManagement.Application.Contracts.PersonGroup;
using PersonnelManagement.Domain.PersonGroupAgg;
using System;
using System.Collections.Generic;

namespace PersonnelManagement.Application
{
    public class PersonGroupApplication : IPersonGroupApplication
    {
        private readonly IPersonGroupRepository _personGroupRepository;
        public PersonGroupApplication(IPersonGroupRepository personGroupRepository)
        {
            _personGroupRepository = personGroupRepository;
        }


        public OperationResult Create(CreatePersonGroup command)
        {
            var operation = new OperationResult();
            if (_personGroupRepository.Exists(x => x.Description == command.Description))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var personGroup = new PersonGroup(command.Description, command.Remark, command.IsActive);
            _personGroupRepository.Create(personGroup);
            _personGroupRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditPersonGroup command)
        {
            var operation = new OperationResult();
            var personGroup = _personGroupRepository.Get(command.Id);
            if (personGroup == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_personGroupRepository.Exists(x => x.Description == command.Description && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            personGroup.Edit(command.Description, command.Remark, command.IsActive);
            _personGroupRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditPersonGroup GetDetails(long id)
        {
            return  _personGroupRepository.GetDetails(id);
        }

        public List<PersonGroupViewModel> GetPersonGroups()
        {
            return _personGroupRepository.GetPersonGroups();
        }

        //public List<PersonGroupViewModel> Search(PersonGroupSearchModel searchModel)
        //{
        //    return _personGroupRepository.Search(searchModel);
        //}
    }
}
