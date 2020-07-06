using System.ComponentModel.DataAnnotations;

namespace Fur.Application.Functions.Dtos
{
    public class TestInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Age { get; set; }
    }
}