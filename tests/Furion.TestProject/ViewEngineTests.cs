using Furion.DataEncryption;
using Furion.DynamicApiController;
using Furion.ViewEngine;
using Furion.ViewEngine.Extensions;

namespace Furion.TestProject;

/// <summary>
/// 视图引擎集成测试
/// </summary>
public class ViewEngineTests : IDynamicApiController
{
    private readonly IViewEngine _viewEngine;

    public ViewEngineTests(IViewEngine viewEngine)
    {
        _viewEngine = viewEngine;
    }

    /// <summary>
    /// 集合的泛型类型为匿名类型
    /// </summary>
    /// <returns></returns>
    public async Task<string> TestRunCompile()
    {
        var str1 = await _viewEngine.RunCompileAsync(@"
            Hello @Model.Name
            @foreach(var item in Model.Items)
            {
                <p>@item</p>
            }
            @foreach(var item in Model.list)
            {
                <p>@item.a</p>
            }
        ", new
        {
            Name = "Furion",
            Items = new[] { 3, 1, 2 },
            ////////////////这里这里
            list = Enumerable.Range(0, 10).Select(x => new
            {
                a = x.ToString(),
                b = x.ToString(),
            }).ToList()
        });

        return str1;
    }

    /// <summary>
    /// 测试 RunCompile 方法（弱类型模型）
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<string> TestRunCompile(string name)
    {
        var runCompileTemplate = "Hello @Model.Name";
        var model = new { Name = name };

        var str1 = _viewEngine.RunCompile(runCompileTemplate, model);
        var str2 = runCompileTemplate.RunCompile(model);
        var str3 = await _viewEngine.RunCompileAsync(runCompileTemplate, model);
        var str4 = await runCompileTemplate.RunCompileAsync(model);

        var isEqual = (str1 == str2) && (str3 == str4) && (str1 == str4);
        if (!isEqual) throw new Exception("多次编译之后模板内容相等");

        return str1;
    }

    /// <summary>
    /// 测试 RunCompile 方法（强类型模型）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> TestRunCompileStrongly(TestStronglyModel model)
    {
        var runCompileTemplate = @"Hello @Model.Name
@foreach(var item in Model.Items)
{
    <p>@item</p>
}";

        var str1 = _viewEngine.RunCompile(runCompileTemplate, model);
        var str2 = runCompileTemplate.RunCompile(model);
        var str3 = await _viewEngine.RunCompileAsync(runCompileTemplate, model);
        var str4 = await runCompileTemplate.RunCompileAsync(model);

        var isEqual = (str1 == str2) && (str3 == str4) && (str1 == str4);
        if (!isEqual) throw new Exception("多次编译之后模板内容相等");

        return str1;
    }

    /// <summary>
    /// 测试 RunCompileFromCached 方法（弱类型模型）
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<string> TestRunCompileFromCached(string name)
    {
        var runCompileTemplate = "Hello @Model.Name";
        var model = new { Name = name };

        var str1 = _viewEngine.RunCompileFromCached(runCompileTemplate, model);
        var str2 = runCompileTemplate.RunCompileFromCached(model);
        var str3 = await _viewEngine.RunCompileFromCachedAsync(runCompileTemplate, model);
        var str4 = await runCompileTemplate.RunCompileFromCachedAsync(model);

        var isEqual = (str1 == str2) && (str3 == str4) && (str1 == str4);
        if (!isEqual) throw new Exception("多次编译之后模板内容相等");

        if (!File.Exists(Path.Combine(AppContext.BaseDirectory, "templates", $"~{MD5Encryption.Encrypt(runCompileTemplate)}.dll")))
        {
            throw new Exception("没找到模板缓存 .dll");
        }

        return str1;
    }

    /// <summary>
    /// 测试 RunCompileFromCached 方法（强类型模型）
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> TestRunCompileStronglyFromCached(TestStronglyModel model)
    {
        var runCompileTemplate = @"Hello @Model.Name
@foreach(var item in Model.Items)
{
    <p>@item</p>
}";

        var str1 = _viewEngine.RunCompileFromCached(runCompileTemplate, model);
        var str2 = runCompileTemplate.RunCompileFromCached(model);
        var str3 = await _viewEngine.RunCompileFromCachedAsync(runCompileTemplate, model);
        var str4 = await runCompileTemplate.RunCompileFromCachedAsync(model);

        var isEqual = (str1 == str2) && (str3 == str4) && (str1 == str4);
        if (!isEqual) throw new Exception("多次编译之后模板内容相等");

        if (!File.Exists(Path.Combine(AppContext.BaseDirectory, "templates", $"~{MD5Encryption.Encrypt(runCompileTemplate)}.dll")))
        {
            throw new Exception("没找到模板缓存 .dll");
        }

        return str1;
    }

    /// <summary>
    /// 测试 RunCompile 添加程序集
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<string> TestRunCompileAssembly(string name)
    {
        var runCompileTemplate = @$"<div>@System.IO.Path.Combine(""{name}"", ""ViewEngine"")</div>";

        return await _viewEngine.RunCompileAsync(runCompileTemplate, builderAction: builder =>
        {
            builder.AddAssemblyReferenceByName("System.IO");
        });
    }

    /// <summary>
    /// 测试 RunCompile 添加程序集和命名空间
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<string> TestRunCompileAddAssemblyAndNamespace(string name)
    {
        var runCompileTemplate = @$"<div>@Path.Combine(""{name}"", ""ViewEngine"")</div>";

        return await _viewEngine.RunCompileAsync(runCompileTemplate, builderAction: builder =>
        {
            builder.AddUsing("System.IO");
            builder.AddAssemblyReferenceByName("System.IO");
        });
    }

    /// <summary>
    /// 测试 RunCompile 定义模板方法并调用
    /// </summary>
    /// <returns></returns>
    public async Task<string> TestRunCompileIncludeFunctionWithInvoke()
    {
        var runCompileTemplate = @"<area>
    @{ RecursionTest(3); }
</area>

@{
  void RecursionTest(int level)
  {
    if (level <= 0)
    {
        return;
    }

    <div>LEVEL: @level</div>
    @{ RecursionTest(level - 1); }
  }
}";

        return await _viewEngine.RunCompileAsync(runCompileTemplate);
    }

    /// <summary>
    /// 测试 RunCompile 调用强类型类方法
    /// </summary>
    /// <returns></returns>
    public async Task<string> TestRunCompileStronglyInvokeClassMethod()
    {
        var content = @"Hello @A, @B, @Decorator(123)";

        var template = await _viewEngine.CompileAsync<CustomModel>(content);

        return await template.RunAsync(instance =>
        {
            instance.A = 10;
            instance.B = "Furion";
        });
    }
}

public class TestStronglyModel
{
    public string Name { get; set; }
    public int[] Items { get; set; }
}

public class CustomModel : ViewEngineModel
{
    public int A { get; set; }
    public string B { get; set; }

    public string Decorator(object value)
    {
        return "-=" + value + "=-";
    }
}