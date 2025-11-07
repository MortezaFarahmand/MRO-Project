using _01_MROQuery.Contracts.Slides;
using BasicDataManagement.Infrastructure.EFCore;

namespace _01_MROQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly BasicDataContext _basicDataContext;
        public SlideQuery(BasicDataContext basicDataContext)
        {
            _basicDataContext = basicDataContext;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _basicDataContext.Slides
                .Where(x => x.IsRemoved == false)
                .Select(x => new SlideQueryModel
                {
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    BtnText = x.BtnText,
                    Heading = x.Heading,
                    Text = x.Text,
                    Link = x.Link,
                    Title = x.Title
                })
              .ToList();
        }
    }
}
