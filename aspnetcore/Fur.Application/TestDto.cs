using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fur.Application
{
    public class TestDto : IValidatableObject
    {
        [Range(10, 20, ErrorMessage = "Id 只能在 10-20 区间取值")]
        public int Id { get; set; }

        [Required(ErrorMessage = "必填"), MinLength(3, ErrorMessage = "字符串长度不能少于3位")]
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name.StartsWith("Fur"))
            {
                yield return new ValidationResult(
                    "不能以 Fur 开头"
                    , new[] { nameof(Name) }
                );
            }
        }
    }
}