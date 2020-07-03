using Fur.Application.Functions.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fur.Application.Functions
{
    public interface ITestAppService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TestOutput>> GetAsync();

        /// <summary>
        /// 搜索数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<TestOutput>> SearchAsync(TestSearchInput input);

        /// <summary>
        /// 关键字搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<IEnumerable<TestOutput>> SearchAsync(string keyword);

        /// <summary>
        /// 查询一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TestOutput> GetAsync(int id);

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> InsertAsync(TestInput input);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(int id, TestInput input);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// 原始Sql查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<TestOutput>> SqlQueryAsync(TestSqlInput input);

        /// <summary>
        /// 原始Sql DataSet 查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<(IEnumerable<TestOutput>, IEnumerable<TestOutput>)> SqlDatasetQueryAsync(TestSqlInput input);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IEnumerable<TestOutput>> SqlProcedureQueryAsync(string name);

        /// <summary>
        /// 调用数据库标量函数
        /// </summary>
        /// <returns></returns>
        Task<int> SqlScalarFunctionQueryAsync();

        /// <summary>
        /// 调用数据库表值函数
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TestOutput>> SqlTableFunctionQueryAsync();

        /// <summary>
        /// 查询视图
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TestOutput>> SqlViewQueryAsync();

        /// <summary>
        /// Linq中调用函数
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TestOutput>> GetLinqFunctionAsync();
    }
}
