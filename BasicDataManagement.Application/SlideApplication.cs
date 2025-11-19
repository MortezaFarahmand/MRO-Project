using _0_Framework.Application;
using BasicDataManagement.Application.Contracts.Slide;
using BasicDataManagement.Domain.SlideAgg;
using System.Collections.Generic;

namespace BasicDataManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository, IFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
            _slideRepository = slideRepository;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();

            var slugTitle = command.PictureTitle.Slugify();
            var picturePath = "SliderPictures";
            var pictureName = _fileUploader.Upload(command.Picture, picturePath);
            var pictureAltName = _fileUploader.Upload(command.PictureAlt, picturePath);

            var slide = new Slide(pictureName, pictureAltName, command.PictureTitle,
                command.Heading, command.Title, command.Text, command.Link, command.BtnText);

            _slideRepository.Create(slide);
            _slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.Get(command.Id);
            if (slide == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            
            var slugTitle = command.PictureTitle.Slugify();
            var picturePath = "SliderPictures";
            var pictureName = _fileUploader.Upload(command.Picture, picturePath);
            var pictureAltName = _fileUploader.Upload(command.PictureAlt, picturePath);

            slide.Edit(pictureName, pictureAltName, command.PictureTitle,
                command.Heading, slugTitle, command.Text, command.Link, command.BtnText);
            _slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditSlide GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }

        public List<SlideViewModel> GetList()
        {
            return _slideRepository.GetList();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            slide.Remove();
            _slideRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            slide.Restore();
            _slideRepository.SaveChanges();
            return operation.Succeeded();
        }
    }
}
