using Fur.Application.Functions.Dtos;
using Fur.Core;
using Fur.Core.DbContextIdentifiers;
using Fur.Core.DbEntities;
using Fur.DatabaseVisitor.Attributes;
using Fur.DatabaseVisitor.Entities;
using Fur.DatabaseVisitor.Identifiers;
using Fur.DatabaseVisitor.Repositories;
using Fur.DatabaseVisitor.Repositories.MasterSlave;
using Fur.DatabaseVisitor.Repositories.Multiples;
using Fur.DatabaseVisitor.Tangent;
using Fur.FriendlyException;
using Fur.Linq.Extensions;
using Fur.MirrorController.Attributes;
using Fur.MirrorController.Dependencies;
using Fur.TypeExtensions;
using Fur.Validation.Attributes;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.Application.Functions
{
    /// <summary>
    /// 测试接口
    /// </summary>
    [MirrorController]
    public class TestAppService : ITestAppService, IMirrorControllerDependency
    {
        private readonly IRepository _repository;
        private readonly IRepositoryOfT<Test> _testRepository;
        private readonly IRepositoryOfT<V_Test> _vTestRepository;
        private readonly IMasterSlaveRepositoryOfT<Test, FurDbContextIdentifier, FurMultipleDbContextIdentifier> _masterSlaveRepository;

        private readonly IMultipleRepositoryOfT<Test, FurMultipleDbContextIdentifier> _multipleRepository;
        private readonly IMultipleRepositoryOfT<V_Test, FurMultipleDbContextIdentifier> _vTestMultipleRepository;

        private readonly ITangentDbContextOfT<INonDbSetQuery> _tangent;

        public TestAppService(IRepository repository
            , IRepositoryOfT<Test> testRepository
            , IRepositoryOfT<V_Test> vTestRepository

            , IMultipleRepositoryOfT<Test, FurMultipleDbContextIdentifier> multipleRepository
            , IMultipleRepositoryOfT<V_Test, FurMultipleDbContextIdentifier> vTestMultipleRepository

            , IMasterSlaveRepositoryOfT<Test, FurDbContextIdentifier, FurMultipleDbContextIdentifier> masterSlaveRepository

            , ITangentDbContextOfT<INonDbSetQuery> tangent
            )
        {
            _repository = repository;
            _testRepository = testRepository;
            _vTestRepository = vTestRepository;

            _multipleRepository = multipleRepository;
            _vTestMultipleRepository = vTestMultipleRepository;

            _masterSlaveRepository = masterSlaveRepository;

            _tangent = tangent;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [NonVaildate, NonTransaction, ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<TestOutput>> GetAsync()
        {
            return await _testRepository.Entities
                .ProjectToType<TestOutput>()
                .ToListAsync();
        }

        /// <summary>
        /// 分页查询所有
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<PagedList> GetAsync(int page = 1, int page_size = 20)
        {
            var pageList = await _testRepository.PagedAllAsync(page, page_size);
            return pageList.Adapt<PagedList>();
        }

        /// <summary>
        /// 分页查询所有
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<PagedList> GetAsync(int page = 1, int page_size = 20, string _ = "3.0")
        {
            var pageList = await _testRepository.PagedAllAsync(page, page_size);
            return pageList.Adapt<PagedList>();
        }

        /// <summary>
        /// 搜索数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost, MirrorAction(KeepRouteVerb = true)]
        public async Task<IEnumerable<TestOutput>> SearchAsync([Required] TestSearchInput input)
        {
            input = input ?? throw new InvalidOperationException("非法操作：搜索条件为空。");

            return await _testRepository.All()
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
        [MirrorAction(KeepRouteVerb = true)]
        public async Task<IEnumerable<TestOutput>> SearchAsync(string keyword)
        {
            return await _testRepository.All()
                    .Where(u => u.Name.Contains(keyword))
                    .ProjectToType<TestOutput>()
                    .ToListAsync();
        }

        /// <summary>
        /// 查询一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status204NoContent)]
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
        [Authorize]
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
            var entity = await _testRepository.FindAsync(id) ?? throw Oops.Set(ExceptionCodes.DataNotFound1000);
            input.Adapt(entity);

            await _testRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 更新指定列（只更新Name列）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [MirrorAction(SplitWordToRoutePath = true)]
        public async Task UpdateIncludeProperties(int id, TestInput input)
        {
            var entity = await _testRepository.FindAsync(id)
                ?? throw Oops.Set(ExceptionCodes.DataNotFound1000);

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
        [MirrorAction(SplitWordToRoutePath = true)]
        public async Task UpdateExcludeProperties(int id, TestInput input)
        {
            var entity = await _testRepository.FindAsync(id) ?? throw Oops.Set(ExceptionCodes.DataNotFound1000);
            input.Adapt(entity);

            entity.CreatedTime = DateTime.Now; // 测试代码，不会更新

            await _testRepository.UpdateExcludePropertiesAsync(entity, u => u.Name);
        }

        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [MirrorAction(KeepOriginalName = true)]
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
            await _testRepository.FindToDeleteAsync(id, ExceptionCodes.DataNotFound1000);
        }

        /// <summary>
        /// 软删除数据
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        [MirrorAction(SplitWordToRoutePath = true)]
        public Task FakeDeleteAsync(int id)
        {
            return _testRepository.FindToFakeDeleteAsync(id, u => u.IsDeleted, true);
        }

        /// <summary>
        /// 原始Sql查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [MirrorAction(SplitWordToRoutePath = true)]
        public Task<IEnumerable<TestOutput>> SqlQueryAsync(TestSqlInput input)
        {
            return _testRepository.SqlQueryAsync<TestOutput>(input.Sql);
        }

        /// <summary>
        /// 原始Sql DataSet 查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [MirrorAction(SplitWordToRoutePath = true)]
        public Task<(IEnumerable<TestOutput>, IEnumerable<TestOutput>)> SqlDatasetQueryAsync(TestSqlInput input)
        {
            return _testRepository.SqlDataSetAsync<TestOutput, TestOutput>(input.Sql);
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
        [MirrorAction(SplitWordToRoutePath = true)]
        public Task<IEnumerable<TestOutput>> SqlProcedureAsync(string name)
        {
            return _testRepository.SqlProcedureAsync<TestOutput>(name, new { Name = "string" });
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
        [MirrorAction(SplitWordToRoutePath = true)]
        public Task<int> SqlScalarFunctionAsync()
        {
            return _testRepository.SqlScalarFunctionAsync<int>("dbo.FN_GetId", new { Id = 1 });
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
        [MirrorAction(SplitWordToRoutePath = true)]
        public Task<IEnumerable<TestOutput>> SqlTableFunctionAsync()
        {
            return _testRepository.SqlTableFunctionAsync<TestOutput>("dbo.FN_GetTable", new { Id = 5 });
        }

        /// <summary>
        /// 查询视图
        /// </summary>
        /// <returns></returns>
        // ================================
        // CREATE VIEW V_Test
        // AS
        // SELECT Id,Name,Age,TenantId FROM Tests;
        // ================================
        [MirrorAction(SplitWordToRoutePath = true)]
        public async Task<IEnumerable<TestOutput>> SqlViewQueryAsync()
        {
            return await _vTestRepository.Entities
                .ProjectToType<TestOutput>()
                .ToListAsync();
        }

        /// <summary>
        /// Linq中调用函数
        /// </summary>
        /// <returns></returns>
        [MirrorAction(SplitWordToRoutePath = true)]
        public async Task<IEnumerable<TestOutput>> GetLinqFunctionAsync()
        {
            return await _testRepository.All()
                .Where(u => u.Id >= DbStaticFunctions.GetId(0))
                .ProjectToType<TestOutput>()
                .ToListAsync();
        }

        /// <summary>
        /// 测试仓储实例生命周期
        /// </summary>
        /// <returns></returns>
        [MirrorAction(KeepOriginalName = true)]
        public Task<bool> TestRepositoryScopeLifetime()
        {
            var testRepository = _repository.Set<Test>();

            return Task.FromResult(testRepository == _testRepository);
        }

        /// <summary>
        /// 无参数获取DataTable
        /// </summary>
        /// <returns></returns>
        [MirrorAction(KeepOriginalName = true)]
        public Task GetDataTable()
        {
            return _tangent.Proxy.Execute("小僧");
        }
    }
}