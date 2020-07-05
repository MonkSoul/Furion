using Fur.Application.Functions.Dtos;
using Fur.AttachController.Attributes;
using Fur.AttachController.Dependencies;
using Fur.DatabaseVisitor.Page;
using Fur.DatabaseVisitor.Repositories;
using Fur.DatabaseVisitor.Tangent;
using Fur.Extensions;
using Fur.FriendlyException;
using Fur.FriendlyException.Attributes;
using Fur.Linq.Extensions;
using Fur.Record;
using Fur.Record.Entities;
using Fur.Record.Identifiers;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.Application.Functions
{
    /// <summary>
    /// 测试接口
    /// </summary>
    [AttachController]
    public class TestAppService : ITestAppService, IAttachControllerDependency
    {
        private readonly IRepository _repository;
        private readonly IRepositoryOfT<Test> _testRepository;
        private readonly IRepositoryOfT<V_Test> _vTestRepository;
        private readonly INonDbSetQuery _nonDbSetQuery;
        private readonly IMultipleRepositoryOfT<Test, FurMultipleDbContextIdentifier> _multipleRepository;
        private readonly IMultipleRepositoryOfT<V_Test, FurMultipleDbContextIdentifier> _vTestMultipleRepository;

        public TestAppService(IRepository repository
            , IRepositoryOfT<Test> testRepository
            , IRepositoryOfT<V_Test> vTestRepository
            , ITangentDbContext tangentDbContext
            , IMultipleRepositoryOfT<Test, FurMultipleDbContextIdentifier> multipleRepository
            , IMultipleRepositoryOfT<V_Test, FurMultipleDbContextIdentifier> vTestMultipleRepository)
        {
            _repository = repository;
            _testRepository = testRepository;
            _vTestRepository = vTestRepository;
            _nonDbSetQuery = tangentDbContext.For<INonDbSetQuery>();
            _multipleRepository = multipleRepository;
            _vTestMultipleRepository = vTestMultipleRepository;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<TestOutput>> GetAsync()
        {
            return await _testRepository.Entity
                .ProjectToType<TestOutput>()
                .ToListAsync();
        }

        /// <summary>
        /// 分页查询所有
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IPagedListOfT<TestOutput>> GetAsync(int page = 0, int page_size = 20)
        {
            var pageList = await _testRepository.GetPageAsync(page, page_size);
            return pageList.Adapt<IPagedListOfT<TestOutput>>();
        }

        /// <summary>
        /// 搜索数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AttachAction(KeepRouteVerb = true)]
        [HttpPost]
        public async Task<IEnumerable<TestOutput>> SearchAsync([Required] TestSearchInput input)
        {
            input = input ?? throw new InvalidOperationException("非法操作：搜索条件为空。");

            return await _testRepository.Get()
                    .WhereIf(input.Id.HasValue, u => u.Id == input.Id.Value)
                    .WhereIf(input.Name.HasValue(), u => u.Name.Contains(input.Name))
                    .ProjectToType<TestOutput>()
                    .ToListAsync();
        }

        /// <summary>
        /// 关键字搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [AttachAction(KeepRouteVerb = true)]
        public async Task<IEnumerable<TestOutput>> SearchAsync(string keyword)
        {
            return await _testRepository.Get(true)
                    .Where(u => u.Name.Contains(keyword))
                    .ProjectToType<TestOutput>()
                    .ToListAsync();
        }

        /// <summary>
        /// 查询一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<TestOutput> GetAsync(int id)
        {
            var entity = await _testRepository.FindAsync(id);
            return entity.Adapt<TestOutput>();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[IfException(2000, "我自己抛出的错误")]
        public async Task<int> InsertAsync(TestInput input)
        {
            var entity = input.Adapt<Test>();
            var newEntity = await _testRepository.InsertAsync(entity);

            //throw Oops.Set(2000);

            return newEntity.Entity.Id;
        }

        /// <summary>
        /// 更新所有列数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateAsync(int id, TestInput input)
        {
            var entity = await _testRepository.FindAsync(id) ?? throw new InvalidOperationException("非法操作：没找到数据。");
            input.Adapt(entity);

            await _testRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 更新指定列（只更新Name列）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [AttachAction(EveryWordToRoutePath = true)]
        public async Task UpdateIncludeProperties(int id, TestInput input)
        {
            var entity = await _testRepository.FindAsync(id) ?? throw new InvalidOperationException("非法操作：没找到数据。");
            input.Adapt(entity);

            entity.CreatedTime = DateTime.Now; // 测试代码，不会更新

            await _testRepository.UpdateIncludePropertiesAsync(entity, u => u.Name);
        }

        /// <summary>
        /// 排除指定列更新（除了Name列都更新）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [AttachAction(EveryWordToRoutePath = true)]
        public async Task UpdateExcludeProperties(int id, TestInput input)
        {
            var entity = await _testRepository.FindAsync(id) ?? throw new InvalidOperationException("非法操作：没找到数据。");
            input.Adapt(entity);

            entity.CreatedTime = DateTime.Now; // 测试代码，不会更新

            await _testRepository.UpdateExcludePropertiesAsync(entity, u => u.Name);
        }

        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AttachAction(KeepOriginalName = true)]
        public async Task<TestOutput> InsertOrUpdateAsync(TestOutput input)
        {
            var entity = input.Adapt<Test>();
            var trackEntity = await _testRepository.InsertOrUpdateAsync(entity);
            return trackEntity.Adapt<TestOutput>();
        }

        /// <summary>
        /// 真删除数据
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var entity = await _testRepository.FindAsync(id) ?? throw new InvalidOperationException("非法操作：没找到数据。");

            await _testRepository.DeleteAsync(entity);
        }

        /// <summary>
        /// 假删除数据
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        [AttachAction(EveryWordToRoutePath = true)]
        public Task FakeDeleteAsync(int id)
        {
            return _testRepository.FakeDeleteAsync(id, u => u.IsDeleted, true);
        }

        /// <summary>
        /// 原始Sql查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AttachAction(EveryWordToRoutePath = true)]
        public Task<IEnumerable<TestOutput>> SqlQueryAsync(TestSqlInput input)
        {
            return _testRepository.FromSqlQueryAsync<TestOutput>(input.Sql);
        }

        /// <summary>
        /// 原始Sql DataSet 查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AttachAction(EveryWordToRoutePath = true)]
        public Task<(IEnumerable<TestOutput>, IEnumerable<TestOutput>)> SqlDatasetQueryAsync(TestSqlInput input)
        {
            return _testRepository.FromSqlDataSetQueryAsync<TestOutput, TestOutput>(input.Sql);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        // ================================
        //    CREATE PROCEDURE PR_GetTest @Name NVARCHAR(32)
        //    AS
        //    BEGIN
        //        SELECT Id,
        //               Name,
        //               Age
        //        FROM dbo.Tests
        //        WHERE Name LIKE '%' + @Name + '%';
        //    END;
        // ================================
        [AttachAction(EveryWordToRoutePath = true)]
        public Task<IEnumerable<TestOutput>> SqlProcedureQueryAsync(string name)
        {
            return _testRepository.FromSqlProcedureQueryAsync<TestOutput>(name, new { Name = "小僧" });
        }

        /// <summary>
        /// 调用数据库标量函数
        /// </summary>
        /// <returns></returns>
        // ================================
        //   CREATE FUNCTION FN_GetId
        //  (
        //      @id INT
        //  )
        //  RETURNS INT
        //  AS
        //  BEGIN
        //      RETURN @id + 1;
        //  END;
        // ================================
        [AttachAction(EveryWordToRoutePath = true)]
        public Task<int> SqlScalarFunctionQueryAsync()
        {
            return _testRepository.FromSqlScalarFunctionQueryAsync<int>("dbo.FN_GetId", new { Id = 1 });
        }

        /// <summary>
        /// 调用数据库表值函数
        /// </summary>
        /// <returns></returns>
        // ================================
        //   CREATE FUNCTION FN_GetTable
        //  (
        //      @id INT
        //  )
        //  RETURNS TABLE
        //  AS
        //  RETURN
        //  (
        //      SELECT Id,
        //             Name,
        //             Age
        //      FROM dbo.Tests
        //      WHERE Id > @id
        //  );
        // ================================
        [AttachAction(EveryWordToRoutePath = true)]
        public Task<IEnumerable<TestOutput>> SqlTableFunctionQueryAsync()
        {
            return _testRepository.FromSqlTableFunctionQueryAsync<TestOutput>("dbo.FN_GetTable", new { Id = 5 });
        }

        /// <summary>
        /// 查询视图
        /// </summary>
        /// <returns></returns>
        // ================================
        //  CREATE VIEW V_Test
        //  AS
        //  SELECT Id,Name,Age FROM Tests;
        // ================================
        [AttachAction(EveryWordToRoutePath = true)]
        public async Task<IEnumerable<TestOutput>> SqlViewQueryAsync()
        {
            return await _vTestRepository.Entity
                .ProjectToType<TestOutput>()
                .ToListAsync();
        }

        /// <summary>
        /// Linq中调用函数
        /// </summary>
        /// <returns></returns>
        [AttachAction(EveryWordToRoutePath = true)]
        public async Task<IEnumerable<TestOutput>> GetLinqFunctionAsync()
        {
            return await _testRepository.Get(true)
                .Where(u => u.Id >= LinqDbFunctions.GetId(0))
                .ProjectToType<TestOutput>()
                .ToListAsync();
        }

        /// <summary>
        /// 测试仓储实例生命周期
        /// </summary>
        /// <returns></returns>
        [AttachAction(KeepOriginalName = true)]
        public Task<bool> TestRepositoryScopeLifetime()
        {
            var testRepository = _repository.GetRepository<Test>();

            return Task.FromResult(testRepository == _testRepository);
        }

        /// <summary>
        /// 切面上下文读取
        /// </summary>
        /// <returns></returns>
        [AttachAction(KeepOriginalName = true)]
        public Task<IEnumerable<TestOutput>> GetTestsByTangent()
        {
            return _nonDbSetQuery.GetTestsAsync();
        }

        /// <summary>
        /// 切面执行存储过程
        /// </summary>
        /// <returns></returns>
        [AttachAction(KeepOriginalName = true)]
        public Task<IEnumerable<TestOutput>> GetTestsByTangentPR()
        {
            //var result = _nonDbSetQuery.GetPRTests("小僧");
            //return Task.FromResult(result);

            return _nonDbSetQuery.GetPRTestsAsync("小僧");
        }

        /// <summary>
        /// 切面执行标量函数
        /// </summary>
        /// <returns></returns>
        [AttachAction(KeepOriginalName = true)]
        public Task<int> GetIdTangentSF()
        {
            return _nonDbSetQuery.GetSFByIdAsync(0);
        }

        /// <summary>
        /// 切面执行表值函数
        /// </summary>
        /// <returns></returns>
        [AttachAction(KeepOriginalName = true)]
        public Task<IEnumerable<TestOutput>> GetTestsByTangentTR()
        {
            return _nonDbSetQuery.GetFNTestsAsync(1);
        }

        //[AttachAction(KeepOriginalName = true)]
        //public async Task<string> GetsDataSet()
        //{
        //    var dataset = await _nonDbSetQuery.GetTestsDataSetAsync();

        //    return JsonConvert.SerializeObject(dataset);

        //    //return _nonDbSetQuery.GetTestsDataSetAsync();
        //}
    }
}