using Fur.Application.Functions.Dtos;
using Fur.DatabaseVisitor.Tangent;
using Fur.DatabaseVisitor.Tangent.Attributes;
using Fur.Record.Entities;
using System.Collections.Generic;

namespace Fur.Application.Functions
{
    public interface INonDbSetQuery : ITangentQueryDependency
    {
        [DbSentence("select * from tests", SourceType = typeof(IEnumerable<Test>))]
        IEnumerable<TestOutput> GetTests();

        [DbProcedure("dbo.PR_GetTest")]
        IEnumerable<TestOutput> GetPRTests(string name);

        [DbScalarFunction("dbo.FN_GetId")]
        int GetSFById(int id);

        [DbTableFunction("dbo.FN_GetTable")]
        IEnumerable<TestOutput> GetFNTests(int id);
    }
}
