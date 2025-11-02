namespace BasicDataManagement.Application.Contracts.PictureCategory
{
    public class PictureCategoryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public string CreationDate { get; set; }
        public long EntitiId { get; set; }
        public string Entiti { get; set; }
        public bool IsActive { get; set; }
    }
}
