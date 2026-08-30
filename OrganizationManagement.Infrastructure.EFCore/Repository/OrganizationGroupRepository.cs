
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using OrganizationManagement.Application.Contracts.Country;
using OrganizationManagement.Domain.OrganizationGroupAgg;

namespace OrganizationManagement.Infrastructure.EFCore.Repository
{
    public class OrganizationGroupRepository : RepositoryBase<long, OrganizationGroup>, IOrganizationGroupRepository
    {
        private readonly OrganizationContext _context;

        public OrganizationGroupRepository(OrganizationContext context) : base(context)
        {
            _context = context;
        }



        public EditOrganizationGroup GetDetails(long id)
        {
            return _context.OrganizationGroups.Select(x => new EditOrganizationGroup()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                //Picture = x.Picture,
                NameCode = x.NameCode,
                //PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<OrganizationGroupViewModel> GetOrganizationGroups()
        {
            return _context.OrganizationGroups.Select(x => new OrganizationGroupViewModel()
            {

                Id = x.Id,
                Name = x.Name,
                //NameCode=x.NameCode,

            }).ToList();
        }

        public List<OrganizationGroupViewModel> Search(OrganizationGroupSearchModel searchModel)
        {
            var query = _context.OrganizationGroups.Select(x => new OrganizationGroupViewModel()
            {
                Id = x.Id,
                Picture = x.Picture,
                Name = x.Name,
                Description = x.Description,
                NameCode = x.NameCode,
                CreationDate = x.CreationDate.ToString()

            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Description))
                query = query.Where(x => x.Description.Contains(searchModel.Description));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }

}
