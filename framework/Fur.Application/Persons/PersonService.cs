using Fur.Application.Persons;
using Fur.Authorization;
using Fur.Core;
using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Fur.LinqBuilder;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Fur.Application
{
    /// <summary>
    /// 用户管理
    /// </summary>
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
            var person = await _personRepository.Entities.Include(u => u.PersonDetail)
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
        [NonTransact]
        public async Task<List<PersonDto>> GetAll()
        {
            var persons = _personRepository.AsQueryable()
                                                                .ProjectToType<PersonDto>();
            return await persons.ToListAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [NonTransact]
        public async Task<PagedList<PersonDto>> GetAllByPage(int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = _personRepository.AsQueryable()
                                                                     .ProjectToType<PersonDto>();

            return await pageResult.ToPagedListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 搜索数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        [NonTransact]
        public async Task<List<PersonDto>> Search([FromQuery] string name, [FromQuery] int age)
        {
            var persons = _personRepository.Where(!string.IsNullOrEmpty(name), u => u.Name.Contains(name))
                                                                .Where(age > 18, u => u.Age > 18)
                                                                .ProjectToType<PersonDto>();

            return await persons.ToListAsync();
        }

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public string GetToken()
        {
            var jwtSettings = App.GetOptions<JWTSettingsOptions>();

            var datetimeOffset = new DateTimeOffset(DateTime.Now);
            var token = JWTEncryption.Encrypt(jwtSettings.IssuerSigningKey, new JObject()
            {
                { JwtRegisteredClaimNames.UniqueName, 1 },
                { JwtRegisteredClaimNames.NameId,"百小僧" },
                { JwtRegisteredClaimNames.Iat, datetimeOffset.ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Nbf, datetimeOffset.ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddSeconds(jwtSettings.ExpiredTime.Value*60)).ToUnixTimeSeconds() },
                { JwtRegisteredClaimNames.Iss, jwtSettings.ValidIssuer},
                { JwtRegisteredClaimNames.Aud, jwtSettings.ValidAudience }
            });

            // 设置 Swagger 刷新自动授权
            _httpContextAccessor.HttpContext.Response.Headers["access-token"] = token;

            return token;
        }

        /// <summary>
        /// 需要授权才能访问
        /// </summary>
        /// <returns></returns>
        [AuthorizePolicy]
        public string GetEmail()
        {
            return "fur@chinadot.net";
        }
    }
}