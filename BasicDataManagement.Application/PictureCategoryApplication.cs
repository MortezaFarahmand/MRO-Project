using _0_Framework.Application;
using BasicDataManagement.Application.Contracts.PictureCategory;
using BasicDataManagement.Domain.PictureCategoryAgg;
using System.Collections.Generic;

namespace BasicDataManagement.Application
{
    public class PictureCategoryApplication : IPictureCategoryApplication
    {
        private readonly IPictureCategoryRepository _pictureCategoryRepository;
        public PictureCategoryApplication(IPictureCategoryRepository pictureCategoryRepository)
        {
            _pictureCategoryRepository = pictureCategoryRepository;
        }


        public OperationResult Create(CreatePictureCategory command)
        {
            var operation = new OperationResult();
            if (_pictureCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var pictureCategory = new PictureCategory(command.Name ,command.Remark, command.EntitiId);

            _pictureCategoryRepository.Create(pictureCategory);
            _pictureCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditPictureCategory command)
        {
            var operation = new OperationResult();
            var pictureCategory = _pictureCategoryRepository.Get(command.Id);
            if (pictureCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_pictureCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            pictureCategory.Edit(command.Name, command.Remark, command.EntitiId);

            _pictureCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult DeActive(long id)
        {
            var operation = new OperationResult();
            var pictureCategory = _pictureCategoryRepository.Get(id);
            if (pictureCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            pictureCategory.DeActive();
            _pictureCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Active(long id)
        {
            var operation = new OperationResult();
            var pictureCategory = _pictureCategoryRepository.Get(id);
            if (pictureCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            pictureCategory.Active();
            _pictureCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditPictureCategory GetDetails(long id)
        {
            return _pictureCategoryRepository.GetDetails(id);
        }

        public List<PictureCategoryViewModel> Search(PictureCategorySearchModel searchModel)
        {
            return _pictureCategoryRepository.Search(searchModel);
        }

        public List<PictureCategoryViewModel> GetPictureCategorys()
        {
            return _pictureCategoryRepository.GetPictureCategory();
        }
    }
}
