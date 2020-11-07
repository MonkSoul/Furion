using Fur.Application.Persons;
using Fur.Core;
using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.Application
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [AllowAnonymous, ApiDescriptionSettings("Default@1")]
    public class PersonService : IDynamicApiController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Person> _personRepository;

        public PersonService(IRepository<Person> personRepository
            , IHttpContextAccessor httpContextAccessor)
        {
            _personRepository = personRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 新增一条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<int> Insert(PersonInputDto input)
        {
            // 如果不需要返回自增Id，使用 InsertAsync即可
            var newEntity = await _personRepository.InsertNowAsync(input.Adapt<Person>());
            return newEntity.Entity.Id;

            // 还可以直接操作
            // await personDto.Adapt<Person>().InsertNowAsync();
        }

        /// <summary>
        /// 更新一条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Update(PersonInputDto input)
        {
            var person = await _personRepository.Include(u => u.PersonDetail, false)
                                                                                     .Include(u => u.Childrens)
                                                                                     .Include(u => u.Posts)
                                                                                     .SingleAsync(u => u.Id == input.Id.Value);

            input.Adapt(person);

            await _personRepository.UpdateAsync(person);

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
            var persons = _personRepository.AsQueryable(false)
                                                                .ProjectToType<PersonDto>();
            return await persons.ToListAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedList<PersonDto>> GetAllByPage(int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = _personRepository.AsQueryable(false)
                                                                     .ProjectToType<PersonDto>();

            return await pageResult.ToPagedListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 搜索数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        public async Task<List<PersonDto>> Search([FromQuery] string name, [FromQuery] int age)
        {
            var persons = _personRepository.Where(!string.IsNullOrEmpty(name), u => u.Name.Contains(name), false)
                                                                .Where(age > 18, u => u.Age > 18)
                                                                .ProjectToType<PersonDto>();

            return await persons.ToListAsync();
        }
    }
}