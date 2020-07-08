using Fur.DatabaseVisitor.Attributes;
using System.Data;

namespace Fur.Application.Functions.Dtos
{
    public class TestDbProcedureParameterModel
    {
        public string Name { get; set; }

        [DbProcedureParameter(ParameterDirection.Output)]
        public int OutputValue { get; set; }

        [DbProcedureParameter(ParameterDirection.ReturnValue)]
        public int ReturnValue { get; set; }
    }
}
