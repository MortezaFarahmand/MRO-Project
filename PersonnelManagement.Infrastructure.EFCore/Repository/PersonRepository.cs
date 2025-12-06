using _0_Framework.Infrastructure;
using BasicDataManagement.Domain.CityAgg;
using BasicDataManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.Contracts.Person;
using PersonnelManagement.Domain.PersonAgg;
using PersonnelManagement.Domain.PersonGroupAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace PersonnelManagement.Infrastructure.EFCore.Repository
{
    public class PersonRepository : RepositoryBase <long, Person>, IPersonRepository
    {
        private readonly PersonnelContext _context;
        private readonly BasicDataContext _basicDataContext;

        public PersonRepository(PersonnelContext context, BasicDataContext basicDataContext) : base(context)
        {
            _context = context;
            _basicDataContext = basicDataContext;
        }


        public EditPerson GetDetails(long id)
        {
            return _context.Persons.Select(x =>  new EditPerson() 
            { 
                Id = x.Id,
                NameEn = x.NameEn,
                NameFa = x.NameFa,
                FamilyEn = x.FamilyEn,
                FamilyFa = x.FamilyFa,
                FatherName = x.FatherName,
                Gender = x.Gender,
                Birthday = x.Birthday.ToString(),
                Marriage = x.Marriage,
                PassportNo = x.PassportNo,
                NationalCode = x.NationalCode,
                NationalCodeOfFather = x.NationalCodeOfFather,
                BirthCityId = x.BirthCityId,//
                CityId = x.CityId,//
                Attachment = x.Attachment,
                EducationalDegree = x.EducationalDegree,
                EducationalField = x.EducationalField,
                IDCartNo = x.IDCartNo,
                AddressId = x.AddressId,
                MobileNo1 = x.MobileNo1,
                MobileNo2 = x.MobileNo2,
                PhoneNo = x.PhoneNo,
                MailBoxAddress1 = x.MailBoxAddress1,
                MailBoxAddress2 = x.MailBoxAddress2,
                SocialAddress1 = x.SocialAddress1,
                SocialAddress2 = x.SocialAddress2,
                WorkingStartDate = x.WorkingStartDate.ToString(),
                PersonGroupId = x.PersonGroupId,//
                OrganizationId = x.OrganizationId,
                BirthProvinceId = x.BirthProvinceId,
                BirthCountryId = x.BirthCountryId


            }).FirstOrDefault(x => x.Id == id);   
                
        }

        public List<PersonViewModel> GetPerson()
        {
            return _context.Persons.Select(x => new PersonViewModel()
            {
                Id = x.Id,
                NameEn = x.NameEn,
                NameFa = x.NameFa,
                FamilyEn = x.FamilyEn,
                FamilyFa = x.FamilyFa,
                FatherName = x.FatherName,
                Birthday = x.Birthday.ToString(),
                PassportNo = x.PassportNo,
                NationalCode = x.NationalCode,
                PersonGroupId = x.PersonGroupId,
                OrganizationId = x.OrganizationId//


            }).ToList();
        }


        public List<PersonViewModel> Search(PersonSearchModel searchModel)
        {
            var Countries = _basicDataContext.Countrys.Select(x => new { x.Id, x.Name }).ToList();
            var Provinces = _basicDataContext.Provinces.Select(x => new { x.Id, x.Name }).ToList();
            var Cities = _basicDataContext.Citys.Select(x => new { x.Id, x.Name }).ToList();

            var query = _context.Persons
                .Include(x => x.Group)
                .Select(x => new PersonViewModel()
            {
                Id = x.Id,
                NameEn = x.NameEn,
                NameFa = x.NameFa,
                FamilyEn = x.FamilyEn,
                Activate = x.Activate,
                FamilyFa = x.FamilyFa,
                FatherName = x.FatherName,
                Birthday = x.Birthday.ToString(),
                PassportNo = x.PassportNo,
                NationalCode = x.NationalCode,
                PersonGroupId = x.PersonGroupId, 
                PersonGroup = x.Group.Description,
                OrganizationId = x.OrganizationId,
                BirthCityId = x.BirthCityId,
                BirthProvinceId = x.BirthProvinceId,
                BirthCountryId = x.BirthCountryId,
                WorkingStartDate = x.WorkingStartDate.ToString()
                
                });


            if(!string.IsNullOrWhiteSpace(searchModel.NameFa))
                query = query.Where(x => x.NameFa.Contains(searchModel.NameFa));

            if (!string.IsNullOrWhiteSpace(searchModel.NameEn))
                query = query.Where(x => x.NameEn.Contains(searchModel.NameEn));

            if (!string.IsNullOrWhiteSpace(searchModel.FamilyFa))
                query = query.Where(x => x.FamilyFa.Contains(searchModel.FamilyFa));

            if (!string.IsNullOrWhiteSpace(searchModel.FamilyEn))
                query = query.Where(x => x.FamilyEn.Contains(searchModel.FamilyEn));

            if (!string.IsNullOrWhiteSpace(searchModel.NationalCode))
                query = query.Where(x => x.NationalCode.Contains(searchModel.NationalCode));

            if (searchModel.PersonGroupId != 0)
                query = query.Where(x => x.PersonGroupId == searchModel.PersonGroupId);

            /////////////////////
            var persons = query.OrderByDescending(x => x.Id).ToList();
            persons.ForEach
                (persn => persn.BirthCountry = Countries.FirstOrDefault(x => x.Id == persn.BirthCountryId)?.Name);
            persons.ForEach
                (persn => persn.BirthProvince = Provinces.FirstOrDefault(x => x.Id == persn.BirthProvinceId)?.Name);
            persons.ForEach
                (persn => persn.BirthCity = Cities.FirstOrDefault(x => x.Id == persn.BirthCityId)?.Name);

            return query.ToList();
        }
    }
}
