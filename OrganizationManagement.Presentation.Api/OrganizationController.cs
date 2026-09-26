using Microsoft.AspNetCore.Mvc;
using OrganizationManagement.Application.Contracts.Organization;
using OrganizationManagement.Domain.OrganizationAgg;

namespace OrganizationManagement.Presentation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationController(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }


        [HttpGet]
        public List<OrganizationViewModel> GetOrganizations()
        {
            return _organizationRepository.GetOrganizations();
        }
    }
}
