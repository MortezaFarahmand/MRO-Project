using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace BasicDataManagement.Presentation.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        //private readonly IOrganizationRepository _organizationRepository;

        //public OrganizationController(IOrganizationRepository organizationRepository)
        //{
        //    _organizationRepository = organizationRepository;
        //}


        //[HttpGet]
        //public List<OrganizationViewModel> GetOrganizations()
        //{
        //    return _organizationRepository.GetOrganizations();
        //}
    }
}
