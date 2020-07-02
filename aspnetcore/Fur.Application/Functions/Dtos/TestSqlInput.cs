using System.ComponentModel.DataAnnotations;

namespace Fur.Application.Functions.Dtos
{
    public class TestSqlInput
    {
        [Required]
        public string Sql { get; set; }
    }
}
