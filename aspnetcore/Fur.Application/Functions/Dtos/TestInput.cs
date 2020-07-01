using System.ComponentModel.DataAnnotations;

namespace Fur.Application.Functions.Dtos
{
    public class TestInput
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
    }
}
