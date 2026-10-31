using _01_MROQuery.Contracts.Organization;
using Microsoft.EntityFrameworkCore;
using OrganizationManagement.Infrastructure.EFCore;

namespace _01_MROQuery.Query
{
    public class OrganizationQuery : IOrganizationQuery
    {
        private readonly OrganizationContext _context;

        public OrganizationQuery(OrganizationContext context)
        {
            _context = context;
        }

        public string GetNameEn(long organizationId)
        {
            return _context.Organizations
                .Where(x => x.Id == organizationId)
                .Select(x => x.NameEn)
                .FirstOrDefault();
        }

        public string GetNameFa(long organizationId)
        {
            return _context.Organizations
                .Where(x => x.Id == organizationId)
                .Select(x => x.NameFa)
                .FirstOrDefault();
        }

        public Dictionary<long, string> GetNames(List<long> organizationIds)
        {
            if (organizationIds == null || !organizationIds.Any())
                return new Dictionary<long, string>();

            return _context.Organizations
                .Where(x => organizationIds.Contains(x.Id))
                .Select(x => new { x.Id, x.NameEn })
                .ToDictionary(x => x.Id, x => x.NameEn);
        }
    }
}