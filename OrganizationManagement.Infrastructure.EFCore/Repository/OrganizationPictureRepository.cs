using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OrganizationManagement.Application.Contracts.OrganizationPicture;
using OrganizationManagement.Domain.OrganizationPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationManagement.Infrastructure.EFCore.Repository
{
    public class OrganizationPictureRepository : RepositoryBase<long, OrganizationPicture>, IOrganizationPictureRepository
    {
        private readonly OrganizationContext _context;
        public OrganizationPictureRepository(OrganizationContext context) : base(context)
        {
            _context = context;
        }


        public EditOrganizationPicture GetDetails(long id)
        {
            return _context.OrganizationPictures.Select(x => new EditOrganizationPicture {
                Id = x.Id,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,  
                OrganizationId = x.OrganizationId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<OrganizationPictureViewModel> Search(OrganizationPictureSearchModel searchModel)
        {
            var query = _context.OrganizationPictures
                .Include(x => x.Organization)
                .Select(x => new OrganizationPictureViewModel 
            {
                Id = x.Id,
                Organization = x.Organization.NameEn,
                CreationDate = x.CreationDate.ToString(),
                Picture = x.Picture,
                OrganizationId = x.OrganizationId,
                IsRemoved = x.IsRemoved
            });

            if(searchModel.OrganizationId != 0)
                query = query.Where(x => x.OrganizationId == searchModel.OrganizationId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
