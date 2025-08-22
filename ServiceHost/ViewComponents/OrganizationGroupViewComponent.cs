using _01_MROQuery.Contracts.OrganizationGroup;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class OrganizationGroupViewComponent : ViewComponent
    {
        private readonly IOrganizatonGroupQuery _organizatonGroupQuery;

        public OrganizationGroupViewComponent(IOrganizatonGroupQuery organizatonGroupQuery)
        {
            _organizatonGroupQuery = organizatonGroupQuery;
        }


        public IViewComponentResult Invoke()
        {
            var organizationGroups = _organizatonGroupQuery.GetOrganizationGroups();
            return View(organizationGroups);
        }
    }
}
