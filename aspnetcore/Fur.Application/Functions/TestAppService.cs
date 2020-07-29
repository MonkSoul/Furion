using Fur.Application.Functions.Dtos;
using Fur.Core.DbEntities;
using Fur.DatabaseAccessor.Repositories;
using Fur.MirrorController.Attributes;
using Fur.MirrorController.Dependencies;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fur.Application.Functions
{
    /// <summary>
    /// 测试接口
    /// </summary>
    [MirrorController]
    public class TestAppService : ITestAppService, IMirrorControllerDependency
    {
        private readonly IRepository<Test> _repository;
        public TestAppService(IRepository<Test> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TestDto>> Get()
        {
            return await _repository.Entities.ProjectToType<TestDto>().ToListAsync();
        }

        public async Task<TestDto> InsertAsync(TestDto test)
        {
            var entityEntry = await _repository.InsertAsync(test.Adapt<Test>());
            return entityEntry.Entity.Adapt<TestDto>();
        }
    }
}