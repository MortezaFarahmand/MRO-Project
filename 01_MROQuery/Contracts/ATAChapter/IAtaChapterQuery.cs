
namespace _01_MROQuery.Contracts.ATAChapter
{
    public interface IAtaChapterQuery
    {
        List<AtaChapterQueryModel> GetAll();
        AtaChapterQueryModel GetById(int id);
        List<AtaChapterQueryModel> GetByIds(List<int> ids);
    }
}
