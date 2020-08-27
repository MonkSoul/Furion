using System.ComponentModel.DataAnnotations;

namespace Fur.Application
{
    public class TestDto
    {
        [Range(10, 20)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}