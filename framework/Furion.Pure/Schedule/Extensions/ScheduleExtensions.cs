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

using System.Reflection;

namespace Furion.Schedule;

/// <summary>
/// Schedule 模块拓展类
/// </summary>
[SuppressSniffer]
public static class ScheduleExtensions
{
    /// <summary>
    /// 判断类型是否时 IJob 实现类型
    /// </summary>
    /// <param name="jobType">类型</param>
    /// <returns><see cref="bool"/></returns>
    public static bool IsJobType(this Type jobType)
    {
        // 检查 jobType 类型是否实现 IJob 接口
        if (!typeof(IJob).IsAssignableFrom(jobType)
            || jobType.IsInterface
            || jobType.IsAbstract) return false;

        return true;
    }

    /// <summary>
    /// 扫描作业类型触发器特性
    /// </summary>
    /// <param name="jobType">作业类型</param>
    /// <returns><see cref="TriggerBuilder"/>[]</returns>
    public static TriggerBuilder[] ScanTriggers(this Type jobType)
    {
        // 空检查
        if (jobType == null) throw new ArgumentNullException(nameof(jobType));

        // 检查 jobType 类型是否实现 IJob 接口
        if (!jobType.IsJobType()) throw new InvalidOperationException("The <jobType> does not implement IJob interface.");

        // 扫描所有 [Trigger] 特性
        var triggerAttributes = jobType.GetCustomAttributes<TriggerAttribute>(true);

        var triggerBuilders = new List<TriggerBuilder>();

        // 遍历所有作业触发器特性并添加到集合中
        foreach (var triggerAttribute in triggerAttributes)
        {
            // 创建作业触发器并添加到当前作业触发器构建器中
            var triggerBuilder = TriggerBuilder.Create(triggerAttribute.RuntimeTriggerType)
                .SetArgs(triggerAttribute.RuntimeTriggerArgs)
                .SetTriggerId(triggerAttribute.TriggerId)
                .SetDescription(triggerAttribute.Description)
                .SetMaxNumberOfRuns(triggerAttribute.MaxNumberOfRuns)
                .SetMaxNumberOfErrors(triggerAttribute.MaxNumberOfErrors)
                .SetNumRetries(triggerAttribute.NumRetries)
                .SetRetryTimeout(triggerAttribute.RetryTimeout)
                .SetStartTime(triggerAttribute.RuntimeStartTime)
                .SetEndTime(triggerAttribute.RuntimeEndTime)
                .SetStartNow(triggerAttribute.StartNow)
                .SetRunOnStart(triggerAttribute.RunOnStart);

            triggerBuilders.Add(triggerBuilder);
        }

        return triggerBuilders.ToArray();
    }

    /// <summary>
    /// 对象映射
    /// </summary>
    /// <typeparam name="TTarget">目标类型</typeparam>
    /// <param name="source">源对象</param>
    /// <param name="target">目标类型对象</param>
    /// <param name="ignoreNullValue">忽略空值</param>
    /// <returns>目标类型对象</returns>
    internal static TTarget MapTo<TTarget>(this object source, object target = default, bool ignoreNullValue = false)
        where TTarget : class
    {
        if (source == null) return default;

        var sourceType = source.GetType();
        var targetType = typeof(TTarget);
        var bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        // 创建目标对象
        var constructors = targetType.GetConstructors(bindFlags);
        target ??= constructors.Length == 0 ? Activator.CreateInstance<TTarget>() : constructors[0].Invoke(null);

        var targetProperties = targetType.GetProperties(bindFlags);

        // 遍历实例属性并设置
        foreach (var property in targetProperties)
        {
            var propertyName = property.Name;

            // 下面代码使用 ”套娃“ 方式~~
            // 查找 CamelCase 属性命名
            var sourceProperty = sourceType.GetProperty(Penetrates.GetNaming(propertyName, NamingConventions.CamelCase), bindFlags);
            if (sourceProperty == null)
            {
                // 查找 Pascal 属性命名
                sourceProperty = sourceType.GetProperty(Penetrates.GetNaming(propertyName, NamingConventions.Pascal), bindFlags);
                if (sourceProperty == null)
                {
                    // 查找 UnderScoreCase 属性命名
                    sourceProperty = sourceType.GetProperty(Penetrates.GetNaming(propertyName, NamingConventions.UnderScoreCase), bindFlags);
                    if (sourceProperty == null)
                    {
                        continue;
                    }
                }
            }

            var value = sourceProperty.GetValue(source);

            // 忽略空值控制
            if (ignoreNullValue && value == null) continue;

            property.SetValue(target, value);
        }

        return target as TTarget;
    }
}