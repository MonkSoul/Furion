using Fur.Application.Functions.Dtos;
using Fur.DatabaseVisitor.Tangent;
using Fur.DatabaseVisitor.Tangent.Attributes;
using Fur.Record.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [DbSentence("select * from tests", SourceType = typeof(Task<IEnumerable<Test>>))]
        Task<IEnumerable<TestOutput>> GetTestsAsync();

        [DbProcedure("dbo.PR_GetTest")]
        Task<IEnumerable<TestOutput>> GetPRTestsAsync(string name);

        [DbScalarFunction("dbo.FN_GetId")]
        Task<int> GetSFByIdAsync(int id);

        [DbTableFunction("dbo.FN_GetTable")]
        Task<IEnumerable<TestOutput>> GetFNTestsAsync(int id);

        //[DbSentence("select * from tests;")]
        //Task<DataTable> GetTestsDataSetAsync();

        //[DbSentence("select * from tests;select top 20 * from tests;")]
        //Task<DataSet> GetTestsDataSetAsync();

        //[DbSentence("select * from tests;")]
        //Task<IEnumerable<Test>> GetTestsDataSetAsync();

        //[DbSentence("select * from tests;", SourceType = typeof(Task<IEnumerable<Test>>))]
        //Task<IEnumerable<TestOutput>> GetTestsDataSetAsync();

        //[DbSentence("select * from tests;select top 20 * from tests;")]
        //Task<(IEnumerable<Test>, IEnumerable<Test>)> GetTestsDataSetAsync();

        //[DbSentence("select * from tests;select top 20 * from tests;", SourceType = typeof(Task<(IEnumerable<Test>, IEnumerable<Test>)>))]
        //Task<(IEnumerable<TestInput>, IEnumerable<TestInput>)> GetTestsDataSetAsync();

        //[DbSentence("select * from tests;")]
        //Task GetTestsDataSetAsync();
    }
}