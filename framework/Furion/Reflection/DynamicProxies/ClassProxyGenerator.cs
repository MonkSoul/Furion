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