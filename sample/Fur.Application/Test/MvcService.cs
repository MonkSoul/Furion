using Fur.Application.Persons;
using Fur.Core;
using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Fur.LinqBuilder;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fur.DependencyInjection;

namespace Fur.Application
{

    /// <summary>
    /// MVC
    /// </summary>
    public class MvcService : IMvcService, ITransientDependency
    {
        // 初始化仓储
        private readonly IRepository<Person> _personRepository;

        public MvcService(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
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
    }

    public interface IMvcService
    {
        Task<List<PersonDto>> GetAll();
    }
}