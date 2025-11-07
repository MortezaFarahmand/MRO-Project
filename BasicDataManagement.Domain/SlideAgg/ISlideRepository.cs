using _0_Framework.Domain;
using BasicDataManagement.Application.Contracts.Slide;
using System.Collections.Generic;

namespace BasicDataManagement.Domain.SlideAgg
{
    public interface ISlideRepository : IRepository<long, Slide>
    {
        EditSlide GetDetails(long id);
        List<SlideViewModel> GetList();
    }
}
