using Furion.Application.Persons;
using Furion.Application.Services;

namespace Furion.Application;

/// <summary>
/// 用户管理
/// </summary>
[AllowAnonymous, ApiDescriptionSettings("Default@1")]
public class PersonService : IDynamicApiController
{
    private readonly IRepository<Person> _personRepository;
    private readonly ITestService _testService;
    private readonly ISql _sql;

    public PersonService(IRepository<Person> personRepository
        , ITestService testService
        , ISql sql)
    {
        _personRepository = personRepository;
        _testService = testService;
        _sql = sql;
    }

    /// <summary>
    /// 新增一条
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Description = "我是一段描述，显示更多内容 <button>我是按钮</button>")]
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
    [Obsolete]
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
    [ApiDescriptionSettings(LowercaseRoute = false, KeepName = true)]
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

    public string GetName()
    {
        return _testService.GetName();
    }

    public List<Person> GetProxyPersons()
    {
        return _sql.GetPersons();
    }
}