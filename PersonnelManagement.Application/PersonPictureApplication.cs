using _0_Framework.Application;
using PersonnelManagement.Application.Contracts.PersonPicture;
using PersonnelManagement.Domain.PersonPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PersonnelManagement.Application
{
    public class PersonPictureApplication : IPersonPictureApplication
    {
        private readonly IPersonPictureRepository _personPictureRepository;
        public PersonPictureApplication(IPersonPictureRepository personPictureRepository)
        {
            _personPictureRepository = personPictureRepository;
        }


        public OperationResult Create(CreatePersonPicture command)
        {
            var operation = new OperationResult();
            var personPicture = new PersonPicture (command.Picture, command.Title, command.Text, 
                command.Remark, command.PictureCategoryId, command.PictureCategoryId);

            _personPictureRepository.Create(personPicture);
            _personPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditPersonPicture command)
        {
            var operation = new OperationResult();
            var personPicture = _personPictureRepository.Get(command.Id);
            if(personPicture == null) 
                return operation.Failed(ApplicationMessages.RecordNotFound);

            personPicture.Edit(command.Picture, command.Title, command.Text,
                command.Remark, command.PictureCategoryId, command.PictureCategoryId);
            _personPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditPersonPicture GetDetails(long id)
        {
            return _personPictureRepository.GetDetails(id);
        }

        public List<PersonPictureViewModel> GetList()
        {
            return _personPictureRepository.GetList();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var personPicture = _personPictureRepository.Get(id);
            if (personPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            personPicture.Remove();
            _personPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var personPicture = _personPictureRepository.Get(id);
            if (personPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            personPicture.Restore();
            _personPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<PersonPictureViewModel> Search(PersonPictureSearchModel searchModel)
        {
            return _personPictureRepository.Search(searchModel);
        }
    }
}
