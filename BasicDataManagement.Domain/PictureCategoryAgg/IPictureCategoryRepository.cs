using _0_Framework.Domain;
using BasicDataManagement.Application.Contracts.PictureCategory;
using System.Collections.Generic;

namespace BasicDataManagement.Domain.PictureCategoryAgg
{
    public interface IPictureCategoryRepository : IRepository<long, PictureCategory>
    {
        List<PictureCategoryViewModel> GetPictureCategory();
        EditPictureCategory GetDetails(long id);
        List<PictureCategoryViewModel> Search(PictureCategorySearchModel searchModel);
    }
}
