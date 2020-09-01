using Fur.Core;
using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
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

        public FurAppService(
            IRepository<Test> testRepository
            , IRepository repository
            , IServiceProvider serviceProvider)
        {
            _testRepository = testRepository;
            _repository = repository;
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<TestDto> Get()
        {
            var a1 = _serviceProvider.GetService<IRepository>();
            var b1 = _serviceProvider.GetService<IRepository>();

            var c1 = a1.Equals(b1); // true

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

        public void Add(TestDto testDto)
        {
            _testRepository.AddSaveChanges(testDto.Adapt<Test>());
        }

        public void Update(TestDto testDto)
        {
            _testRepository.UpdateIncludePropertiesSaveChanges(testDto.Adapt<Test>(), u => u.Name);
        }
    }
}