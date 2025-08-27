namespace BasicDataManagement.Application.Contract.Entiti
{
    public class EditEntiti : CreateEntiti
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
    }
}
