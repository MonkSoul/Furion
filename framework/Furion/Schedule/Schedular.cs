// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Reflection.Metadata;

namespace Furion.Schedule;

/// <summary>
/// 调度作业服务静态类
/// </summary>
[SuppressSniffer]
public static class ScheduleServe
{
    /// <summary>
    /// 初始化 Schedule 服务
    /// </summary>
    /// <remarks>仅限不能依赖注入的服务使用</remarks>
    /// <param name="configureOptionsBuilder">作业调度器配置选项构建器委托</param>
    /// <returns><see cref="IDisposable"/></returns>
    public static IDisposable Run(Action<ScheduleOptionsBuilder> configureOptionsBuilder)
    {
        // 创建初始作业调度器配置选项构建器
        var scheduleOptionsBuilder = new ScheduleOptionsBuilder();
        configureOptionsBuilder.Invoke(scheduleOptionsBuilder);

        return Run(scheduleOptionsBuilder);
    }

    /// <summary>
    /// 初始化 Schedule 服务
    /// </summary>
    /// <remarks>仅限不能依赖注入的服务使用</remarks>
    /// <param name="scheduleOptionsBuilder">作业调度器配置选项构建器</param>
    public static IDisposable Run(ScheduleOptionsBuilder scheduleOptionsBuilder = default)
    {
        // 创建服务集合
        var services = new ServiceCollection();

        // 注册初始服务
        services.AddConsoleFormatter();

        // 注册 Schedule 服务
        services.AddSchedule(scheduleOptionsBuilder);

        // 构建服务并解析 ScheduleHostedService
        var serviceProvider = services.BuildServiceProvider();
        var scheduleHostedService = serviceProvider.GetServices<IHostedService>()
            .Single(s => s.GetType().Name == nameof(ScheduleHostedService));

        // 启动服务
        var cancellationTokenSource = new CancellationTokenSource();
        scheduleHostedService.StartAsync(cancellationTokenSource.Token);

        return serviceProvider;
    }
}

/// <summary>
/// 作业调度器静态类
/// </summary>
[SuppressSniffer]
public static class Schedular
{
    /// <summary>
    /// 获取作业计划工厂
    /// </summary>
    /// <returns><see cref="ISchedulerFactory"/></returns>
    public static ISchedulerFactory GetFactory()
    {
        return App.GetRequiredService<ISchedulerFactory>(App.RootServices);
    }

    /// <summary>
    /// 获取作业
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <returns><see cref="IScheduler"/></returns>
    public static IScheduler GetJob(string jobId)
    {
        return GetFactory().GetJob(jobId);
    }

    /// <summary>
    /// 序列化对象
    /// </summary>
    /// <remarks>主要用于作业触发器参数，作业信息额外数据序列化</remarks>
    /// <param name="obj">对象</param>
    /// <returns><see cref="string"/></returns>
    public static string Serialize(object obj)
    {
        return Penetrates.Serialize(obj);
    }

    /// <summary>
    /// 反序列化对象
    /// </summary>
    /// <remarks>主要用于作业触发器参数，作业信息额外数据序列化</remarks>
    /// <param name="json">JSON 字符串</param>
    /// <returns>T</returns>
    public static T Deserialize<T>(string json)
    {
        return Penetrates.Deserialize<T>(json);
    }

    /// <summary>
    /// 编译 C# 类定义代码
    /// </summary>
    /// <param name="csharpCode">字符串代码</param>
    /// <param name="assemblyName">自定义程序集名称</param>
    /// <param name="additionalAssemblies">附加的程序集</param>
    /// <returns><see cref="Assembly"/></returns>
    public static Assembly CompileCSharpClassCode(string csharpCode, string assemblyName = default, params Assembly[] additionalAssemblies)
    {
        // 空检查
        if (csharpCode == null) throw new ArgumentNullException(nameof(csharpCode));

        // 合并程序集
        var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        var references = assemblyName != null && additionalAssemblies.Length > 0
            ? domainAssemblies.Concat(additionalAssemblies)
            : domainAssemblies;

        // 生成语法树
        var syntaxTree = CSharpSyntaxTree.ParseText(csharpCode);

        // 创建 C# 编译器
        var compilation = CSharpCompilation.Create(
          string.IsNullOrWhiteSpace(assemblyName) ? Path.GetRandomFileName() : assemblyName.Trim(),
          new[]
          {
                    syntaxTree
          },
          references.Select(ass =>
          {
              // MetadataReference.CreateFromFile(ass.Location)
              unsafe
              {
                  ass.TryGetRawMetadata(out var blob, out var length);
                  var moduleMetadata = ModuleMetadata.CreateFromMetadata((IntPtr)blob, length);
                  var assemblyMetadata = AssemblyMetadata.Create(moduleMetadata);
                  var metadataReference = assemblyMetadata.GetReference();
                  return metadataReference;
              }
          }),
          new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        // 编译代码
        using var memoryStream = new MemoryStream();
        var emitResult = compilation.Emit(memoryStream);

        // 编译失败抛出异常
        if (!emitResult.Success)
        {
            throw new InvalidOperationException($"Unable to compile template: {string.Join("\n", emitResult.Diagnostics.ToList().Where(w => w.IsWarningAsError || w.Severity == DiagnosticSeverity.Error))}");
        }

        memoryStream.Position = 0;

        // 返回编译程序集
        return Assembly.Load(memoryStream.ToArray());
    }
}