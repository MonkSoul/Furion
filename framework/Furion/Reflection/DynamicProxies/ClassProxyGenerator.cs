// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Loader;
using System.Text;

namespace Furion.Reflection;

/// <summary>
/// Class 代理类生成器
/// </summary>
/// <typeparam name="TClass">代理类型</typeparam>
[SuppressSniffer]
public class ClassProxyGenerator<TClass>
    where TClass : class
{
    /// <summary>
    /// 代理程序集
    /// </summary>
    private static readonly Assembly _assembly;

    /// <summary>
    /// 代理接口
    /// </summary>
    private static readonly Type _interfaceType;

    /// <summary>
    /// 静态构造函数
    /// </summary>
    /// <remarks>用来缓存类型生成鸭子类型</remarks>
    static ClassProxyGenerator()
    {
        // 代理类型
        var classType = typeof(TClass);

        // 抽象类/密封类检查
        if (classType.IsSealed || classType.IsAbstract) throw new InvalidOperationException($"Type {classType} cannot be an abstract or sealed class.");

        // [代理]类型名称（不含泛型）
        var originName = (!classType.IsGenericType
            ? classType.Name
            : classType.Name[..classType.Name.IndexOf('`')]);
        var proxyOriginName = originName + "Proxy";

        // [代理]类型名称（含泛型）
        var genericDefinition = !classType.IsGenericType ? string.Empty : $"<{string.Join(',', classType.GetGenericTypeDefinition().GetGenericArguments().Select(t => t.Name))}>";
        var @type_name = originName + genericDefinition;
        var @proxy_type_name = proxyOriginName + genericDefinition;

        // ============ 生成接口方法 ============
        var @interface_methods = new StringBuilder();
        var methods = classType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        for (var m = 0; m < methods.Length; m++)
        {
            var method = methods[m];

            // 方法原始名称
            var methodOriginName = method.Name;

            // 代理方法名称（含泛型）
            var @method_name = !method.IsGenericMethod
                ? methodOriginName
                : $"{methodOriginName}<{string.Join(',', method.GetGenericMethodDefinition().GetGenericArguments().Select(t => t.Name))}>";

            // 返回值类型
            var @return_type = TypeConvertToRawString(method.ReturnType);

            // 生成参数定义
            var parameterInfos = method.GetParameters();
            var methodParametersStringBuilder = new StringBuilder();

            for (var i = 0; i < parameterInfos.Length; i++)
            {
                var parameter = parameterInfos[i];
                var parameterName = parameter.Name;
                var parameterType = TypeConvertToRawString(parameter.ParameterType);

                methodParametersStringBuilder.Append($"{parameterType} {parameterName}");
                if (i < parameterInfos.Length - 1) methodParametersStringBuilder.Append(',');
            }
            var @parameters = methodParametersStringBuilder.ToString();

            // 生成完整的方法定义
            @interface_methods.Append(DUCK_METHOD_TEMPLATE
                .Replace("@return_type", @return_type)
                .Replace("@method_name", @method_name)
                .Replace("@parameters", parameters));
            if (m < methods.Length - 1) @interface_methods.AppendLine();
        }

        // ============ 生成构造函数 ============
        var @constructor_methods = new StringBuilder();
        var constructors = classType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        for (var c = 0; c < constructors.Length; c++)
        {
            var constructor = constructors[c];

            // 生成参数定义
            var parameterInfos = constructor.GetParameters();
            var @parameters = new StringBuilder();
            var @call_parameters = new StringBuilder();

            for (var i = 0; i < parameterInfos.Length; i++)
            {
                var parameter = parameterInfos[i];
                var parameterName = parameter.Name;
                var parameterType = TypeConvertToRawString(parameter.ParameterType);

                @parameters.Append($"{parameterType} {parameterName}");
                @call_parameters.Append($"{parameterName}");
                if (i < parameterInfos.Length - 1)
                {
                    @parameters.Append(',');
                    @call_parameters.Append(',');
                }
            }

            // 生成完整的构造函数定义
            @constructor_methods.Append(DUCK_CONSTRUCTOR_TEMPLATE
                .Replace("@proxy_origin_name", proxyOriginName)
                .Replace("@parameters", @parameters.ToString())
                .Replace("@call_parameters", @call_parameters.ToString()));
            if (c < constructors.Length - 1) @constructor_methods.AppendLine();
        }

        // 命名空间、接口名称
        var @namespace = classType.Namespace ?? classType.Assembly.GetName().Name;
        var @interface_name = "I" + proxyOriginName + genericDefinition;

        // 生成最终代理类型定义代码
        var duckCode = DUCK_CLASS_TYPE_TEMPLATE
            .Replace("@namespace", @namespace)
            .Replace("@interface_name", @interface_name)
            .Replace("@proxy_type_name", @proxy_type_name)
            .Replace("@type_name", @type_name)
            .Replace("@constructor_methods", @constructor_methods.ToString())
            .Replace("@interface_methods", @interface_methods.ToString());

        // 编译代码并返回程序集
        var typeAssembly = classType.Assembly;
        _assembly = CompileCSharpClassCode(duckCode
             , null
             , new[] { typeAssembly }.Concat(typeAssembly.GetReferencedAssemblies().Select(assName => AssemblyLoadContext.Default.LoadFromAssemblyName(assName))));
        _interfaceType = _assembly.GetTypes().First(u => u.IsInterface);
    }

    /// <summary>
    /// 创建代理对象
    /// </summary>
    /// <typeparam name="TProxy"><see cref="DynamicDispatchProxy"/> 派生类</typeparam>
    /// <param name="target">代理实例</param>
    /// <param name="properties">额外数据</param>
    /// <returns><see cref="object"/></returns>
    public static dynamic Decorate<TProxy>(TClass target, Dictionary<object, object> properties = default)
        where TProxy : DynamicDispatchProxy
    {
        // 处理泛型类型
        var interfaceType = target == null
            ? _interfaceType
            : target.GetType().IsGenericType
                ? _interfaceType.MakeGenericType(target.GetType().GenericTypeArguments)
                : _interfaceType;

        return DynamicDispatchProxy.Decorate(interfaceType, typeof(TProxy), target, properties);
    }

    /// <summary>
    /// 编译 C# 类定义代码
    /// </summary>
    /// <param name="csharpCode">字符串代码</param>
    /// <param name="assemblyName">自定义程序集名称</param>
    /// <param name="additionalAssemblies">附加的程序集</param>
    /// <returns><see cref="Assembly"/></returns>
    private static Assembly CompileCSharpClassCode(string csharpCode, string assemblyName = default, IEnumerable<Assembly> additionalAssemblies = default)
    {
        // 空检查
        if (csharpCode == null) throw new ArgumentNullException(nameof(csharpCode));

        // 默认程序集
        var defaultAssemblies = new[] { typeof(object).Assembly };
        var references = additionalAssemblies?.Any() == true ? defaultAssemblies.Concat(additionalAssemblies) : defaultAssemblies;

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
          new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, optimizationLevel: OptimizationLevel.Release));

        // 编译代码
        using var memoryStream = new MemoryStream();
        var emitResult = compilation.Emit(memoryStream);

        // 编译失败抛出异常
        if (!emitResult.Success)
        {
            throw new InvalidOperationException($"Unable to compile class code: {string.Join("\n", emitResult.Diagnostics.ToList().Where(w => w.IsWarningAsError || w.Severity == DiagnosticSeverity.Error))}");
        }

        memoryStream.Position = 0;

        // 返回编译程序集
        return Assembly.Load(memoryStream.ToArray());
    }

    /// <summary>
    /// 转换类型为字符串定义方式
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns><see cref="string"/></returns>
    private static string TypeConvertToRawString(Type type)
    {
        if (type == null) return string.Empty;

        var typeName = type.FullName ?? (!string.IsNullOrEmpty(type.Namespace) ? type.Namespace + "." : string.Empty) + type.Name;

        // 处理泛型类型问题
        if (type.IsConstructedGenericType)
        {
            var prefix = type.GetGenericArguments()
                .Select(genericArg => TypeConvertToRawString(genericArg))
                .Aggregate((previous, current) => previous + ", " + current);

            typeName = typeName.Split('`').First() + "<" + prefix + ">";
        }

        return typeName;
    }

    /// <summary>
    /// 代理类模板常量
    /// </summary>
    private const string DUCK_CLASS_TYPE_TEMPLATE = @"
namespace @namespace;

internal interface @interface_name
{
@interface_methods
}

internal class @proxy_type_name: @type_name, @interface_name
{
@constructor_methods
}";

    /// <summary>
    /// 代理方法模板常量
    /// </summary>
    private const string DUCK_METHOD_TEMPLATE = @"  @return_type @method_name(@parameters);";

    /// <summary>
    /// 代理构造函数模板常量
    /// </summary>
    private const string DUCK_CONSTRUCTOR_TEMPLATE = @"  public @proxy_origin_name(@parameters): base(@call_parameters) {}";
}