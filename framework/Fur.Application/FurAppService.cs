using Fur.Core;
using Fur.DatabaseAccessor.Repositories;
using Fur.DynamicApiController;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        private readonly IRepository<Test> _testRepository;
        private readonly IRepository _repository;

        public FurAppService(
            IRepository<Test> testRepository
            , IRepository repository)
        {
            _testRepository = testRepository;
            _repository = repository;
        }

        public IEnumerable<TestDto> Get()
        {
            var a1 = App.NewServiceProvider.GetRequiredService<IRepository>();
            var b1 = App.NewServiceProvider.GetRequiredService<IRepository>();

            var c1 = a1.Equals(b1); // false

            var test1 = _repository.Get<Test>();
            var test2 = _repository.Get<Test>();
            var a = test1.Equals(test2);    // true
            var b = test1.Equals(_testRepository);  // true
            var c = test2.Equals(_testRepository);  // true

            return _testRepository.Entities.ProjectToType<TestDto>().ToList();
        }

        public TestDto Get(int id)
        {
            return _testRepository.Entities.Find(id).Adapt<TestDto>();
        }

        public void Post(TestDto testDto)
        {
            _testRepository.AddSaveChanges(testDto.Adapt<Test>());
        }
    }
}