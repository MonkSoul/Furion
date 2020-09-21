using Fur.Core;
using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fur.Application.Persons
{
    /// <summary>
    /// 关联数据演示
    /// </summary>
    [ApiDescriptionSettings("Demo")]
    public class PersonNavigationService : IDynamicApiController
    {
        private readonly IRepository<Person> _personRepository;

        public PersonNavigationService(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        /// <summary>
        /// 一对一
        /// </summary>
        /// <returns></returns>
        public async Task<List<PersonDto>> OneToOne()
        {
            var persons = await _personRepository.Include(u => u.PersonDetail)
                                                                          .ToListAsync();

            return persons.Adapt<List<PersonDto>>();
        }

        /// <summary>
        /// 一对多
        /// </summary>
        /// <returns></returns>
        public async Task<List<PersonDto>> OneToMany()
        {
            var persons = await _personRepository.Include(u => u.PersonDetail)
                                                                          .Include(u => u.Childrens)
                                                                          .ToListAsync();

            return persons.Adapt<List<PersonDto>>();
        }

        /// <summary>
        /// 多对多
        /// </summary>
        /// <returns></returns>
        public async Task<List<PersonDto>> ManyToMany()
        {
            var persons = await _personRepository.Include(u => u.PersonDetail)
                                                                          .Include(u => u.Childrens)
                                                                          .Include(u => u.Posts)
                                                                          .ToListAsync();

            return persons.Adapt<List<PersonDto>>();
        }

        /// <summary>
        /// 内连接
        /// </summary>
        /// <returns></returns>
        public async Task<List<PersonDto>> InnerJoin()
        {
            var query = from p in _personRepository.AsQueryable()
                        join d in _personRepository.Change<PersonDetail>().AsQueryable() on p.Id equals d.PersonId
                        select new PersonDto
                        {
                            PhoneNumber = p.PersonDetail.PhoneNumber,
                            Address = p.Address,
                            Age = p.Age,
                            Name = p.Name,
                            Id = p.Id,
                            QQ = p.PersonDetail.QQ
                        };

            return await query.ToListAsync();
        }

        /// <summary>
        /// 左连接
        /// </summary>
        /// <returns></returns>
        public async Task<List<PersonDto>> LeftJoin()
        {
            var query = from p in _personRepository.AsQueryable()
                        join d in _personRepository.Change<PersonDetail>().AsQueryable() on p.Id equals d.PersonId into results
                        from d in results.DefaultIfEmpty()
                        select new PersonDto
                        {
                            PhoneNumber = p.PersonDetail.PhoneNumber,
                            Address = p.Address,
                            Age = p.Age,
                            Name = p.Name,
                            Id = p.Id,
                            QQ = p.PersonDetail.QQ
                        };

            return await query.ToListAsync();
        }

        /// <summary>
        /// 右连接
        /// </summary>
        /// <returns></returns>
        public async Task<List<PersonDto>> RightJoin()
        {
            var query = from d in _personRepository.Change<PersonDetail>().AsQueryable()
                        join p in _personRepository.AsQueryable() on d.PersonId equals p.Id into results
                        from p in results.DefaultIfEmpty()
                        select new PersonDto
                        {
                            PhoneNumber = p.PersonDetail.PhoneNumber,
                            Address = p.Address,
                            Age = p.Age,
                            Name = p.Name,
                            Id = p.Id,
                            QQ = p.PersonDetail.QQ
                        };

            return await query.ToListAsync();
        }

        /// <summary>
        /// 树形/递归查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<CityDto>> TreeSelect()
        {
            var cities = await _personRepository.Change<City>().AsQueryable()
                                                             .Include(u => u.Childrens)
                                                             .Where(u => u.Id == 1)
                                                             .ToListAsync();

            return cities.Adapt<List<CityDto>>();
        }
    }
}