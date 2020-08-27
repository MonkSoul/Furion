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
            var a = testDto.TryValidate();

            return a.ValidationResults.ToList();
        }
    }
}