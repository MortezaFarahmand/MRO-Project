using _0_Framework.Application;
using OrganizationManagement.Application.Contracts.OrganizationPicture;
using OrganizationManagement.Domain.OrganizationPictureAgg;
using System;
using System.Collections.Generic;


namespace OrganizationManagement.Application
{
    public class OrganizationPictureApplication : IOrganizationPictureApplication
    {
        private readonly IOrganizationPictureRepository _organizationPictureRepository;

        public OrganizationPictureApplication(IOrganizationPictureRepository organizationPictureRepository)
        {
            _organizationPictureRepository = organizationPictureRepository;
        }



        public OperationResult Create(CreateOrganizationPicture command)
        {
            var operation = new OperationResult();
            if (_organizationPictureRepository.Exists(x => x.Picture == command.Picture && x.OrganizationId == command.OrganizationId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var organizationPicture = new OrganizationPicture(command.OrganizationId, command.Picture, command.PictureAlt, command.PictureTitle);
            _organizationPictureRepository.Create(organizationPicture);
            _organizationPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditOrganizationPicture command)
        {
            var operation = new OperationResult();
            var organizationPicture = _organizationPictureRepository.Get(command.Id);
            if (organizationPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_organizationPictureRepository.Exists(x => x.Picture == command.Picture && x.OrganizationId == command.OrganizationId && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            organizationPicture.Edit(command.OrganizationId, command.Picture, command.PictureAlt, command.PictureTitle);
            _organizationPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditOrganizationPicture GetDetails(long id)
        {
            return _organizationPictureRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var organizationPicture = _organizationPictureRepository.Get(id);
            if (organizationPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            organizationPicture.Remove();
            _organizationPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var organizationPicture = _organizationPictureRepository.Get(id);
            if (organizationPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            organizationPicture.Restore();
            _organizationPictureRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<OrganizationPictureViewModel> Search(OrganizationPictureSearchModel searchModel)
        {
            return _organizationPictureRepository.Search(searchModel);
        }
    }
}
