using Fur.DataValidation;

namespace Fur.Application
{
    public class TestDto
    {
        public int Id { get; set; }

        [DataValidation(ValidationOptions.AtLeastOne, ValidationTypes.Chinese, ValidationTypes.Date)]
        public string Name { get; set; }
    }
}