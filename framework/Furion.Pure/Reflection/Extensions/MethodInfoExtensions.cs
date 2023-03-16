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

using System.Reflection;

namespace Furion.Reflection.Extensions;

/// <summary>
/// Method Info 拓展
/// </summary>
[SuppressSniffer]
public static class MethodInfoExtensions
{
    /// <summary>
    /// 获取真实方法的特性集合
    /// </summary>
    /// <param name="method"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static IEnumerable<Attribute> GetActualCustomAttributes(this MethodInfo method, object target)
    {
        return GetActualMethodInfo(method, target)?.GetCustomAttributes();
    }

    /// <summary>
    /// 获取真实方法的特性集合
    /// </summary>
    /// <param name="method"></param>
    /// <param name="target"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static object[] GetActualCustomAttributes(this MethodInfo method, object target, bool inherit)
    {
        return GetActualMethodInfo(method, target)?.GetCustomAttributes(inherit);
    }

    /// <summary>
    /// 获取真实方法的特性集合
    /// </summary>
    /// <param name="method"></param>
    /// <param name="target"></param>
    /// <param name="attributeType"></param>
    /// <returns></returns>
    public static IEnumerable<Attribute> GetActualCustomAttributes(this MethodInfo method, object target, Type attributeType)
    {
        return GetActualMethodInfo(method, target)?.GetCustomAttributes(attributeType);
    }

    /// <summary>
    /// 获取真实方法的特性集合
    /// </summary>
    /// <param name="method"></param>
    /// <param name="target"></param>
    /// <param name="attributeType"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static object[] GetActualCustomAttributes(this MethodInfo method, object target, Type attributeType, bool inherit)
    {
        return GetActualMethodInfo(method, target)?.GetCustomAttributes(attributeType, inherit);
    }

    /// <summary>
    /// 获取真实方法的特性集合
    /// </summary>
    /// <param name="method"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static IEnumerable<TAttribute> GetActualCustomAttributes<TAttribute>(this MethodInfo method, object target)
        where TAttribute : Attribute
    {
        return GetActualMethodInfo(method, target)?.GetCustomAttributes<TAttribute>();
    }

    /// <summary>
    /// 获取真实方法的特性集合
    /// </summary>
    /// <param name="method"></param>
    /// <param name="target"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static IEnumerable<TAttribute> GetActualCustomAttributes<TAttribute>(this MethodInfo method, object target, bool inherit)
        where TAttribute : Attribute
    {
        return GetActualMethodInfo(method, target)?.GetCustomAttributes<TAttribute>(inherit);
    }

    /// <summary>
    /// 获取真实方法的特性
    /// </summary>
    /// <param name="method"></param>
    /// <param name="target"></param>
    /// <param name="attributeType"></param>
    /// <returns></returns>
    public static Attribute GetActualCustomAttribute(this MethodInfo method, object target, Type attributeType)
    {
        return GetActualMethodInfo(method, target)?.GetCustomAttribute(attributeType);
    }

    /// <summary>
    /// 获取真实方法的特性
    /// </summary>
    /// <param name="method"></param>
    /// <param name="target"></param>
    /// <param name="attributeType"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static Attribute GetActualCustomAttribute(this MethodInfo method, object target, Type attributeType, bool inherit)
    {
        return GetActualMethodInfo(method, target)?.GetCustomAttribute(attributeType, inherit);
    }

    /// <summary>
    /// 获取真实方法的特性
    /// </summary>
    /// <param name="method"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static TAttribute GetActualCustomAttribute<TAttribute>(this MethodInfo method, object target)
        where TAttribute : Attribute
    {
        return GetActualMethodInfo(method, target)?.GetCustomAttribute<TAttribute>();
    }

    /// <summary>
    /// 获取真实方法的特性
    /// </summary>
    /// <param name="method"></param>
    /// <param name="target"></param>
    /// <param name="inherit"></param>
    /// <returns></returns>
    public static TAttribute GetActualCustomAttribute<TAttribute>(this MethodInfo method, object target, bool inherit)
        where TAttribute : Attribute
    {
        return GetActualMethodInfo(method, target)?.GetCustomAttribute<TAttribute>(inherit);
    }

    /// <summary>
    /// 获取实际方法对象
    /// </summary>
    /// <param name="method"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    private static MethodInfo GetActualMethodInfo(MethodInfo method, object target)
    {
        if (target == null) return default;

        var targetType = target.GetType();
        var actualMethod = targetType.GetMethods()
                                             .FirstOrDefault(u => u.ToString().Equals(method.ToString()));

        if (actualMethod == null) return default;

        return actualMethod;
    }
}