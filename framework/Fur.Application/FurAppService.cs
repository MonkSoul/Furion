using Fur.Core;
using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        private readonly IRepository<Test> _testRepository;
        private readonly IRepository _repository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepository<Test, DbContextLocator> _repository1;

        public FurAppService(
            IRepository<Test> testRepository
            , IRepository repository
            , IServiceProvider serviceProvider
            , IRepository<Test, DbContextLocator> repository1)
        {
            _testRepository = testRepository;
            _repository = repository;
            _serviceProvider = serviceProvider;
            _repository1 = repository1;
        }

        public IEnumerable<TestDto> Get()
        {
            //var a1 = _serviceProvider.GetService<IRepository>();
            //var b1 = _serviceProvider.GetService<IRepository>();

            //var c1 = a1.Equals(b1); // true

            var test1 = _repository.Use<Test>();
            var test2 = _repository.Use<Test>();
            var a = test1.Equals(test2);    // true
            var b = test1.Equals(_testRepository);  // true
            var c = test2.Equals(_testRepository);  // true

            var test3 = _testRepository.Use<Test>();
            var d = test1.Equals(test3);
            var e = test3.Equals(_testRepository);

            var test4 = _repository1.Use<Test>();
            var f = test1.Equals(test4);

            //return _testRepository.Entities.ProjectToType<TestDto>().ToList();

            return _testRepository.Filter().ProjectToType<TestDto>().ToList();
        }

        public TestDto Get(int id)
        {
            return _testRepository.Find(id).Adapt<TestDto>();
        }

        public void Add(TestDto testDto)
        {
            _testRepository.InsertNow(testDto.Adapt<Test>());
        }

        public void UpdateAll(TestDto testDto)
        {
            _testRepository.UpdateNow(testDto.Adapt<Test>());
        }

        public void UpdateInclude(TestDto testDto)
        {
            _testRepository.UpdateIncludeNow(testDto.Adapt<Test>(), u => u.Name, u => u.Age);
        }

        public void UpdateExclude(TestDto testDto)
        {
            _testRepository.UpdateExcludeNow(testDto.Adapt<Test>(), u => u.Address);
        }

        public void Delete(TestDto testDto)
        {
            _testRepository.DeleteNow(testDto.Adapt<Test>());
        }
    }
}