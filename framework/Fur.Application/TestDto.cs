using Fur.DataValidation;

namespace Fur.Application
{
    public class TestDto
    {
        public int? Id { get; set; }

        [DataValidation(ValidationTypes.Chinese)]
        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }
    }
}