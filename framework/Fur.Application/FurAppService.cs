using Fur.Core;
using Fur.DatabaseAccessor.Repositories;
using Fur.DynamicApiController;
using Mapster;
using System.Collections.Generic;
using System.Linq;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        private readonly IRepository<Test> _repository;

        public FurAppService(IRepository<Test> repository)
        {
            _repository = repository;
        }

        public IEnumerable<TestDto> Get()
        {
            return _repository.Entities.ProjectToType<TestDto>().ToList();
        }

        public TestDto Get(int id)
        {
            return _repository.Entities.Find(id).Adapt<TestDto>();
        }

        public void Post(TestDto testDto)
        {
            _repository.DbContext.Add(testDto.Adapt<Test>());
            _repository.DbContext.SaveChanges();
        }
    }
}