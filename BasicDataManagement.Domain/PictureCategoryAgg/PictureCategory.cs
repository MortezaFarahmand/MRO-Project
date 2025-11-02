using _0_Framework.Domain;
using BasicDataManagement.Domain.EntitiAgg;

namespace BasicDataManagement.Domain.PictureCategoryAgg
{
    public class PictureCategory : EntityBase
    {
        public string  Name { get; private set; }
        public bool IsActive { get; private set; }
        public string Remark { get; private set; }
        public long EntitiId { get; private set; }
        public Entiti Entiti { get; private set; }


        public PictureCategory(string name, string remark, long entitiId)
        {
            Name = name;
            IsActive = true;
            Remark = remark;
            EntitiId = entitiId;

        }

        public void Edit(string name, string remark, long entitiId)
        {
            Name = name;
            Remark = remark;
            EntitiId = entitiId;
        }

        public void DeActive()
        { 
            IsActive = false;
        }

        public void Active()
        {
            IsActive = true;
        }
    }
}
