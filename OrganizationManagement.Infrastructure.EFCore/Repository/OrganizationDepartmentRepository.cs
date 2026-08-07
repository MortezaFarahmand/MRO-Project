using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OrganizationManagement.Application.Contracts.OrganizationDepartment;
using OrganizationManagement.Domain.OrganizationDepartmentAgg;
using System.Collections.Generic;
using System.Linq;

namespace OrganizationManagement.Infrastructure.EFCore.Repository
{
    public class OrganizationDepartmentRepository : RepositoryBase<long, OrganizationDepartment>, IOrganizationDepartmentRepository
    {
        private readonly OrganizationContext _context;
        public OrganizationDepartmentRepository(OrganizationContext context) : base(context)
        { 
            _context = context;
        }

        public EditOrganizationDepartment GetDetails(long id)
        {
            return _context.OrganizationDepartments.Select(x => new EditOrganizationDepartment()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                OrganizationId = x.OrganizationId,
                ParentDepartmentId = x.ParentDepartmentId

            }).FirstOrDefault(d => d.Id == id);
        }

        public List<OrganizationDepartmentViewModel> GetOrganizationDepartments()
        {
            return _context.OrganizationDepartments.Select(x => new OrganizationDepartmentViewModel()
            {

                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                OrganizationId = x.OrganizationId,
                ParentDepartmentId = x.ParentDepartmentId

            }).ToList();
        }

        

        public List<OrganizationDepartmentViewModel> Search(OrganizationDepartmentSearchModel searchModel)
        {
            var OrganizationDeparts = _context.OrganizationDepartments.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.OrganizationDepartments
                .Include(x => x.Organization)
                .Select(x => new OrganizationDepartmentViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                OrganizationId = x.OrganizationId,
                ParentDepartmentId = x.ParentDepartmentId,
                Organization = x.Organization.NameEn,
                //ParentDepartment = x.OrganizationDepartments.Name,
                CreationDate = x.CreationDate.ToString()

            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (searchModel.OrganizationId != 0)
                query = query.Where(x => x.OrganizationId == searchModel.OrganizationId);

            //if (searchModel.ParentDepartmentId != 0)
            //    query = query.Where(x => x.ParentDepartmentId == searchModel.ParentDepartmentId);

            /////////////////////
            var organizDept = query.OrderByDescending(x => x.Id).ToList();
            organizDept.ForEach
                (Dept => Dept.ParentDepartment = OrganizationDeparts.FirstOrDefault(x => x.Id == Dept.ParentDepartmentId)?.Name);
            ///////////////////////

            return organizDept;
        }

        public List<OrganizationDepartmentViewModel> GetDepartmentsByOrganizationId(long id)
        {
            var OrganizationDeparts = _context.OrganizationDepartments
                .Where(x => x.OrganizationId == id).Select(x => new OrganizationDepartmentViewModel()
                { 
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            return OrganizationDeparts;

            

        }
    }
}
