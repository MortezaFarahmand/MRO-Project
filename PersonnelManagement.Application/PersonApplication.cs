using _0_Framework.Application;
using PersonnelManagement.Application.Contracts.Person;
using PersonnelManagement.Domain.PersonAgg;
using System.Collections.Generic;

namespace PersonnelManagement.Application
{
    public class PersonApplication : IPersonApplication
    {
        private readonly IPersonRepository _personRepository;

        public PersonApplication(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public OperationResult Create(CreatePerson command)
        {
            var operation = new OperationResult();
            if (_personRepository.Exists(x => x.NationalCode == command.NationalCode || x.PassportNo == command.PassportNo))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var person = new Person(command.NameEn, command.NameFa, command.FamilyEn, command.FamilyFa,
                command.FatherName, command.Gender, command.Birthday, command.Marriage, command.PassportNo,
                command.NationalCode, command.NationalCodeOfFather, command.BirthCityId, command.BirthProvinceId,
                command.BirthCountryId, command.CityId, command.Attachment, command.EducationalDegree,
                command.EducationalField, command.IDCartNo, command.AddressId, command.MobileNo1,
                command.MobileNo2, command.PhoneNo, command.MailBoxAddress1, command.MailBoxAddress2,
                command.SocialAddress1, command.SocialAddress2, command.WorkingStartDate,
                command.PersonGroupId, command.OrganizationId);

            _personRepository.Create(person);
            _personRepository.SaveChanges();
            return operation.Succeeded();
        }


        public OperationResult Edit(EditPerson command)
        {
            var operation = new OperationResult();
            var person = _personRepository.Get(command.Id);
            if (person == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_personRepository.Exists(x => x.NationalCode == command.NationalCode && x.Id == command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            person.Edit(command.NameEn, command.NameFa, command.FamilyEn, command.FamilyFa,
                command.FatherName, command.Gender, command.Birthday, command.Marriage, command.PassportNo,
                command.NationalCode, command.NationalCodeOfFather, command.BirthCityId, command.BirthProvinceId,
                command.BirthCountryId, command.CityId, command.Attachment, command.EducationalDegree,
                command.EducationalField, command.IDCartNo, command.AddressId, command.MobileNo1,
                command.MobileNo2, command.PhoneNo, command.MailBoxAddress1, command.MailBoxAddress2,
                command.SocialAddress1, command.SocialAddress2, command.WorkingStartDate,
                command.PersonGroupId, command.OrganizationId);

            _personRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditPerson GetDetails(long id)
        {
            return _personRepository.GetDetails(id);
        }

        public List<PersonViewModel> GetPerson()
        {
            return _personRepository.GetPerson();
        }

        public OperationResult Active(long id)
        {
            var operation = new OperationResult();
            var person = _personRepository.Get(id);
            if (person == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            person.Active();
            _personRepository.SaveChanges();
            return operation.Succeeded();
        }
        public OperationResult DeActive(long id)
        {
            var operation = new OperationResult();
            var person = _personRepository.Get(id);
            if (person == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            person.DeActive();
            _personRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<PersonViewModel> Search(PersonSearchModel searchModel)
        {
            return _personRepository.Search(searchModel);
        }


    }
}
