
namespace BasicDataManagement.Infrastructure.EFCore.Repository.DocumentSequence
{
    public class DocumentSequence
    {
        public long Id { get; set; }
        public string DocumentType { get; set; }
        public int Year { get; set; }
        public int? BaseId { get; set; }
        public long LastValue { get; set; }
    }
}
