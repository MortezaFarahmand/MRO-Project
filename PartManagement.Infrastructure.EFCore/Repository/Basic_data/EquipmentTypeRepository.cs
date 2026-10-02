using _0_Framework.Infrastructure;
using PartManagement.Application.Contracts.Basic_data.EquipmentType;
using PartManagement.Domain.Basic_data.EquipmentTypeAgg;

namespace PartManagement.Infrastructure.EFCore.Repository.Basic_data
{
    public class EquipmentTypeRepository : RepositoryBase<long, EquipmentType>, IEquipmentTypeRepository
    {
        public readonly PartContext _context;
        public EquipmentTypeRepository(PartContext context) : base(context)
        {
            _context = context;
        }


        public EditEquipmentType Getdetails(long id)
        {
            return _context.EquipmentTypes.Select(x => new EditEquipmentType() {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Code = x.Code,
                ColorCode = x.ColorCode,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToString()
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<EquipmentTypeViewModel> GetEquipmentTypes()
        {
            return _context.EquipmentTypes.Select(x => new EquipmentTypeViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
            }).ToList();
        }

        public List<EquipmentTypeViewModel> Search(EquipmentTypeSearchModel searchModel)
        {
            var query = _context.EquipmentTypes.Select(x => new EquipmentTypeViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Picture = x.Picture,
                ColorCode = x.ColorCode,
                IsActive = x.IsActive,
                CreationDate = x.CreationDate.ToString()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code.Contains(searchModel.Code));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
