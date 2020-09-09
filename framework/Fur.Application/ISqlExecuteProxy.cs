using Fur.Core;
using Fur.DatabaseAccessor;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Fur.Application
{
    public interface ISqlExecuteProxy : ISqlDispatchProxy
    {
        [SqlExecute("select * from dbo.Test where Id > @id")]
        DataTable GetDataTable(int id);

        [SqlExecute(@"
select * from dbo.test where id > @id;
select top 10 * from dbo.test;
select Id,Name,Age,Address from dbo.test where id > @id;")]
        DataSet GetDataSet(int id);

        [SqlExecute(@"
select * from dbo.test where id > @id;
select top 10 * from dbo.test;
select Id,Name,Age,Address from dbo.test where id > @id;")]
        Task<DataSet> GetDataSetAsync(int id);

        [SqlExecute(@"
select * from dbo.test where id > @id;
select top 10 * from dbo.test;
select Id,Name,Age,Address from dbo.test where id > @id;")]
        (List<Test>, List<Test>, List<TestDto>) GetDataSetTuple(int id);

        [SqlExecute("select top 1 Name from dbo.Test;")]
        string GetScalarName();

        [SqlProduce("PROC_Output")]
        ProcedureOutputResult<(List<Test>, List<Test>)> GetOutput(ProcOutputParameter parameter);

        [SqlFunction("dbo.FN_GetName")]
        string GetScalarFunction(string name);

        [SqlFunction("dbo.FN_GetTest")]
        List<Test> GetTableFunction(int id);
    }
}