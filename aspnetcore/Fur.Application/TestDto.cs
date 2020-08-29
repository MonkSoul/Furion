using Fur.DataValidation;

namespace Fur.Application
{
    public class TestDto
    {
        public int Id { get; set; }

        [DataValidate(ValidationLogicOptions.Or, ValidationTypes.Chinese, ValidationTypes.Date)]
        public string Name { get; set; }
    }
}