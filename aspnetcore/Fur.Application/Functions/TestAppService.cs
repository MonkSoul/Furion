using Fur.Application.Functions.Dtos;
using Fur.Core.DbEntities;
using Fur.DatabaseAccessor.Repositories;
using Fur.FriendlyException.Attributes;
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
    public class TestAppService : IMirrorControllerModel
    {
        private readonly IRepository<Test> _repository;

        public TestAppService(IRepository<Test> repository)
        {
            _repository = repository;
        }

        [IfException(1000, "The {0} Data is {1} Not Found")]
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