using Fur.Application.Functions.Dtos;
using Fur.AttachController.Attributes;
using Fur.AttachController.Dependencies;
using Fur.DatabaseVisitor.Repositories;
using Fur.Extensions;
using Fur.Linq.Extensions;
using Fur.Record.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Fur.Application.Functions
{
    [AttachController]
    public class TestAppService : ITestAppService, IAttachControllerDependency
    {
        private readonly IRepositoryOfT<Test> _testRepository;
        public TestAppService(IRepositoryOfT<Test> testRepository)
        {
            _testRepository = testRepository;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<TestOutput>> GetAsync()
        {
            return await _testRepository.Entity
                .ProjectToType<TestOutput>()
                .ToListAsync();
        }

        /// <summary>
        /// 搜索数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TestOutput>> SubmitSearchAsync([Required] TestSearchInput input)
        {
            input = input ?? throw new InvalidOperationException("非法操作：搜索条件为空。");

            return await _testRepository.Get(true)
                    .WhereIf(input.Id.HasValue, u => u.Id == input.Id.Value)
                    .WhereIf(input.Name.HasValue(), u => u.Name.Contains(input.Name))
                    .ProjectToType<TestOutput>()
                    .ToListAsync();
        }

        /// <summary>
        /// 查询一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<TestOutput> GetAsync(int id)
        {
            var entity = await _testRepository.FindAsync(id);
            return entity.Adapt<TestOutput>();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(TestInput input)
        {
            var entity = input.Adapt<Test>();
            var newEntity = await _testRepository.InsertSaveChangesAsync(entity);
            return newEntity.Entity.Id;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateAsync(int id, TestInput input)
        {
            var entity = await _testRepository.FindAsync(id) ?? throw new InvalidOperationException("非法操作：没找到数据。");

            input.Adapt(entity);
            await _testRepository.SaveChangesAsync();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var entity = await _testRepository.FindAsync(id) ?? throw new InvalidOperationException("非法操作：没找到数据。");

            await _testRepository.DeleteSaveChangesAsync(entity);
        }
    }
}
