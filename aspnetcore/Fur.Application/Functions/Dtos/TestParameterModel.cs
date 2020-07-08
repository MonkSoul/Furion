using Fur.DatabaseVisitor.Attributes;

namespace Fur.Application.Functions.Dtos
{
    public class TestParameterModel
    {
        public string Name { get; set; }

        [DbParameter(nameof(ReturnValue) + "a", Direction = System.Data.ParameterDirection.ReturnValue)]
        public int ReturnValue { get; set; }
    }
}
