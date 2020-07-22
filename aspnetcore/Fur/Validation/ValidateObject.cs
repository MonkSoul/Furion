using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fur.Validation
{
    public class ValidateObject : IValidateObject
    {
        public (bool isVaild, ICollection<ValidationResult> validationResults) TryValidateObject(object obj, bool validateAllProperties = true)
        {
            ICollection<ValidationResult> results = new List<ValidationResult>(); ;
            var isVaild = Validator.TryValidateObject(obj, new ValidationContext(obj), results, validateAllProperties);
            return (isVaild, results);
        }

        public (bool isVaild, ICollection<ValidationResult> validationResults) TryValidateValue(object value, IEnumerable<ValidationAttribute> validationAttributes)
        {
            ICollection<ValidationResult> results = new List<ValidationResult>(); ;
            var isVaild = Validator.TryValidateValue(value, new ValidationContext(value), results, validationAttributes);
            return (isVaild, results);
        }
    }
}