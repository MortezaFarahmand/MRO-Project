using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BasicDataManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using OrganizationManagement.Application.Contracts.Organization;
using OrganizationManagement.Domain.OrganizationAgg;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace OrganizationManagement.Infrastructure.EFCore.Repository
{
    public class OrganizationRepository : RepositoryBase<long, Organization>, IOrganizationRepository
    {
        private readonly OrganizationContext _context;
        private readonly BasicDataContext _basicDataContext;

        public OrganizationRepository(OrganizationContext context, BasicDataContext basicDataContext) : base(context)
        {
            _context = context;
            _basicDataContext = basicDataContext;
        }


        public EditOrganization GetDetails(long id)
        {
            return _context.Organizations.Select(x => new EditOrganization
            {
                Id = x.Id,
                NameEn = x.NameEn,
                NameFa = x.NameFa,
                DescriptionEn = x.DescriptionEn,
                DescriptionFa = x.DescriptionFa,
                AddressFa = x.AddressFa,
                AddressEn = x.AddressEn,
                Tel1 = x.Tel1,
                Tel2 = x.Tel2,
                Fax = x.Fax,
                WebSite = x.WebSite,
                SocialAddress1 = x.SocialAddress1,
                SocialAddress2 = x.SocialAddress2,
                MailBox = x.MailBox,
                Note = x.Note,
                Remark = x.Remark,
                OrganizationAviationCodeId = x.OrganizationAviationCodeId,
                CompanyRegisterNo = x.CompanyRegisterNo,
                FormedDate = x.FormedDate.ToString(CultureInfo.InvariantCulture),
                ParentId = x.ParentId,
                LogoPicture = x.LogoPicture,
                LogoPictureAlt = x.LogoPictureAlt,
                LogoPictureTitle = x.LogoPictureTitle,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                CanonicalAddress = x.CanonicalAddress,
                Keywords = x.Keywords,
                OrganizationGroupId = x.OrganizationGroupId,
                EntitiId = x.EntitiId
                //Entiti = x.Entiti.Name

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<OrganizationViewModel> GetOrganizations()
        {
            return _context.Organizations.Select(x => new OrganizationViewModel
            {
                Id = x.Id,
                NameEn = x.NameEn
            }).ToList();
        }

        public List<OrganizationViewModel> Search(OrganizationSearchModel searchModel)
        {
            var Entities = _basicDataContext.Entitis.Select(x => new {x.Id, x.Name}).ToList();
            var query = _context.Organizations
                .Include(x => x.Group)
                .Select(x => new OrganizationViewModel
                {
                    Id = x.Id,
                    NameEn = x.NameEn,
                    NameFa = x.NameFa,
                    DescriptionEn = x.DescriptionEn,
                    DescriptionFa = x.DescriptionFa,
                    AddressEn = x.AddressEn,
                    AddressFa = x.AddressFa,
                    Tel1 = x.Tel1,
                    Tel2 = x.Tel2,
                    Fax = x.Fax,
                    WebSite = x.WebSite,
                    SocialAddress1 = x.SocialAddress1,
                    SocialAddress2 = x.SocialAddress2,
                    MailBox = x.MailBox,
                    Note = x.Note,
                    Remark = x.Remark,
                    CompanyRegisterNo = x.CompanyRegisterNo,
                    FormedDate = x.FormedDate.ToFarsi(),
                    FormedDateGr = x.FormedDate,
                    LogoPicture = x.LogoPicture,
                    ParentId = x.ParentId,
                    OrganizationAviationCodeId = x.OrganizationAviationCodeId,
                    Group = x.Group.Name,
                    OrganizationGroupId = x.OrganizationGroupId,
                    CreationDate = x.CreationDate.ToString(),
                    IsActive = x.IsActive,
                    EntitiId = x.EntitiId,
                    //Entiti = x.Entiti.Name
                });

            if (!string.IsNullOrWhiteSpace(searchModel.NameEn))
                query = query.Where(x => x.NameEn.Contains(searchModel.NameEn));

            if (!string.IsNullOrWhiteSpace(searchModel.NameFa))
                query = query.Where(x => x.NameFa.Contains(searchModel.NameFa));

            if (!string.IsNullOrWhiteSpace(searchModel.DescriptionEn))
                query = query.Where(x => x.DescriptionEn.Contains(searchModel.DescriptionEn));

            if (!string.IsNullOrWhiteSpace(searchModel.DescriptionFa))
                query = query.Where(x => x.DescriptionFa.Contains(searchModel.DescriptionFa));

            if (searchModel.OrganizationGroupId != 0)
                query = query.Where(x => x.OrganizationGroupId == searchModel.OrganizationGroupId);

            /////////////////////
            var organizes = query.OrderByDescending(x => x.Id).ToList();
            organizes.ForEach
                (organiz => organiz.Entiti = Entities.FirstOrDefault(x => x.Id == organiz.EntitiId)?.Name);
            ///////////////////////
            
            return organizes;

        }
    }
}
