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
    }
}
