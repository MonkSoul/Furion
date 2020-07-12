using Fur.Application.Functions.Dtos;
using Fur.DatabaseVisitor.Tangent;
using Fur.DatabaseVisitor.Tangent.Attributes;
using Fur.Record.Entities;
using System.Collections.Generic;
using System.Data;

namespace Fur.Application.Functions
{
    public interface INonDbSetQuery : ITangentProxyDependency
    {
        [DbQuery("select top 20 * from tests where id > @id; select top 20 * from tests;", SourceType = (typeof((IEnumerable<Test>, IEnumerable<Test>))))]
        (IEnumerable<TestOutput>, IEnumerable<TestOutput>) GetDataTable(int id);

        [DbProcedure("dbo.PR_GetTest")]
        DataTable Execute(string name);
    }
}