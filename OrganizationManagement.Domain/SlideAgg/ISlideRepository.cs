using _0_Framework.Domain;
using OrganizationManagement.Application.Contracts.Slide;
using System.Collections.Generic;

namespace OrganizationManagement.Domain.SlideAgg
{
    public interface ISlideRepository : IRepository<long, Slide>
    {
        EditSlide GetDetails(long id);
        List<SlideViewModel> GetList();
    }
}
