using _0_Framework.Infrastructure;
using BasicDataManagement.Application.Contract.Entiti;
using BasicDataManagement.Domain.EntitiAgg;
using System.Collections.Generic;
using System.Linq;

namespace BasicDataManagement.Infrastructure.EFCore.Repository
{
    public class EntitiRepository : RepositoryBase<long, Entiti>, IEntitiRepository
    {
        private readonly BasicDataContext _context;
        public EntitiRepository(BasicDataContext context) : base(context)
        {
            _context = context;
        }


        public EditEntiti GetDetails(long id)
        {
            return _context.Entitis.Select(x => new EditEntiti()
            {
                Id = x.Id,
                Name = x.Name,
                Remark = x.Remark,
                Title = x.Title,
                IsActive = x.IsActive

            } ).FirstOrDefault(x => x.Id == id);
        }

        public List<EntitiViewModel> GetEntitis()
        {
            return _context.Entitis.Select(x => new EntitiViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Remark = x.Remark,
                Title = x.Title,
                IsActive = x.IsActive,
                CreationDate = x.CreationDate.ToString()

            }).ToList();
        }
    }
}
