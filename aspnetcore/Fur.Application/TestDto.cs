using System.ComponentModel.DataAnnotations;

namespace Fur.Application
{
    public class TestDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}