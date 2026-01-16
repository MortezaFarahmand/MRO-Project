using _0_Framework.Infrastructure;
using BasicDataManagement.Application.Contracts.PictureCategory;
using BasicDataManagement.Domain.EntitiAgg;
using BasicDataManagement.Domain.PictureCategoryAgg;
using System.Collections.Generic;
using System.Linq;

namespace BasicDataManagement.Infrastructure.EFCore.Repository
{
    public class PictureCategoryRepository : RepositoryBase<long, PictureCategory>, IPictureCategoryRepository
    {
        private readonly BasicDataContext _context;
        public PictureCategoryRepository(BasicDataContext context) : base(context)
        {
            _context = context;
        }


        
        public EditPictureCategory GetDetails(long id)
        {
            return _context.PictureCategorys.Select(x => new EditPictureCategory
            {
                Id = x.Id,
                Name = x.Name,
                Remark = x.Remark,
                EntitiId = x.EntitiId

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<PictureCategoryViewModel> GetPictureCategory()
        {
            return _context.PictureCategorys.Select(x => new PictureCategoryViewModel
            {
                Id = x.Id,
                Name= x.Name,
                Remark= x.Remark,
                EntitiId= x.EntitiId,
                IsActive = x.IsActive
            }).ToList();
        }

        public string GetPictureCategoryNameById(long id)
        {
            return  _context.PictureCategorys.Select(x => new { x.Id, x.Name }).FirstOrDefault(x => x.Id == id).Name;
        }

        public List<PictureCategoryViewModel> Search(PictureCategorySearchModel searchModel)
        {
            var Entities = _context.Entitis.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.PictureCategorys.Select(x => new PictureCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Remark = x.Remark,
                EntitiId = x.EntitiId,
                IsActive = x.IsActive
            });

            if(searchModel.EntitiId != 0)
                query = query.Where(x => x.EntitiId == searchModel.EntitiId);

            /////////////////////
            var picCategory = query.OrderByDescending(x => x.Id).ToList();
            picCategory.ForEach
                (picCategory => picCategory.Entiti = Entities.FirstOrDefault(x => x.Id == picCategory.EntitiId)?.Name);
            ///////////////////////
            return picCategory;
        }
    }
}
