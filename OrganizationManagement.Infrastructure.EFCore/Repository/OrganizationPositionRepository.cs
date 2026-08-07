using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OrganizationManagement.Application.Contracts.OrganizationDepartment;
using OrganizationManagement.Application.Contracts.OrganizationPosition;
using OrganizationManagement.Domain.OrganizationDepartmentAgg;
using OrganizationManagement.Domain.OrganizationPositionAgg;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrganizationManagement.Infrastructure.EFCore.Repository
{
    public class OrganizationPositionRepository : RepositoryBase<long, OrganizationPosition>, IOrganizationPositionRepository
    {
        private readonly OrganizationContext _context;
        public OrganizationPositionRepository(OrganizationContext context) : base(context)
        {
            _context = context;
        }


        public EditOrganizationPosition GetDetails(long id)
        {
            return _context.OrganizationPositions.Select(x => new EditOrganizationPosition()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                OrganizationDepartmentId = x.OrganizationDepartmentId,
                ParentPositionId = x.ParentPositionId

            }).FirstOrDefault(d => d.Id == id);
        }

        public List<OrganizationPositionViewModel> GetOrganizationPositions()
        {
            return _context.OrganizationPositions.Select(x => new OrganizationPositionViewModel()
            {

                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                OrganizationDepartmentId = x.OrganizationDepartmentId,
                ParentPositionId = x.ParentPositionId

            }).ToList();

        }

        public List<OrganizationPositionViewModel> Search(OrganizationPositionSearchModel searchModel)
        {
            var organization = _context.Organizations.Select(x => new { x.Id, x.NameEn }).ToList();
            var organizationPos = _context.OrganizationPositions.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.OrganizationPositions
                .Include(x => x.OrganizationDepartment)
                .Select(x => new OrganizationPositionViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    OrganizationId = x.OrganizationDepartment.OrganizationId,
                    OrganizationDepartmentId = x.OrganizationDepartmentId,
                    ParentPositionId = x.ParentPositionId,
                    CreationDate = x.CreationDate.ToString()

                });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (searchModel.OrganizationId != 0)
                query = query.Where(x => x.OrganizationId == searchModel.OrganizationId);

            if (searchModel.OrganizationDepartmentId != 0)
                query = query.Where(x => x.OrganizationDepartmentId == searchModel.OrganizationDepartmentId);

            /////////////////////
            var organizPos = query.OrderByDescending(x => x.Id).ToList();
            organizPos.ForEach
                (org => org.OrganizationName = organization.FirstOrDefault(x => x.Id == org.OrganizationId)?.NameEn);
            organizPos.ForEach
                (Pos => Pos.ParentPosition = organizationPos.FirstOrDefault(x => x.Id == Pos.ParentPositionId)?.Name);
            ///////////////////////

            return organizPos;
        }
    }
}
