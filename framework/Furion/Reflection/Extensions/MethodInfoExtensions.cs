// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
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