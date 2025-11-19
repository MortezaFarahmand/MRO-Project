using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BasicDataManagement.Application.Contracts.Slide;
using BasicDataManagement.Domain.SlideAgg;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicDataManagement.Infrastructure.EFCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly BasicDataContext _context;

        public SlideRepository(BasicDataContext context) : base(context)
        {
            _context = context;
        }



        public EditSlide GetDetails(long id)
        {
            return _context.Slides.Select(x => new EditSlide
            {
                Id = x.Id,
                BtnText = x.BtnText,
                Heading = x.Heading,
                //Picture = x.Picture,
                //PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Text = x.Text,
                Link = x.Link,
                Title = x.Title
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<SlideViewModel> GetList()
        {
            return _context.Slides.Select(x => new SlideViewModel
            {
                Id = x.Id,
                Heading = x.Heading,
                Picture = x.Picture,
                Title = x.Title,
                IsRemoved = x.IsRemoved,
                CreationDate = x.CreationDate.ToString("dd/MM/yyyy")
            }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
