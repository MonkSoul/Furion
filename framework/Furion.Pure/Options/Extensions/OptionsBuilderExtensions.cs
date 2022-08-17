// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Furion.Extensions;
using Furion.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.Extensions.Options;

/// <summary>
/// OptionsBuilder 拓展类
/// </summary>
[SuppressSniffer]
public static class OptionsBuilderExtensions
{
    /// <summary>
    /// 配置选项构建器
    /// </summary>
    /// <typeparam name="TOptions">选项类型</typeparam>
    /// <param name="optionsBuilder">选项构建器实例</param>
    /// <param name="configuration">配置对象</param>
    /// <param name="optionsBuilderType">选项构建器类型，默认为 typeof(TOptions) </param>
    /// <returns>选项构建器实例</returns>
    public static OptionsBuilder<TOptions> ConfigureBuilder<TOptions>(this OptionsBuilder<TOptions> optionsBuilder
        , IConfiguration configuration
        , Type optionsBuilderType = default)
        where TOptions : class
    {
        // 配置默认处理和选项构建器
        optionsBuilder.ConfigureDefaults(configuration)
            .ConfigureBuilder(optionsBuilderType);

        return optionsBuilder;
    }

    /// <summary>
    /// 配置多个选项构建器
    /// </summary>
    /// <typeparam name="TOptions">选项类型</typeparam>
    /// <param name="optionsBuilder">选项构建器实例</param>
    /// <param name="configuration">配置对象</param>
    /// <param name="optionsBuilderTypes">配置多个选项构建器</param>
    /// <returns>选项构建器实例</returns>
    /// <exception cref="ArgumentNullException" />
    public static OptionsBuilder<TOptions> ConfigureBuilders<TOptions>(this OptionsBuilder<TOptions> optionsBuilder
        , IConfiguration configuration
        , Type[] optionsBuilderTypes)
        where TOptions : class
    {
        // 配置默认处理和多个选项构建器
        optionsBuilder.ConfigureDefaults(configuration)
            .ConfigureBuilders(optionsBuilderTypes);

        return optionsBuilder;
    }

    /// <summary>
    /// 配置选项构建器
    /// </summary>
    /// <typeparam name="TOptions">选项类型</typeparam>
    /// <param name="optionsBuilder">选项构建器实例</param>
    /// <param name="optionsBuilderType">选项构建器类型，默认为 typeof(TOptions) </param>
    /// <returns>选项构建器实例</returns>
    public static OptionsBuilder<TOptions> ConfigureBuilder<TOptions>(this OptionsBuilder<TOptions> optionsBuilder, Type optionsBuilderType = default)
        where TOptions : class
    {
        optionsBuilderType ??= typeof(TOptions);
        var optionsBuilderDependency = typeof(IOptionsBuilderDependency<TOptions>);

        // 获取所有构建器依赖接口
        var builderInterfaces = optionsBuilderType.GetInterfaces()
            .Where(u => optionsBuilderDependency.IsAssignableFrom(u) && u != optionsBuilderDependency);

        if (!builderInterfaces.Any())
        {
            return optionsBuilder;
        }

        // 逐条调用 .NET 底层选项配置方法
        foreach (var builderInterface in builderInterfaces)
        {
            InvokeMapMethod(optionsBuilder, optionsBuilderType, builderInterface);
        }

        return optionsBuilder;
    }

    /// <summary>
    /// 配置多个选项构建器
    /// </summary>
    /// <typeparam name="TOptions">选项类型</typeparam>
    /// <param name="optionsBuilder">选项构建器实例</param>
    /// <param name="optionsBuilderTypes">配置多个选项构建器</param>
    /// <returns>选项构建器实例</returns>
    /// <exception cref="ArgumentNullException" />
    public static OptionsBuilder<TOptions> ConfigureBuilders<TOptions>(this OptionsBuilder<TOptions> optionsBuilder, Type[] optionsBuilderTypes)
        where TOptions : class
    {
        // 处理空对象或空值
        if (optionsBuilderTypes.IsEmpty())
        {
            throw new ArgumentNullException(nameof(optionsBuilderTypes));
        }

        // 逐条配置选项构建器
        Array.ForEach(optionsBuilderTypes, optionsBuilderType =>
        {
            optionsBuilder.ConfigureBuilder(optionsBuilderType);
        });

        return optionsBuilder;
    }

    /// <summary>
    /// 配置选项常规默认处理
    /// </summary>
    /// <typeparam name="TOptions">选项类型</typeparam>
    /// <param name="optionsBuilder">选项构建器实例</param>
    /// <param name="configuration">配置对象</param>
    /// <returns>选项构建器实例</returns>
    public static OptionsBuilder<TOptions> ConfigureDefaults<TOptions>(this OptionsBuilder<TOptions> optionsBuilder, IConfiguration configuration)
        where TOptions : class
    {
        // 获取 [OptionsBuilder] 特性
        var optionsType = typeof(TOptions);
        var optionsBuilderAttribute = typeof(TOptions).GetTypeAttribute<OptionsBuilderAttribute>();

        // 解析配置类型（自动识别是否是节点配置对象）
        var configurationSection = configuration is IConfigurationSection section
                                    ? section
                                    : configuration.GetSection(
                                          string.IsNullOrWhiteSpace(optionsBuilderAttribute?.SectionKey)
                                              ? optionsType.Name.ClearStringAffixes(1, Constants.OptionsTypeSuffix)
                                              : optionsBuilderAttribute.SectionKey
                                       );

        // 绑定配置
        optionsBuilder.Bind(configurationSection, binderOptions =>
        {
#if !NET5_0
            binderOptions.ErrorOnUnknownConfiguration = optionsBuilderAttribute?.ErrorOnUnknownConfiguration ?? false;
#endif
            binderOptions.BindNonPublicProperties = optionsBuilderAttribute?.BindNonPublicProperties ?? false;
        });

        // 注册验证特性支持
        if (optionsBuilderAttribute?.ValidateDataAnnotations == true)
        {
            optionsBuilder.ValidateDataAnnotations();
        }

        // 注册复杂验证类型
        if (optionsBuilderAttribute?.ValidateOptionsTypes.IsEmpty() == false)
        {
            var validateOptionsType = typeof(IValidateOptions<TOptions>);

            // 注册复杂选项
            Array.ForEach(optionsBuilderAttribute.ValidateOptionsTypes!, type =>
            {
                optionsBuilder.Services.TryAddEnumerable(ServiceDescriptor.Singleton(validateOptionsType, type));
            });
        }

        return optionsBuilder;
    }

    /// <summary>
    /// 调用 OptionsBuilder{TOptions} 对应方法
    /// </summary>
    /// <param name="optionsBuilder">选项构建器实例</param>
    /// <param name="optionsBuilderType">选项构建器类型</param>
    /// <param name="builderInterface">构建器接口</param>
    private static void InvokeMapMethod(object optionsBuilder
        , Type optionsBuilderType
        , Type builderInterface)
    {
        // 获取接口对应 OptionsBuilder 方法映射特性
        var optionsBuilderMethodMapAttribute = builderInterface.GetCustomAttribute<OptionsBuilderMethodMapAttribute>()!;
        var methodName = optionsBuilderMethodMapAttribute.MethodName;

        // 获取选项构建器接口实际泛型参数
        var genericArguments = builderInterface.GetGenericArguments();

        // 获取匹配的配置方法
        var bindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        var matchMethod = optionsBuilderType.GetMethods(bindingAttr)
            .First(u => u.Name == methodName || u.Name.EndsWith("." + methodName) && u.GetParameters().Length == genericArguments.Length);

        // 构建表达式实际传入参数
        var parameterExpressions = BuildExpressionCallParameters(matchMethod
            , !optionsBuilderMethodMapAttribute.VoidReturn
            , genericArguments
            , out var args);

        // 创建 OptionsBuilder<TOptions> 实例对应调用方法表达式
        var callExpression = Expression.Call(Expression.Constant(optionsBuilder)
            , methodName
            , genericArguments.IsEmpty() ? default : genericArguments!.Skip(1).ToArray()
            , parameterExpressions);

        // 创建调用委托
        var @delegate = Expression.Lambda(callExpression, parameterExpressions).Compile();

        // 动态调用
        @delegate.DynamicInvoke(args);
    }

    /// <summary>
    /// 构建 Call 调用方法表达式参数
    /// </summary>
    /// <remarks>含实际传入参数</remarks>
    /// <param name="matchMethod">表达式匹配方法</param>
    /// <param name="isValidateMethod">是否校验方法</param>
    /// <param name="genericArguments">泛型参数</param>
    /// <param name="args">实际传入参数</param>
    /// <returns>调用参数表达式数组</returns>
    private static ParameterExpression[] BuildExpressionCallParameters(MethodInfo matchMethod
        , bool isValidateMethod
        , Type[] genericArguments
        , out object[] args)
    {
        /*
         * 该方法目的是构建符合 OptionsBuilder 对象的 Configure、PostConfigure、Validate 方法签名委托参数表达式，如：
         * Configure/PostConfigure: [Method](Action<TDep1, .. TDep5>);
         * Validate: [Method](Func<TOptions, TDep1, .. TDep5, bool>, string);
         */

        // 创建调用方法第一个委托参数表达式
        var delegateType = CreateDelegate(genericArguments, !isValidateMethod ? default : typeof(bool));

        var arg0Expression = Expression.Parameter(delegateType, "arg0");
        var arg0 = matchMethod.CreateDelegate(delegateType, default);

        // 创建调用方法第二个字符串参数表达式（仅限 Validate 方法使用）
        ParameterExpression arg1Expression = default;
        string arg1 = default;

        if (isValidateMethod)
        {
            // 获取 [FailureMessage] 特性配置
            arg1 = matchMethod.IsDefined(typeof(FailureMessageAttribute))
                ? matchMethod.GetCustomAttribute<FailureMessageAttribute>()!.Text
                : default;

            if (!string.IsNullOrWhiteSpace(arg1))
            {
                arg1Expression = Expression.Parameter(typeof(string), "arg1");
            }
        }

        // 设置调用方法实际传入参数
        args = arg1Expression == default
            ? new object[] { arg0 }
            : new object[] { arg0, arg1! };

        // 返回调用方法参数定义表达式
        return arg1Expression == default
            ? new[] { arg0Expression }
            : new[] { arg0Expression, arg1Expression };
    }

    /// <summary>
    /// 创建委托类型
    /// </summary>
    /// <param name="inputTypes">输入类型</param>
    /// <param name="outputType">输出类型</param>
    /// <returns>Action或Func 委托类型</returns>
    internal static Type CreateDelegate(Type[] inputTypes, Type outputType = default)
    {
        var isFuncDelegate = outputType != default;

        // 获取基础委托类型，通过是否带返回值判断
        var baseDelegateType = !isFuncDelegate ? typeof(Action) : typeof(Func<>);

        // 处理无输入参数委托类型
        if (inputTypes.IsEmpty())
        {
            return !isFuncDelegate
                ? baseDelegateType
                : baseDelegateType.MakeGenericType(outputType!);
        }

        // 处理含输入参数委托类型
        return !isFuncDelegate
            ? baseDelegateType.Assembly.GetType($"{baseDelegateType.FullName}`{inputTypes!.Length}")!.MakeGenericType(inputTypes)
            : baseDelegateType.Assembly.GetType($"{(baseDelegateType.FullName![0..^2])}`{inputTypes!.Length + 1}")
                !.MakeGenericType(inputTypes.Concat(new[] { outputType! }).ToArray());
    }
}