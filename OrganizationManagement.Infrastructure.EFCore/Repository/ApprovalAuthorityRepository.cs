using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OrganizationManagement.Application.Contracts.ApprovalAuthority;
using OrganizationManagement.Application.Contracts.Organization;
using OrganizationManagement.Domain.ApprovalAutorityAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationManagement.Infrastructure.EFCore.Repository
{
    public class ApprovalAuthorityRepository : RepositoryBase<long, ApprovalAuthority>, IApprovalAuthorityRepository
    {
        private readonly OrganizationContext _context;

        public ApprovalAuthorityRepository(OrganizationContext context) : base(context)
        {
            _context = context;
        }

        public List<ApprovalAuthorityViewModel> GetApprovalAutoritys()
        {
            return _context.ApprovalAuthorities
                .Select(x => new ApprovalAuthorityViewModel
            {
                Id = x.Id,
                NameEn = x.NameEn,
                NameFa = x.NameFa,
                Description = x.Description,
                Remark = x.Remark,
                Code = x.Code,
                LogoPicture = x.LogoPicture,
            }).ToList();
        }

        //public ApprovalAuthority GetApprovalAutorityWithCountry(long id)
        //{
        //    throw new NotImplementedException();
        //}

        public EditApprovalAuthority GetDetails(long id)
        {
            return _context.ApprovalAuthorities.Select(x => new EditApprovalAuthority()
            {
                Id = x.Id,
                NameEn = x.NameEn,
                NameFa = x.NameFa,
                Description = x.Description,
                Address = x.Address,
                WebSite = x.WebSite,
                Remark = x.Remark,
                Code = x.Code,
                LogoPicture = x.LogoPicture,
                LogoPictureAlt = x.LogoPictureAlt,
                LogoPictureTitle = x.LogoPictureTitle,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                CanonicalAddress = x.CanonicalAddress,
                Keywords = x.Keywords
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ApprovalAuthorityViewModel> Search(ApprovalAuthoritySearchModel searchModel)
        {
            var query = _context.ApprovalAuthorities
                .Include(x => x.Country)
                .Select(x => new ApprovalAuthorityViewModel()
            {
                Id = x.Id,
                NameEn = x.NameEn,
                NameFa = x.NameFa,
                Description = x.Description,
                Remark = x.Remark,
                Code = x.Code,
                LogoPicture = x.LogoPicture,
                CountryId = x.CountryId,
                Country = x.Country.Name
                });

            if(!string.IsNullOrWhiteSpace(searchModel.NameEn)) 
                query = query.Where(x=>x.NameEn.Contains(searchModel.NameEn));

            if (!string.IsNullOrWhiteSpace(searchModel.Description))
                query = query.Where(x => x.Description.Contains(searchModel.Description));

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code.Contains(searchModel.Code));

            if (searchModel.CountryId != 0)
                query = query.Where(x => x.CountryId == searchModel.CountryId);

            return query.OrderByDescending(x => x.Id).ToList();

        }
    }
}
