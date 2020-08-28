using Fur.DataValidation;
using Fur.DynamicApiController;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        public int Get(int id)
        {
            return id;
        }

        public List<ValidationResult> Post(TestDto testDto)
        {
            var validate = "".TryValidate(ValidationTypes.Required);

            return validate.ValidationResults.ToList();
        }
    }
}