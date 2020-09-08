using Fur.Core;
using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Fur.FriendlyException;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fur.Application
{
    /// <summary>
    /// 这里是注释
    /// </summary>
    [ApiDescriptionSettings(Tag = "数据库操作测试示例")]
    public class FurAppService : IDynamicApiController
    {
        private readonly IRepository<Test> _testRepository;
        private readonly IRepository _repository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepository<Test, DbContextLocator> _repository1;
        private readonly IRepository<Test, FurDbContextLocator2> _repository2;

        public FurAppService(
            IRepository<Test> testRepository
            , IRepository repository
            , IServiceProvider serviceProvider
            , IRepository<Test, DbContextLocator> repository1
            , IRepository<Test, FurDbContextLocator2> repository2)
        {
            _testRepository = testRepository;
            _repository = repository;
            _serviceProvider = serviceProvider;
            _repository1 = repository1;
            _repository2 = repository2;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [NonTransact]
        public IEnumerable<TestDto> Get()
        {
            return _testRepository.AsQueryable().ProjectToType<TestDto>().ToList();
        }

        /// <summary>
        /// 查询sql
        /// </summary>
        /// <returns></returns>
        [NonTransact]
        public List<Test> ExecuteSqlQuery()
        {
            return _testRepository.SqlQuery<Test>("SELECT * FROM dbo.Test where id > @id", new { id = 10 });
        }

        /// <summary>
        /// 查询多个sql
        /// </summary>
        /// <returns></returns>
        [NonTransact]
        public object ExecuteSqlQuerySet()
        {
            var (list1, list2, list3) = _testRepository.SqlQueryMultiple<Test, Test, TestDto>(@"
select * from dbo.test where id > @id;
select top 10 * from dbo.test;
select Id,Name,Age,Address from dbo.test where id > @id;
", new { id = 10 });

            return new
            {
                list1,
                list2,
                list3
            };
        }

        /// <summary>
        /// 测试单个数据库，多个数据库、读写分离、主从库数据库生命周期
        /// </summary>
        /// <returns></returns>
        [NonTransact]
        public bool CheckScope()
        {
            // 测试只读数据库操作
            var ts = _testRepository.Constraint<IReadableRepository<Test>>();
            //var ad = ts.Find(6);

            // 测试服务提供器获取
            var a1 = _serviceProvider.GetService<IRepository>();
            var b1 = _serviceProvider.GetService<IRepository>();

            var z = a1.Equals(b1); // true

            // 测试非泛型动态切换数据库
            var test1 = _repository.Change<Test>();
            var test2 = _repository.Change<Test>();
            var a = test1.Equals(test2);    // true
            var b = test1.Equals(_testRepository);  // true
            var c = test2.Equals(_testRepository);  // true

            // 测试泛型仓储动态切换数据库
            var test3 = _testRepository.Change<Test>();
            var d = test1.Equals(test3); // true
            var e = test3.Equals(_testRepository); // true

            var test4 = _repository1.Change<Test>();
            var f = test1.Equals(test4); // true

            var test5 = _repository2.Change<Test>();
            var g = test4.Equals(test5); // true

            return a == b == c == d == e == f == g == z;
        }

        /// <summary>
        /// 获取一个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [NonTransact]
        public TestDto Get(int id)
        {
            return _testRepository.Find(id).Adapt<TestDto>();
        }

        /// <summary>
        /// 新增一条
        /// </summary>
        /// <param name="testDto"></param>
        public void Add(TestDto testDto)
        {
            _testRepository.Insert(testDto.Adapt<Test>());
        }

        /// <summary>
        /// 更新部分字段
        /// </summary>
        /// <param name="testDto"></param>
        [IfException(EFCoreErrorCodes.KeyNotSet, ErrorMessage = "没有设置主键")]
        [IfException(EFCoreErrorCodes.DataNotFound, ErrorMessage = "数据没找到")]
        public void UpdateInclude(TestDto testDto)
        {
            _testRepository.UpdateIncludeExists(testDto.Adapt<Test>(), u => u.Name, u => u.Age);
        }

        /// <summary>
        /// 排除部分字段更新
        /// </summary>
        /// <param name="testDto"></param>
        [IfException(EFCoreErrorCodes.KeyNotSet, ErrorMessage = "没有设置主键")]
        [IfException(EFCoreErrorCodes.DataNotFound, ErrorMessage = "数据没找到")]
        public void UpdateExclude(TestDto testDto)
        {
            _testRepository.UpdateExcludeExists(testDto.Adapt<Test>(), u => u.Address);
        }

        /// <summary>
        /// 更新所有
        /// </summary>
        /// <param name="testDto"></param>
        [IfException(EFCoreErrorCodes.KeyNotSet, ErrorMessage = "没有设置主键")]
        [IfException(EFCoreErrorCodes.DataNotFound, ErrorMessage = "数据没找到")]
        public void UpdateAll(TestDto testDto)
        {
            _testRepository.UpdateExistsNow(testDto.Adapt<Test>());
        }

        /// <summary>
        /// 删除一条
        /// </summary>
        /// <param name="id"></param>
        [IfException(EFCoreErrorCodes.DataNotFound, ErrorMessage = "数据没找到")]
        public void Delete(int id)
        {
            _testRepository.DeleteExists(id);
        }

        /// <summary>
        /// 执行存储过程查询
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// <code>
        /// CREATE PROC PROC_GetTable @id INT
        /// AS
        /// BEGIN
        ///     SELECT *
        ///     FROM dbo.Test
        ///     WHERE Id > @id;
        /// END
        /// </code>
        /// </remarks>
        /// <returns></returns>
        public List<TestDto> ExecuteProcedureQuery(int id)
        {
            return _testRepository.SqlProcedureQuery<Test>("PROC_GetTable", new { id = id }).Adapt<List<TestDto>>();
        }

        /// <summary>
        /// 执行存储过程查询返回多个结果
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// <code>
        /// CREATE PROC PROC_GetTableSet @id INT
        /// AS
        /// BEGIN
        ///     SELECT *
        ///     FROM dbo.Test
        ///     WHERE Id > @id;
        ///
        ///     SELECT TOP 20
        ///            *
        ///     FROM dbo.Test
        ///     WHERE Id > @id;
        ///
        ///     SELECT TOP 5
        ///            *
        ///     FROM dbo.Test
        ///     WHERE Id > @id;
        /// END
        /// </code>
        /// </remarks>
        /// <returns></returns>
        public object ExecuteProcedureQuerySet(int id)
        {
            var (list1, list2, list3) = _testRepository.SqlProcedureQueryMultiple<Test, Test, Test>("PROC_GetTableSet", new { id = id });

            return new
            {
                list1,
                list2,
                list3
            };
        }

        /// <summary>
        /// 执行存储过程返回单行单列
        /// </summary>
        /// <remarks>
        /// <code>
        /// CREATE PROC PROC_GetScalarValue
        /// AS
        /// BEGIN
        ///     SELECT 'Fur';
        /// END
        /// </code>
        /// </remarks>
        /// <returns></returns>
        public string ExecuteProcedureScalar()
        {
            return _testRepository.SqlProcedureScalar<string>("PROC_GetScalarValue");
        }

        /// <summary>
        /// 执行存储过程返回受影响行数
        /// </summary>
        /// <remarks>
        /// <code>
        /// CREATE PROC PROC_AddTest
        /// AS
        /// BEGIN
        ///     INSERT INTO dbo.Test
        ///     (
        ///         TenantId,
        ///         Name,
        ///         Address,
        ///         Age
        ///     )
        ///     VALUES
        ///     (   NULL,     -- TenantId - uniqueidentifier
        ///         N'存储过程',  -- Name - nvarchar(max)
        ///         N'微软数据库', -- Address - nvarchar(max)
        ///         5         -- Age - int
        ///         );
        /// END
        /// </code>
        /// </remarks>
        public int ExecuteProcedureNonQuery()
        {
            return _testRepository.SqlProcedureNonQuery("PROC_AddTest");
        }

        /// <summary>
        /// 执行存储过程带OUTPUT，返回值，还带结果集（非常复杂）😡
        /// </summary>
        /// <remarks>
        /// <code>
        /// CREATE PROC PROC_Output
        ///     @Id INT,
        ///     @Name NVARCHAR(32) OUTPUT,
        ///     @Age INT OUTPUT
        /// AS
        /// BEGIN
        ///     SET @Name = 'Fur Output';
        ///
        ///     SELECT *
        ///     FROM dbo.Test
        ///     WHERE Id > @Id;
        ///
        ///     SELECT TOP 10
        ///            *
        ///     FROM dbo.Test;
        ///
        ///     SET @Age = 27;
        ///
        ///     RETURN 10;
        /// END;
        /// </code>
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public object ExecuteProcedureOutput(int id)
        {
            var result = _testRepository.SqlProcedureOutput("PROC_Output", new ProcOutputParameter { Id = id });

            // 将Result转为集合
            var (list1, list2) = result.Result.ToList<Test, Test>();

            return new
            {
                result.OutputValues,
                result.ReturnValue,
                list1,
                list2
            };
        }
    }
}