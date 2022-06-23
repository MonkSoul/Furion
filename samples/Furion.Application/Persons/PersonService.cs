using Furion.Application.Persons;
using Furion.Application.Services;
using Furion.DatabaseAccessor.Extensions;

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

    public void TestDbParameters()
    {
        var data = "select * from person where id=@Id".SqlQuery(new
        {
            Id = 1,
            Name = "Furion"
        });
    }

    /// <summary>
    /// 测试 Markdown
    /// </summary>
    /// <remarks>
    /// # 先知 / Furion ([探索版](https://gitee.com/dotnetchina/Furion/tree/experimental/))
    /// 
    ///     一个应用程序框架，您可以将它集成到任何.NET/C# 应用程序中。
    /// 
    /// An application framework that you can integrate into any.NET/C# application.
    /// 
    /// ## 安装 / Installation
    /// 
    /// - [Package Manager] (https://www.nuget.org/packages/Furion)
    /// 
    /// ```powershell
    /// Install-Package Furion
    /// ```
    /// 
    /// - [.NET CLI] (https://www.nuget.org/packages/Furion)
    /// 
    /// ```powershell
    /// dotnet add package Furion
    /// ```
    /// 
    /// ## 例子 / Examples
    /// 
    /// 我们在[主页](https://dotnetchina.gitee.io/furion)上有不少例子，这是让您入门的第一个：
    /// 
    /// We have several examples [on the website] (https://dotnetchina.gitee.io/furion). Here is the first one to get you started:
    /// 
    /// ```cs
    /// Serve.Run();
    /// 
    ///     [DynamicApiController]
    ///     public class HelloService
    ///     {
    ///         public string Say()
    ///         {
    ///             return "Hello, Furion";
    ///         }
    ///     }
    /// ```
    /// 
    /// 打开浏览器访问 `https://localhost:5001`。
    /// 
    /// Open browser access `https://localhost:5001`.
    /// 
    /// ## 文档 / Documentation
    /// 
    /// 您可以在[主页] (https://dotnetchina.gitee.io/furion)或[备份主页](https://furion.icu)找到 Furion 文档。
    /// 
    /// You can find the Furion documentation[on the website](https://dotnetchina.gitee.io/furion) or [on the backup website](https://furion.icu).
    /// 
    /// ## 贡献 / Contributing
    /// 
    /// 该存储库的主要目的是继续发展 Furion 核心，使其更快、更易于使用。 Furion 的开发在[Gitee](https://gitee.com/dotnetchina/Furion) 上公开进行，我们感谢社区贡献错误修复和改进。
    /// 
    /// 阅读[贡献指南] (https://dotnetchina.gitee.io/furion/docs/contribute)内容，了解如何参与改进 Furion。
    /// 
    /// The main purpose of this repository is to continue evolving Furion core, making it faster and easier to use.Development of Furion happens in the open on[Gitee] (https://gitee.com/dotnetchina/Furion), and we are grateful to the community for contributing bugfixes and improvements.
    /// 
    /// Read[contribution documents] (https://dotnetchina.gitee.io/furion/docs/contribute) to learn how you can take part in improving Furion.
    /// 
    /// ## 许可证 / License
    /// 
    /// Furion 采用[MulanPSL - 2.0](https://gitee.com/dotnetchina/Furion/blob/master/LICENSE) 开源许可证。
    /// 
    /// Furion uses the[MulanPSL - 2.0] (https://gitee.com/dotnetchina/Furion/blob/master/LICENSE) open source license.
    /// 
    /// ```
    /// Copyright(c) 2020-2022 百小僧, Baiqian Co., Ltd.
    /// Furion is licensed under Mulan PSL v2.
    /// You can use this software according to the terms andconditions of the Mulan PSL v2.
    /// You may obtain a copy of Mulan PSL v2 at:
    ///             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
    /// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUTWARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
    /// See the Mulan PSL v2 for more details.
    /// ```
    /// 
    /// </remarks>
    /// <returns></returns>
    public string Hello()
    {
        return "Furion";
    }
}