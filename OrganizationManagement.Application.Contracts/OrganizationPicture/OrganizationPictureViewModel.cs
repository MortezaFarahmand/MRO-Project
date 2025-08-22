
namespace OrganizationManagement.Application.Contracts.OrganizationPicture
{
    public class OrganizationPictureViewModel
    {
        public long Id { get; set; }
        public string Organization { get; set; }
        public string Picture { get; set;}
        public string CreationDate { get; set;}
        public long OrganizationId { get; set; }
        public bool IsRemoved { get; set; }
    }
}
