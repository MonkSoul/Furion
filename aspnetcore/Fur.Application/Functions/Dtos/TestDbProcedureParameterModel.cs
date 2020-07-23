using Fur.DatabaseAccessor.Attributes;
using System.Data;

namespace Fur.Application.Functions.Dtos
{
    public class TestDbProcedureParameterModel
    {
        public string Name { get; set; }

        [DbParameter(ParameterDirection.Output)]
        public int OutputValue { get; set; }

        [DbParameter(ParameterDirection.ReturnValue)]
        public int ReturnValue { get; set; }
    }
}
