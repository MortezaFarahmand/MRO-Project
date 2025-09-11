
using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace BasicDataManagement.Application.Contracts.Entiti
{
    public class CreateEntiti
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }

        public string Title { get; set; }

        public string Remark { get; set; }
    }
}
