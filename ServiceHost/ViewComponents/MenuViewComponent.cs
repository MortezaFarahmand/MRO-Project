using _01_MROQuery.Contracts.OrganizationGroup;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IOrganizatonGroupQuery _organizatonGroupQuery;

        public MenuViewComponent(IOrganizatonGroupQuery organizatonGroupQuery)
        {
            _organizatonGroupQuery = organizatonGroupQuery;
        }


        public IViewComponentResult Invoke()
        {
            //var organizationGroups = _organizatonGroupQuery.GetOrganizationGroups();
            //return View(organizationGroups);
            return View();
        }
    }
}
