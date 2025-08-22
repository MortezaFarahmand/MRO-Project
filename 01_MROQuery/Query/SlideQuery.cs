using _01_MROQuery.Contracts.Slides;
using OrganizationManagement.Infrastructure.EFCore;

namespace _01_MROQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly OrganizationContext _organizationContext;
        public SlideQuery(OrganizationContext organizationContext)
        {
            _organizationContext = organizationContext;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _organizationContext.Slides
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
