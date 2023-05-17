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

using Furion.ConfigurableOptions;
using Furion.Reflection;
using Furion.Templates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using StackExchange.Profiling;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace Furion;

/// <summary>
/// 全局应用类
/// </summary>
[SuppressSniffer]
public static class App
{
    /// <summary>
    /// 私有设置，避免重复解析
    /// </summary>
    internal static AppSettingsOptions _settings;

    /// <summary>
    /// 应用全局配置
    /// </summary>
    public static AppSettingsOptions Settings => _settings ??= GetConfig<AppSettingsOptions>("AppSettings", true);

    /// <summary>
    /// 全局配置选项
    /// </summary>
    public static IConfiguration Configuration => CatchOrDefault(() => InternalApp.Configuration.Reload(), new ConfigurationBuilder().Build());

    /// <summary>
    /// 获取Web主机环境，如，是否是开发环境，生产环境等
    /// </summary>
    public static IWebHostEnvironment WebHostEnvironment => InternalApp.WebHostEnvironment;

    /// <summary>
    /// 获取泛型主机环境，如，是否是开发环境，生产环境等
    /// </summary>
    public static IHostEnvironment HostEnvironment => InternalApp.HostEnvironment;

    /// <summary>
    /// 存储根服务，可能为空
    /// </summary>
    public static IServiceProvider RootServices => InternalApp.RootServices;

    /// <summary>
    /// 判断是否是单文件环境
    /// </summary>
    public static bool SingleFileEnvironment => string.IsNullOrWhiteSpace(Assembly.GetEntryAssembly().Location);

    /// <summary>
    /// 应用有效程序集
    /// </summary>
    public static readonly IEnumerable<Assembly> Assemblies;

    /// <summary>
    /// 有效程序集类型
    /// </summary>
    public static readonly IEnumerable<Type> EffectiveTypes;

    /// <summary>
    /// 获取请求上下文
    /// </summary>
    public static HttpContext HttpContext => CatchOrDefault(() => RootServices?.GetService<IHttpContextAccessor>()?.HttpContext);

    /// <summary>
    /// 获取请求上下文用户
    /// </summary>
    /// <remarks>只有授权访问的页面或接口才存在值，否则为 null</remarks>
    public static ClaimsPrincipal User => HttpContext?.User;

    /// <summary>
    /// 未托管的对象集合
    /// </summary>
    public static readonly ConcurrentBag<IDisposable> UnmanagedObjects;

    /// <summary>
    /// 解析服务提供器
    /// </summary>
    /// <param name="serviceType"></param>
    /// <returns></returns>
    public static IServiceProvider GetServiceProvider(Type serviceType)
    {
        // 处理控制台应用程序
        if (HostEnvironment == default) return RootServices;

        // 第一选择，判断是否是单例注册且单例服务不为空，如果是直接返回根服务提供器
        if (RootServices != null && InternalApp.InternalServices.Where(u => u.ServiceType == (serviceType.IsGenericType ? serviceType.GetGenericTypeDefinition() : serviceType))
                                                                .Any(u => u.Lifetime == ServiceLifetime.Singleton)) return RootServices;

        // 第二选择是获取 HttpContext 对象的 RequestServices
        var httpContext = HttpContext;
        if (httpContext?.RequestServices != null) return httpContext.RequestServices;
        // 第三选择，创建新的作用域并返回服务提供器
        else if (RootServices != null)
        {
            var scoped = RootServices.CreateScope();
            UnmanagedObjects.Add(scoped);
            return scoped.ServiceProvider;
        }
        // 第四选择，构建新的服务对象（性能最差）
        else
        {
            var serviceProvider = InternalApp.InternalServices.BuildServiceProvider();
            UnmanagedObjects.Add(serviceProvider);
            return serviceProvider;
        }
    }

    /// <summary>
    /// 获取请求生存周期的服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static TService GetService<TService>(IServiceProvider serviceProvider = default)
        where TService : class
    {
        return GetService(typeof(TService), serviceProvider) as TService;
    }

    /// <summary>
    /// 获取请求生存周期的服务
    /// </summary>
    /// <param name="type"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static object GetService(Type type, IServiceProvider serviceProvider = default)
    {
        return (serviceProvider ?? GetServiceProvider(type)).GetService(type);
    }

    /// <summary>
    /// 获取请求生存周期的服务集合
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IEnumerable<TService> GetServices<TService>(IServiceProvider serviceProvider = default)
        where TService : class
    {
        return (serviceProvider ?? GetServiceProvider(typeof(TService))).GetServices<TService>();
    }

    /// <summary>
    /// 获取请求生存周期的服务集合
    /// </summary>
    /// <param name="type"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IEnumerable<object> GetServices(Type type, IServiceProvider serviceProvider = default)
    {
        return (serviceProvider ?? GetServiceProvider(type)).GetServices(type);
    }

    /// <summary>
    /// 获取请求生存周期的服务
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static TService GetRequiredService<TService>(IServiceProvider serviceProvider = default)
        where TService : class
    {
        return GetRequiredService(typeof(TService), serviceProvider) as TService;
    }

    /// <summary>
    /// 获取请求生存周期的服务
    /// </summary>
    /// <param name="type"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static object GetRequiredService(Type type, IServiceProvider serviceProvider = default)
    {
        return (serviceProvider ?? GetServiceProvider(type)).GetRequiredService(type);
    }

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <typeparam name="TOptions">强类型选项类</typeparam>
    /// <param name="path">配置中对应的Key</param>
    /// <param name="loadPostConfigure"></param>
    /// <returns>TOptions</returns>
    public static TOptions GetConfig<TOptions>(string path, bool loadPostConfigure = false)
    {
        var options = Configuration.GetSection(path).Get<TOptions>();

        // 加载默认选项配置
        if (loadPostConfigure && typeof(IConfigurableOptions).IsAssignableFrom(typeof(TOptions)))
        {
            var postConfigure = typeof(TOptions).GetMethod("PostConfigure");
            if (postConfigure != null)
            {
                options ??= Activator.CreateInstance<TOptions>();
                postConfigure.Invoke(options, new object[] { options, Configuration });
            }
        }

        return options;
    }

    /// <summary>
    /// 获取选项
    /// </summary>
    /// <typeparam name="TOptions">强类型选项类</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns>TOptions</returns>
    public static TOptions GetOptions<TOptions>(IServiceProvider serviceProvider = default)
        where TOptions : class, new()
    {
        return Penetrates.GetOptionsOnStarting<TOptions>()
            ?? GetService<IOptions<TOptions>>(serviceProvider ?? RootServices)?.Value;
    }

    /// <summary>
    /// 获取选项
    /// </summary>
    /// <typeparam name="TOptions">强类型选项类</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns>TOptions</returns>
    public static TOptions GetOptionsMonitor<TOptions>(IServiceProvider serviceProvider = default)
        where TOptions : class, new()
    {
        return Penetrates.GetOptionsOnStarting<TOptions>()
            ?? GetService<IOptionsMonitor<TOptions>>(serviceProvider ?? RootServices)?.CurrentValue;
    }

    /// <summary>
    /// 获取选项
    /// </summary>
    /// <typeparam name="TOptions">强类型选项类</typeparam>
    /// <param name="serviceProvider"></param>
    /// <returns>TOptions</returns>
    public static TOptions GetOptionsSnapshot<TOptions>(IServiceProvider serviceProvider = default)
        where TOptions : class, new()
    {
        // 这里不能从根服务解析，因为是 Scoped 作用域
        return Penetrates.GetOptionsOnStarting<TOptions>()
            ?? GetService<IOptionsSnapshot<TOptions>>(serviceProvider)?.Value;
    }

    /// <summary>
    /// 获取命令行配置
    /// </summary>
    /// <param name="args"></param>
    /// <param name="switchMappings"></param>
    /// <returns></returns>
    public static CommandLineConfigurationProvider GetCommandLineConfiguration(string[] args, IDictionary<string, string> switchMappings = null)
    {
        var commandLineConfiguration = new CommandLineConfigurationProvider(args, switchMappings);
        commandLineConfiguration.Load();

        return commandLineConfiguration;
    }

    /// <summary>
    /// 获取当前线程 Id
    /// </summary>
    /// <returns></returns>
    public static int GetThreadId()
    {
        return Environment.CurrentManagedThreadId;
    }

    /// <summary>
    /// 获取当前请求 TraceId
    /// </summary>
    /// <returns></returns>
    public static string GetTraceId()
    {
        return Activity.Current?.Id ?? (InternalApp.RootServices == null ? default : HttpContext?.TraceIdentifier);
    }

    /// <summary>
    /// 获取一段代码执行耗时
    /// </summary>
    /// <param name="action">委托</param>
    /// <returns><see cref="long"/></returns>
    public static long GetExecutionTime(Action action)
    {
        // 空检查
        if (action == null) throw new ArgumentNullException(nameof(action));

        // 计算接口执行时间
        var timeOperation = Stopwatch.StartNew();
        action();
        timeOperation.Stop();
        return timeOperation.ElapsedMilliseconds;
    }

    /// <summary>
    /// 获取服务注册的生命周期类型
    /// </summary>
    /// <param name="serviceType"></param>
    /// <returns></returns>
    public static ServiceLifetime? GetServiceLifetime(Type serviceType)
    {
        var serviceDescriptor = InternalApp.InternalServices
            .FirstOrDefault(u => u.ServiceType == (serviceType.IsGenericType ? serviceType.GetGenericTypeDefinition() : serviceType));

        return serviceDescriptor?.Lifetime;
    }

    /// <summary>
    /// 编译 C# 类定义代码返回程序集
    /// </summary>
    /// <param name="csharpCode">字符串代码</param>
    /// <param name="assemblyName">自定义程序集名称</param>
    /// <param name="additionalAssemblies">附加的程序集</param>
    /// <returns><see cref="Assembly"/></returns>
    public static Assembly CompileCSharpClassCode(string csharpCode, string assemblyName = default, params Assembly[] additionalAssemblies)
    {
        // 编译代码
        using var memoryStream = CompileCSharpClassCodeToStream(csharpCode, assemblyName, additionalAssemblies);

        // 返回编译程序集
        return Assembly.Load(memoryStream.ToArray());
    }

    /// <summary>
    /// 编译 C# 类定义代码保存为 dll 文件
    /// </summary>
    /// <param name="csharpCode">字符串代码</param>
    /// <param name="assemblyName">自定义程序集名称</param>
    /// <param name="additionalAssemblies">附加的程序集</param>
    /// <returns><see cref="Assembly"/></returns>
    public static Assembly CompileCSharpClassCodeToDllFile(string csharpCode, string assemblyName = default, params Assembly[] additionalAssemblies)
    {
        var assName = string.IsNullOrWhiteSpace(assemblyName) ? Path.GetRandomFileName() : assemblyName.Trim();

        // 编译代码
        using var memoryStream = CompileCSharpClassCodeToStream(csharpCode, assName, additionalAssemblies);

        // 保存到 dll 文件
        using var fileStream = new FileStream(
            path: Path.Combine(AppContext.BaseDirectory, $"{assName}.dll"),
            mode: FileMode.OpenOrCreate,
            access: FileAccess.Write,
            share: FileShare.None,
            bufferSize: 8192,
            useAsync: true);

        memoryStream.CopyTo(fileStream);

        // 返回编译程序集
        return Assembly.Load(memoryStream.ToArray());
    }

    /// <summary>
    /// 编译 C# 类定义代码返回内存流
    /// </summary>
    /// <param name="csharpCode">字符串代码</param>
    /// <param name="assemblyName">自定义程序集名称</param>
    /// <param name="additionalAssemblies">附加的程序集</param>
    /// <returns><see cref="Assembly"/></returns>
    public static MemoryStream CompileCSharpClassCodeToStream(string csharpCode, string assemblyName = default, params Assembly[] additionalAssemblies)
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
          references.Where(ass =>
          {
              unsafe
              {
                  return ass.TryGetRawMetadata(out var blob, out var length);
              }
          }).Select(ass =>
          {
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
        var memoryStream = new MemoryStream();
        var emitResult = compilation.Emit(memoryStream);

        // 编译失败抛出异常
        if (!emitResult.Success)
        {
            throw new InvalidOperationException($"Unable to compile class code: {string.Join("\n", emitResult.Diagnostics.ToList().Where(w => w.IsWarningAsError || w.Severity == DiagnosticSeverity.Error))}");
        }

        memoryStream.Position = 0;

        return memoryStream;
    }

    /// <summary>
    /// 打印验证信息到 MiniProfiler
    /// </summary>
    /// <param name="category">分类</param>
    /// <param name="state">状态</param>
    /// <param name="message">消息</param>
    /// <param name="isError">是否为警告消息</param>
    public static void PrintToMiniProfiler(string category, string state, string message = null, bool isError = false)
    {
        if (!CanBeMiniProfiler()) return;

        // 打印消息
        var titleCaseCategory = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(category);
        var customTiming = MiniProfiler.Current?.CustomTiming(category, string.IsNullOrWhiteSpace(message) ? $"{titleCaseCategory} {state}" : message, state);
        if (customTiming == null) return;

        // 判断是否是警告消息
        if (isError) customTiming.Errored = true;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    static App()
    {
        // 未托管的对象
        UnmanagedObjects = new ConcurrentBag<IDisposable>();

        // 加载程序集
        var assObject = GetAssemblies();
        Assemblies = assObject.Assemblies;
        ExternalAssemblies = assObject.ExternalAssemblies;

        // 获取有效的类型集合
        EffectiveTypes = Assemblies.SelectMany(GetTypes);

        AppStartups = new ConcurrentBag<AppStartup>();
    }

    /// <summary>
    /// 应用所有启动配置对象
    /// </summary>
    internal static ConcurrentBag<AppStartup> AppStartups;

    /// <summary>
    /// 外部程序集
    /// </summary>
    internal static IEnumerable<Assembly> ExternalAssemblies;

    /// <summary>
    /// 获取应用有效程序集
    /// </summary>
    /// <returns>IEnumerable</returns>
    private static (IEnumerable<Assembly> Assemblies, IEnumerable<Assembly> ExternalAssemblies) GetAssemblies()
    {
        // 需排除的程序集后缀
        var excludeAssemblyNames = new string[] {
                "Database.Migrations"
            };

        // 读取应用配置
        var supportPackageNamePrefixs = Settings.SupportPackageNamePrefixs ?? Array.Empty<string>();

        IEnumerable<Assembly> scanAssemblies;

        // 获取入口程序集
        var entryAssembly = Assembly.GetEntryAssembly();

        // 非独立发布/非单文件发布
        if (!string.IsNullOrWhiteSpace(entryAssembly.Location))
        {
            var dependencyContext = DependencyContext.Default;

            // 读取项目程序集或 Furion 官方发布的包，或手动添加引用的dll，或配置特定的包前缀
            scanAssemblies = dependencyContext.RuntimeLibraries
               .Where(u =>
                      (u.Type == "project" && !excludeAssemblyNames.Any(j => u.Name.EndsWith(j))) ||
                      (u.Type == "package" && (u.Name.StartsWith(nameof(Furion)) || supportPackageNamePrefixs.Any(p => u.Name.StartsWith(p)))) ||
                      (Settings.EnabledReferenceAssemblyScan == true && u.Type == "reference"))    // 判断是否启用引用程序集扫描
               .Select(u => Reflect.GetAssembly(u.Name));
        }
        // 独立发布/单文件发布
        else
        {
            IEnumerable<Assembly> fixedSingleFileAssemblies = new[] { entryAssembly };

            // 扫描实现 ISingleFilePublish 接口的类型
            var singleFilePublishType = entryAssembly.GetTypes()
                                                .FirstOrDefault(u => u.IsClass && !u.IsInterface && !u.IsAbstract && typeof(ISingleFilePublish).IsAssignableFrom(u));
            if (singleFilePublishType != null)
            {
                var singleFilePublish = Activator.CreateInstance(singleFilePublishType) as ISingleFilePublish;

                // 加载用户自定义配置单文件所需程序集
                var nativeAssemblies = singleFilePublish.IncludeAssemblies();
                var loadAssemblies = singleFilePublish.IncludeAssemblyNames()
                                                .Select(u => Reflect.GetAssembly(u));

                fixedSingleFileAssemblies = fixedSingleFileAssemblies.Concat(nativeAssemblies)
                                                            .Concat(loadAssemblies);

                // 解决 Furion.Extras.ObjectMapper.Mapster 程序集不能加载问题
                try
                {
                    if (!fixedSingleFileAssemblies.Any(u => u.GetName().Name.Equals(ObjectMapperServiceCollectionExtensions.ASSEMBLY_NAME)))
                    {
                        fixedSingleFileAssemblies = fixedSingleFileAssemblies.Concat(new[] {
                            Reflect.GetAssembly(ObjectMapperServiceCollectionExtensions.ASSEMBLY_NAME) });
                    }
                }
                catch { }
            }
            else
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                // 提示没有正确配置单文件配置
                Console.WriteLine(TP.Wrapper("Deploy Console"
                    , "Single file deploy error."
                    , "##Exception## Single file deployment configuration error."
                    , "##Documentation## https://furion.baiqian.ltd/docs/singlefile"));
                Console.ResetColor();
            }

            // 通过 AppDomain.CurrentDomain 扫描，默认为延迟加载，正常只能扫描到 Furion 和 入口程序集（启动层）
            scanAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                                    .Where(ass =>
                                            // 排除 System，Microsoft，netstandard 开头的程序集
                                            !ass.FullName.StartsWith(nameof(System))
                                            && !ass.FullName.StartsWith(nameof(Microsoft))
                                            && !ass.FullName.StartsWith("netstandard"))
                                    .Concat(fixedSingleFileAssemblies)
                                    .Distinct();
        }

        IEnumerable<Assembly> externalAssemblies = Array.Empty<Assembly>();

        // 加载 `appsetting.json` 配置的外部程序集
        if (Settings.ExternalAssemblies != null && Settings.ExternalAssemblies.Any())
        {
            foreach (var externalAssembly in Settings.ExternalAssemblies)
            {
                // 加载外部程序集
                var assemblyFileFullPath = Path.Combine(AppContext.BaseDirectory
                    , externalAssembly.EndsWith(".dll") ? externalAssembly : $"{externalAssembly}.dll");

                // 根据路径加载程序集
                var loadedAssembly = Reflect.LoadAssembly(assemblyFileFullPath);
                if (loadedAssembly == default) continue;
                var assembly = new[] { loadedAssembly };

                // 合并程序集
                scanAssemblies = scanAssemblies.Concat(assembly);
                externalAssemblies = externalAssemblies.Concat(assembly);
            }
        }

        // 处理排除的程序集
        if (Settings.ExcludeAssemblies != null && Settings.ExcludeAssemblies.Any())
        {
            scanAssemblies = scanAssemblies.Where(ass => !Settings.ExcludeAssemblies.Contains(ass.GetName().Name, StringComparer.OrdinalIgnoreCase));
        }

        return (scanAssemblies, externalAssemblies);
    }

    /// <summary>
    /// 加载程序集中的所有类型
    /// </summary>
    /// <param name="ass"></param>
    /// <returns></returns>
    private static IEnumerable<Type> GetTypes(Assembly ass)
    {
        var types = Array.Empty<Type>();

        try
        {
            types = ass.GetTypes();
        }
        catch
        {
            Console.WriteLine($"Error load `{ass.FullName}` assembly.");
        }

        return types.Where(u => u.IsPublic && !u.IsDefined(typeof(SuppressSnifferAttribute), false));
    }

    /// <summary>
    /// 判断是否启用 MiniProfiler
    /// </summary>
    /// <returns></returns>
    internal static bool CanBeMiniProfiler()
    {
        // 减少不必要的监听
        if (Settings.InjectMiniProfiler != true || HttpContext == null
            || !(HttpContext.Request.Headers.TryGetValue("request-from", out var value) && value == "swagger")) return false;

        return true;
    }

    /// <summary>
    /// GC 回收默认间隔
    /// </summary>
    private const int GC_COLLECT_INTERVAL_SECONDS = 5;

    /// <summary>
    /// 记录最近 GC 回收时间
    /// </summary>
    private static DateTime? LastGCCollectTime { get; set; }

    /// <summary>
    /// 释放所有未托管的对象
    /// </summary>
    public static void DisposeUnmanagedObjects()
    {
        foreach (var dsp in UnmanagedObjects)
        {
            try
            {
                dsp?.Dispose();
            }
            finally { }
        }

        // 强制手动回收 GC 内存
        if (UnmanagedObjects.Any())
        {
            var nowTime = DateTime.UtcNow;
            if ((LastGCCollectTime == null || (nowTime - LastGCCollectTime.Value).TotalSeconds > GC_COLLECT_INTERVAL_SECONDS))
            {
                LastGCCollectTime = nowTime;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        UnmanagedObjects.Clear();
    }

    /// <summary>
    /// 处理获取对象异常问题
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="action">获取对象委托</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>T</returns>
    private static T CatchOrDefault<T>(Func<T> action, T defaultValue = null)
        where T : class
    {
        try
        {
            return action();
        }
        catch
        {
            return defaultValue ?? null;
        }
    }
}