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
            var b = "2".TryValidate(ValidationTypes.Required);

            var a = testDto.TryValidate();

            return a.ValidationResults.ToList();
        }
    }
}