using _0_Framework.Application;
using System.Collections.Generic;

namespace BasicDataManagement.Application.Contracts.PictureCategory
{
    public interface IPictureCategoryApplication
    {
        OperationResult Create(CreatePictureCategory command);
        OperationResult Edit(EditPictureCategory command);
        OperationResult Active(long id);
        OperationResult DeActive(long id);
        EditPictureCategory GetDetails(long id);
        List<PictureCategoryViewModel> Search(PictureCategorySearchModel searchModel);
        List<PictureCategoryViewModel> GetPictureCategorys();
    }
}
