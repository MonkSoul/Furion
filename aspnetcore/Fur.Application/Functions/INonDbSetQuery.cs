using Fur.Application.Functions.Dtos;
using Fur.Core.DbEntities;
using Fur.DatabaseAccessor.Tangent.Attributes;
using Fur.DatabaseAccessor.Tangent.Dependencies;
using System.Collections.Generic;
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