using _0_Framework.Domain;
using BasicDatanManagement.Application.Contracts.Slide;
using System.Collections.Generic;

namespace BasicDatanManagement.Domain.SlideAgg
{
    public interface ISlideRepository : IRepository<long, Slide>
    {
        EditSlide GetDetails(long id);
        List<SlideViewModel> GetList();
    }
}
