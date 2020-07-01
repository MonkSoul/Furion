using Fur.AttachController.Attributes;
using Fur.AttachController.Dependencies;
using Fur.DatabaseVisitor.Repositories;
using Fur.Record.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fur.Application
{
    [AttachController("Test")]
    public class TestAppService : ITestAppService, IAttachControllerDependency
    {
        private readonly IRepositoryOfT<Test> _testRepository;
        public TestAppService(IRepositoryOfT<Test> testRepository)
        {
            _testRepository = testRepository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IEnumerable<Test>> GetAsync()
        {
            return await _testRepository.Entity.ToListAsync();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<Test> GetAsync(int id)
        {
            return await _testRepository.Entity.FindAsync(id);
        }

        public async Task<int> InsertAsync(Test test)
        {
            var entity = await _testRepository.InsertSaveChangesAsync(test);
            return entity.Entity.Id;
        }

        public async Task<Test> UpdateAsync(int id, string name, int age)
        {
            var entity = await _testRepository.Entity.FindAsync(id);
            entity.Name = name;
            entity.Age = age;

            var trackEntity = await _testRepository.UpdateSaveChangesAsync(entity);
            return trackEntity.Entity;
        }

        public async Task<Test> DeleteAsync(int id)
        {
            var entity = await _testRepository.Entity.FindAsync(id);
            var trackEntity = await _testRepository.DeleteSaveChangesAsync(entity);
            return trackEntity.Entity;
        }
    }
}
