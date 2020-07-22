using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fur.Validation
{
    public interface IValidateObject
    {
        (bool isVaild, ICollection<ValidationResult> validationResults) TryValidateObject(object obj, bool validateAllProperties = true);

        (bool isVaild, ICollection<ValidationResult> validationResults) TryValidateValue(object value, IEnumerable<ValidationAttribute> validationAttributes);
    }
}