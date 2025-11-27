namespace PersonnelManagement.Application.Contracts.PersonPicture
{
    public class PersonPictureViewModel
    {
        public long Id { get; set; }
        public string Picture { get; set; }
        public string Title { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public long PictureCategoryId { get; set; }
        public long PersonId { get; set; }
    }


}
