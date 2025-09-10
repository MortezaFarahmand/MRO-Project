
using _0_Framework.Application;
using OrganizationManagement.Application.Contracts.OrganizationAviationCode;
using OrganizationManagement.Domain.OrganizationAviationCodeAgg;
using System.Collections.Generic;

namespace OrganizationManagement.Application
{
    public class OrganizationAviationCodeApplication : IOrganizationAviationCodeApplication
    {
        private readonly IOrganizationAviationCodeRepository _organizationAviationCodeRepository;

        public OrganizationAviationCodeApplication(IOrganizationAviationCodeRepository organizationAviationCodeRepository)
        {
            _organizationAviationCodeRepository = organizationAviationCodeRepository;
        }



        public OperationResult Create(CreateOrganizationAviationCode command)
        {
            var operation = new OperationResult();
            if (_organizationAviationCodeRepository.Exists(x => x.ICAO == command.ICAO))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var organizationAviationCode = new OrganizationAviationCode(command.ICAO, command.IATA, command.CivilAutority, command.Comment,
                command.Description, command.CallSign, command.CountryId);

            _organizationAviationCodeRepository.Create(organizationAviationCode);
            _organizationAviationCodeRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditOrganizationAviationCode command)
        {
            var operation = new OperationResult();
            var organizationCode = _organizationAviationCodeRepository.Get(command.Id);
            if (organizationCode == null)   
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_organizationAviationCodeRepository.Exists(x => x.ICAO == command.ICAO && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            organizationCode.Edit(command.ICAO, command.IATA, command.CivilAutority, command.Comment,
                command.Description, command.CallSign, command.CountryId);

            _organizationAviationCodeRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<OrganizationAviationCodeViewModel> GetOrganizationAviationCodes()
        {
            return _organizationAviationCodeRepository.GetOrganizationAviationCodes();
        }

        public EditOrganizationAviationCode GetDetails(long id)
        {
            return _organizationAviationCodeRepository.GetDetails(id);
        }

        public List<OrganizationAviationCodeViewModel> Search(OrganizationAviationCodeSearchModel searchModel)
        {
            return _organizationAviationCodeRepository.Search(searchModel);
        }

        public OperationResult IsEnable(long id)
        {
            var operation = new OperationResult();
            var organizationCode = _organizationAviationCodeRepository.Get(id);
            if (organizationCode == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            organizationCode.Enabled();
            _organizationAviationCodeRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult IsDisable(long id)
        {
            var operation = new OperationResult();
            var organizationCode = _organizationAviationCodeRepository.Get(id);
            if (organizationCode == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            organizationCode.Disabled();
            _organizationAviationCodeRepository.SaveChanges();
            return operation.Succeeded();
        }
        

    }
}
