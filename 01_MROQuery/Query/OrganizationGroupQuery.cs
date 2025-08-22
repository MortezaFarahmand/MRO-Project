
using _01_MROQuery.Contracts.OrganizationGroup;
using OrganizationManagement.Infrastructure.EFCore;

namespace _01_MROQuery.Query
{
    public class OrganizationGroupQuery : IOrganizatonGroupQuery
    {
        private readonly OrganizationContext _context;

        public OrganizationGroupQuery(OrganizationContext context)
        {
            _context = context;
        }


        List<OrganizationGroupQueryModel> IOrganizatonGroupQuery.GetOrganizationGroups()
        {
            return _context.OrganizationGroups.Select(x => new OrganizationGroupQueryModel 
            {
              Id = x.Id,
              Name = x.Name,
              Description = x.Description,
              Picture = x.Picture,
              PictureAlt = x.PictureAlt,
              PictureTitle = x.PictureTitle,
              Slug = x.Slug,
              Keywords = x.Keywords,
              CreationDate = x.CreationDate.ToShortDateString()
            }).ToList();
        }
    }
}
