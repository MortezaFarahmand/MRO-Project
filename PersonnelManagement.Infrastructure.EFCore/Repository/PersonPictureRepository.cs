using _0_Framework.Infrastructure;
using PersonnelManagement.Application.Contracts.PersonPicture;
using PersonnelManagement.Domain.PersonPictureAgg;
using BasicDataManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PersonnelManagement.Infrastructure.EFCore.Repository
{
    public class PersonPictureRepository : RepositoryBase<long, PersonPicture>, IPersonPictureRepository
    {
        private readonly PersonnelContext _context;
        private readonly BasicDataContext _basicDataContext;
        public PersonPictureRepository(PersonnelContext context, BasicDataContext basicDataContext) : base(context)
        {
            _context = context;
            _basicDataContext = basicDataContext;
        }


        public EditPersonPicture GetDetails(long id)
        {
            return _context.PersonPictures.Select(x => new EditPersonPicture
            {
                Id = x.Id,
                //Picture = x.Picture,
                Text = x.Text,
                Title = x.Title,
                Remark = x.Remark,
                PictureCategoryId = x.PictureCategoryId,
                PersonId = x.PersonId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<PersonPictureViewModel> GetList()
        {
            var pictureCategory = _basicDataContext.PictureCategorys.Select(x => new {x.Id, x.Name}).ToList();

            var query = _context.PersonPictures
               .Include(x => x.Person)
               .Select(x => new PersonPictureViewModel
               {
                   Id = x.Id,
                   PersonId = x.PersonId,
                   Picture = x.Picture,
                   Text = x.Text,
                   Title = x.Title,
                   Remark = x.Remark,
                   IsActive = x.IsActive,
                   PictureCategoryId = x.PictureCategoryId,
                   Person = x.Person.NameEn + ' ' + x.Person.FamilyEn

               });
            
            var personPic = query.ToList();

            personPic.ForEach(pp => pp.PictureCategory = pictureCategory
            .FirstOrDefault(x => x.Id == pp.PictureCategoryId)?.Name);

            return personPic.ToList();
        }

        public List<PersonPictureViewModel> Search(PersonPictureSearchModel searchModel)
        {
            var pictureCategory = _basicDataContext.PictureCategorys.Select(x => new { x.Id, x.Name }).ToList();

            var query = _context.PersonPictures
               .Include(x => x.Person)
               .Select(x => new PersonPictureViewModel
               {
                   Id = x.Id,
                   PersonId = x.PersonId,
                   Picture = x.Picture,
                   Text = x.Text,
                   Title = x.Title,
                   Remark = x.Remark,
                   IsActive = x.IsActive,
                   PictureCategoryId = x.PictureCategoryId,
                   Person = x.Person.NameEn + ' ' + x.Person.FamilyEn

               });

            if(!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x => x.Title.Contains(searchModel.Title));

            if (searchModel.PictureCategoryId != 0)
                query = query.Where(x => x.PictureCategoryId == searchModel.PictureCategoryId);

            if(searchModel.PersonId != 0)
                query = query.Where(x => x.PersonId == searchModel.PersonId);

            ////////////////

            var personPic = query.OrderByDescending(x => x.Id).ToList();

            personPic.ForEach(pp => pp.PictureCategory = pictureCategory
            .FirstOrDefault(x => x.Id == pp.PictureCategoryId)?.Name);

            return personPic.ToList();
        }
    }
}
