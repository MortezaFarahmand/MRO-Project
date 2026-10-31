using _0_Framework.Domain;

namespace PartManagement.Domain.PartAgg
{
    public interface IPartRepository : IRepository<long, Part>
    {
        Part GetByPartNumber(string partNumber);
        bool Exists(string partNumber);
    }
}