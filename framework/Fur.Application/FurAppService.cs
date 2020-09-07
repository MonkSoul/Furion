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

        public IEnumerable<TestDto> Get()
        {
            return _testRepository.AsQueryable().ProjectToType<TestDto>().ToList();
        }

        public bool CheckScope()
        {
            var ts2 = _testRepository as IReadableRepository<Test>;
            var ts = _testRepository.Restrain<IReadableRepository<Test>>();
            //var ad = ts.Find(6);

            var a1 = _serviceProvider.GetService<IRepository>();
            var b1 = _serviceProvider.GetService<IRepository>();

            var z = a1.Equals(b1); // true

            var test1 = _repository.Change<Test>();
            var test2 = _repository.Change<Test>();
            var a = test1.Equals(test2);    // true
            var b = test1.Equals(_testRepository);  // true
            var c = test2.Equals(_testRepository);  // true

            var test3 = _testRepository.Change<Test>();
            var d = test1.Equals(test3); // true
            var e = test3.Equals(_testRepository); // true

            var test4 = _repository1.Change<Test>();
            var f = test1.Equals(test4); // true

            var test5 = _repository2.Change<Test>();
            var g = test4.Equals(test5); // true

            return a == b == c == d == e == f == g == z;
        }

        public TestDto Get(int id)
        {
            return _testRepository.Find(id).Adapt<TestDto>();
        }

        public void Add(TestDto testDto)
        {
            _testRepository.InsertNow(testDto.Adapt<Test>());
        }

        [IfException(EFCoreErrorCodes.KeyNotSet, ErrorMessage = "没有设置主键")]
        [IfException(EFCoreErrorCodes.DataNotFound, ErrorMessage = "数据没找到")]
        public void UpdateInclude(TestDto testDto)
        {
            _testRepository.UpdateIncludeExistsNow(testDto.Adapt<Test>(), u => u.Name, u => u.Age);
        }

        [IfException(EFCoreErrorCodes.KeyNotSet, ErrorMessage = "没有设置主键")]
        [IfException(EFCoreErrorCodes.DataNotFound, ErrorMessage = "数据没找到")]
        public void UpdateExclude(TestDto testDto)
        {
            _testRepository.UpdateExcludeExistsNow(testDto.Adapt<Test>(), u => u.Address);
        }

        [IfException(EFCoreErrorCodes.KeyNotSet, ErrorMessage = "没有设置主键")]
        [IfException(EFCoreErrorCodes.DataNotFound, ErrorMessage = "数据没找到")]
        public void UpdateAll(TestDto testDto)
        {
            _testRepository.UpdateExistsNow(testDto.Adapt<Test>());
        }

        [IfException(EFCoreErrorCodes.DataNotFound, ErrorMessage = "数据没找到")]
        public void Delete(int id)
        {
            _testRepository.DeleteExistsNow(id);
        }
    }
}