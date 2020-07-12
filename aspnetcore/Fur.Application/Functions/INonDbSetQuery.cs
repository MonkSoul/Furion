using Fur.Application.Functions.Dtos;
using Fur.DatabaseVisitor.Tangent.Attributes;
using Fur.DatabaseVisitor.Tangent.Entities;
using Fur.Record.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.Application.Functions
{
    public interface INonDbSetQuery : ITangentProxyDependency
    {
        [DbQuery("select top 20 * from tests where id > @id; select top 20 * from tests;", DbExecuteType = (typeof((IEnumerable<Test>, IEnumerable<Test>))))]
        (IEnumerable<TestOutput>, IEnumerable<TestOutput>) GetDataTable(int id);

        [DbProcedure("dbo.PR_GetTest")]
        Task Execute(string name);
    }
}