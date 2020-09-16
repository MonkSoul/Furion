using Fur.Core;
using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fur.Application
{
    [DynamicApiController] // 这里使用特性方式
    public class FurService /*: IDynamicApiController*/ // 也可以使用接口方式
    {
        // 初始化仓储
        private readonly IRepository<Person> _personRepository;

        public FurService(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        /// <summary>
        /// 新增一条
        /// </summary>
        public async Task<int> Insert(PersonDto personDto)
        {
            // 如果不需要返回自增Id，使用 InsertAsync即可
            var newEntity = await _personRepository.InsertNowAsync(personDto.Adapt<Person>());
            return newEntity.Entity.Id;

            // 还可以直接操作
            // await personDto.Adapt<Person>().InsertNowAsync();
        }

        /// <summary>
        /// 更新一条
        /// </summary>
        /// <param name="personDto"></param>
        public async Task Update(PersonDto personDto)
        {
            await _personRepository.UpdateAsync(personDto.Adapt<Person>());

            // 还可以直接操作
            // await personDto.Adapt<Person>().UpdateAsync();
        }

        /// <summary>
        /// 删除一条
        /// </summary>
        /// <param name="id"></param>
        public async Task Delete(int id)
        {
            await _personRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 查询一条
        /// </summary>
        /// <param name="id"></param>
        public async Task<PersonDto> Find(int id)
        {
            var person = await _personRepository.FindAsync(id);
            return person.Adapt<PersonDto>();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<List<PersonDto>> GetAll()
        {
            var persons = await _personRepository.AsQueryable().ToListAsync();
            return persons.Adapt<List<PersonDto>>();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<PersonDto>> GetAllByPage(int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = await _personRepository.PagedFilterAsync(pageIndex, pageSize);
            return pageResult.Adapt<PagedList<PersonDto>>();
        }
    }
}