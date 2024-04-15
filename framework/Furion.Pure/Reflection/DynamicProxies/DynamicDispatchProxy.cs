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

using System.Reflection;

namespace Furion.Reflection;

/// <summary>
/// <see cref="DispatchProxy"/> 重写抽象类
/// </summary>
/// <remarks>解决 <see cref="DispatchProxy"/> 不支持异步问题</remarks>
public abstract class DynamicDispatchProxy : DispatchProxy
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public DynamicDispatchProxy() : base() { }

    /// <summary>
    /// 代理实例
    /// </summary>
    private object Target { get; set; }

    /// <summary>
    /// 额外数据
    /// </summary>
    private Dictionary<object, object> Properties { get; set; }

    /// <summary>
    /// 创建代理对象
    /// </summary>
    /// <param name="interfaceType">接口</param>
    /// <param name="proxyType"><see cref="DynamicDispatchProxy"/> 派生类</param>
    /// <param name="target">代理实例</param>
    /// <param name="properties">额外数据</param>
    /// <returns><see cref="object"/></returns>
    public static object Decorate(Type interfaceType, Type proxyType, object target, Dictionary<object, object> properties = default)
    {
        // 代理派生类检查
        if (!typeof(DynamicDispatchProxy).IsAssignableFrom(proxyType)) throw new InvalidOperationException($"Type {proxyType} is not a {nameof(DynamicDispatchProxy)} derived type.");

        return _decorateMethod.MakeGenericMethod(interfaceType, proxyType).Invoke(null, new object[] { target, properties });
    }

    /// <summary>
    /// 创建代理对象
    /// </summary>
    /// <typeparam name="TService">接口</typeparam>
    /// <typeparam name="TProxy"><see cref="DynamicDispatchProxy"/> 派生类</typeparam>
    /// <param name="target">代理实例</param>
    /// <param name="properties">额外数据</param>
    /// <returns>接口对象</returns>
    public static TService Decorate<TService, TProxy>(object target, Dictionary<object, object> properties = default)
        where TService : class
        where TProxy : DynamicDispatchProxy
    {
        var proxy = Create<TService, TProxy>() as DynamicDispatchProxy;
        proxy.Target = target;
        proxy.Properties = properties ?? new Dictionary<object, object>();

        return proxy as TService;
    }

    /// <summary>
    /// 创建代理对象
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    /// <typeparam name="TProxy"><see cref="DynamicDispatchProxy"/> 派生类</typeparam>
    /// <param name="target">代理实例</param>
    /// <param name="properties">额外数据</param>
    /// <returns>代理实例</returns>
    public static dynamic DecorateClass<TClass, TProxy>(TClass target, Dictionary<object, object> properties = default)
        where TClass : class
        where TProxy : DynamicDispatchProxy
    {
        return ClassProxyGenerator<TClass>.Decorate<TProxy>(target, properties);
    }

    /// <summary>
    /// 创建代理对象
    /// </summary>
    /// <param name="proxyType"><see cref="DynamicDispatchProxy"/> 派生类</param>
    /// <param name="target">代理实例</param>
    /// <param name="properties">额外数据</param>
    /// <returns>代理实例</returns>
    public static dynamic DecorateClass(Type proxyType, object target, Dictionary<object, object> properties = default)
    {
        // 空检查
        if (target == null) throw new ArgumentNullException(nameof(target));

        return _decorateClassMethod.MakeGenericMethod(target.GetType(), proxyType).Invoke(null, new object[] { target, properties });
    }

    /// <summary>
    /// 同步拦截
    /// </summary>
    /// <param name="invocation"><see cref="Invocation"/></param>
    /// <returns><see cref="object"/></returns>
    public abstract object Invoke(Invocation invocation);

    /// <summary>
    /// 异步拦截
    /// </summary>
    /// <param name="invocation"><see cref="Invocation"/></param>
    /// <returns><see cref="Task"/></returns>
    public abstract Task InvokeAsync(Invocation invocation);

    /// <summary>
    /// 异步带返回值拦截
    /// </summary>
    /// <typeparam name="T">泛型值</typeparam>
    /// <param name="invocation"><see cref="Invocation"/></param>
    /// <returns><see cref="Task{TResult}"/></returns>
    public abstract Task<T> InvokeAsync<T>(Invocation invocation);

    /// <summary>
    /// 重写拦截调用方法
    /// </summary>
    /// <param name="targetMethod">接口方法</param>
    /// <param name="args">调用参数</param>
    /// <returns><see cref="object"/></returns>
    protected override object Invoke(MethodInfo targetMethod, object[] args)
    {
        // 创建方法调用器
        var invocation = new Invocation(targetMethod, args, Target, Properties);

        // 方法返回值类型
        var returnType = targetMethod.ReturnType;

        // 处理返回值 Task 方法
        if (returnType == typeof(Task))
        {
            return InvokeAsync(invocation);
        }
        // 处理返回值 Task<> 方法
        else if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
        {
            return _invokeAsyncOfTMethod.MakeGenericMethod(returnType.GenericTypeArguments).Invoke(this, new[] { invocation });
        }
        // 处理同步方法
        else
        {
            return !targetMethod.IsGenericMethod ? Invoke(invocation) : invocation.Proceed();
        }
    }

    /// <summary>
    /// <see cref="Decorate{TService, TProxy}(object, Dictionary{object, object})"/> 泛型方法
    /// </summary>
    private static readonly MethodInfo _decorateMethod = typeof(DynamicDispatchProxy).GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly).Single(u => u.Name == nameof(Decorate) && u.IsGenericMethod);

    /// <summary>
    /// <see cref="DecorateClass{TClass, TProxy}(TClass, Dictionary{object, object})"/> 泛型方法
    /// </summary>
    private static readonly MethodInfo _decorateClassMethod = typeof(DynamicDispatchProxy).GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly).Single(u => u.Name == nameof(DecorateClass) && u.IsGenericMethod);

    /// <summary>
    /// <see cref="InvokeAsync{T}(Invocation)"/> 泛型方法
    /// </summary>
    private static readonly MethodInfo _invokeAsyncOfTMethod = typeof(DynamicDispatchProxy).GetMethod(nameof(InvokeAsync), 1, BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, new[] { typeof(Invocation) }, null);
}