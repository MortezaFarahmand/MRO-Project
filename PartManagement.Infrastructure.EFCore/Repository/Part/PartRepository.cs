using System.Linq.Expressions;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PartManagement.Domain.PartAgg;

namespace PartManagement.Infrastructure.EFCore.Repository
{
    public class PartRepository : RepositoryBase<long, Part>, IPartRepository
    {
        private readonly PartContext _context;

        public PartRepository(PartContext context) : base(context)
        {
            _context = context;
        }

        public Part GetByPartNumber(string partNumber)
        {
            return _context.Parts
                .FirstOrDefault(x => x.PartNumber == partNumber);
        }

        public bool Exists(string partNumber)
        {
            return _context.Parts
                .Any(x => x.PartNumber == partNumber);
        }

        // برای بارگذاری کامل با روابط (در صورت نیاز)
        public Part GetWithDetails(long id)
        {
            return _context.Parts
                .Include(x => x.Manufacturers)
                .Include(x => x.Organizations)
                .Include(x => x.Children)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}