//using OrganizationManagement.Application.Contracts.Person;
//using System.Collections.Generic;
//using _0_Framework.Application;
//using OrganizationManagement.Domain.PersonAgg;
//using System.Net.Http.Headers;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

//namespace OrganizationManagement.Application
//{
//    public class PersonApplication : IPersonApplication
//    {
//        private readonly IPersonRepository _personRepository;

//        public PersonApplication(IPersonRepository personRepository)
//        {
//            _personRepository = personRepository;
//        }

//        public OperationResult Create(CreatePerson command)
//        {
//            var operation = new OperationResult();
//            if (_personRepository.Exists(x => x.NationalCode == command.NationalCode || x.PassportNo == command.PassportNo))
//                return operation.Failed(ApplicationMessages.DuplicatedRecord);

//            var person = new Person(command.NameEn, command.NameFa, command.FamilyEn, command.FamilyFa, command.FatherName, command.Birthday,
//                command.PassportNo, command.NationalCode, command.Attachment, command.Picture, command.PictureAlt, command.IDCartNo,
//                command.Address, command.OrganizationId);

//            _personRepository.Create(person);
//            _personRepository.SaveChanges();
//            return operation.Succeeded();
//        }

//        public OperationResult Edit(EditPerson command)
//        {
//            var operation = new OperationResult();
//            var person = _personRepository.Get(command.Id);
//            if (person == null)
//                return operation.Failed(ApplicationMessages.RecordNotFound);

//            if (_personRepository.Exists(x => x.NationalCode == command.NationalCode && x.Id == command.Id))
//                return operation.Failed(ApplicationMessages.DuplicatedRecord);

//            person.Edit(command.NameEn, command.NameFa, command.FamilyEn, command.FamilyFa, command.FatherName, command.Birthday,
//                command.PassportNo, command.NationalCode, command.Attachment, command.Picture, command.PictureAlt, command.IDCartNo,
//                command.Address, command.OrganizationId);

//            _personRepository.SaveChanges();
//            return operation.Succeeded();
//        }

//        public EditPerson GetDetails(long id)
//        {
//            return _personRepository.GetDetails(id);
//        }

//        public List<PersonViewModel> GetPerson()
//        {
//            return _personRepository.GetPerson();
//        }

//        public OperationResult IsActive(long id)
//        {
//            var operation = new OperationResult();
//            var person = _personRepository.Get(id);
//            if (person == null)
//                return operation.Failed(ApplicationMessages.RecordNotFound);

//            person.IsActive();
//            _personRepository.SaveChanges();
//            return operation.Succeeded();
//        }
//        public OperationResult IsNotActive(long id)
//        {
//            var operation = new OperationResult();
//            var person = _personRepository.Get(id);
//            if (person == null)
//                return operation.Failed(ApplicationMessages.RecordNotFound);

//            person.IsNotActive();
//            _personRepository.SaveChanges();
//            return operation.Succeeded();
//        }

//        public List<PersonViewModel> Search(PersonSearchModel searchModel)
//        {
//            return _personRepository.Search(searchModel);
//        }



//        //public OperationResult Create(CreatePerson command)
//        //{
//        //    var operation = new OperationResult();
//        //    if (_personRepository.Exists(x => x.Name == command.Name))
//        //        return operation.Failed(ApplicationMessages.DuplicatedRecord);

//        //    var person = new Person(command.Name, command.Alpha2Code, command.Alpha3Code, command.UNCode,
//        //        command.DialCode, command.Picture, command.TailCode);

//        //    _personRepository.Create(person);
//        //    _personRepository.SaveChanges();
//        //    return operation.Succeeded();
//        //}

//        //public OperationResult Edit(EditPerson command)
//        //{
//        //    var operation = new OperationResult();
//        //    var person = _personRepository.Get(command.Id);
//        //    if (person == null)
//        //        return operation.Failed(ApplicationMessages.RecordNotFound);

//        //    if (_personRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
//        //        return operation.Failed(ApplicationMessages.DuplicatedRecord);

//        //    person.Edit(command.Name, command.Alpha2Code, command.Alpha3Code, command.UNCode,
//        //        command.DialCode, command.Picture, command.TailCode);

//        //    _personRepository.SaveChanges();
//        //    return operation.Succeeded();

//        //}

//        //public EditPerson GetDetails(long id)
//        //{
//        //    return _personRepository.GetDetails(id);
//        //}

//        //public List<PersonViewModel> GetPersons()
//        //{
//        //    return _personRepository.GetPersons();
//        //}

//        //public List<PersonViewModel> Search(PersonSearchModel searchModel)
//        //{
//        //    return _personRepository.Search(searchModel);
//        //}
//    }
//}
