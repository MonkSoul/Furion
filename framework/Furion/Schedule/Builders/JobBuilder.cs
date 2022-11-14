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
/// 作业信息构建器
/// </summary>
[SuppressSniffer]
public sealed class JobBuilder : JobDetail
{
    /// <summary>
    /// 构造函数
    /// </summary>
    private JobBuilder()
    {
    }

    /// <summary>
    /// 创建作业信息构建器
    /// </summary>
    /// <typeparam name="TJob"><see cref="IJob"/> 实现类型</typeparam>
    /// <returns><see cref="JobBuilder"/></returns>
    public static JobBuilder Create<TJob>()
        where TJob : class, IJob
    {
        return Create(typeof(TJob));
    }

    /// <summary>
    /// 创建作业信息构建器
    /// </summary>
    /// <param name="assemblyName">作业类型所在程序集 Name</param>
    /// <param name="jobTypeFullName">作业类型 FullName</param>
    /// <returns><see cref="JobBuilder"/></returns>
    public static JobBuilder Create(string assemblyName, string jobTypeFullName)
    {
        return new JobBuilder()
            .SetJobType(assemblyName, jobTypeFullName);
    }

    /// <summary>
    /// 创建作业信息构建器
    /// </summary>
    /// <param name="jobType">作业类型</param>
    /// <returns><see cref="JobBuilder"/></returns>
    public static JobBuilder Create(Type jobType)
    {
        return new JobBuilder()
            .SetJobType(jobType);
    }

    /// <summary>
    /// 设置作业 Id
    /// </summary>
    /// <param name="jobId">作业 Id</param>
    /// <returns><see cref="JobBuilder"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public JobBuilder SetJobId(string jobId)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(jobId)) throw new ArgumentNullException(nameof(jobId));

        JobId = jobId;

        return this;
    }

    /// <summary>
    /// 设置作业组名称
    /// </summary>
    /// <param name="groupName">作业组名称</param>
    /// <returns><see cref="JobBuilder"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public JobBuilder SetGroupName(string groupName)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(groupName)) throw new ArgumentNullException(nameof(groupName));

        GroupName = groupName;

        return this;
    }

    /// <summary>
    /// 设置作业类型
    /// </summary>
    /// <param name="assemblyName">作业类型所在程序集 Name</param>
    /// <param name="jobTypeFullName">作业类型 FullName</param>
    /// <returns><see cref="JobBuilder"/></returns>
    public JobBuilder SetJobType(string assemblyName, string jobTypeFullName)
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(assemblyName)) throw new ArgumentNullException(nameof(assemblyName));
        if (string.IsNullOrWhiteSpace(jobTypeFullName)) throw new ArgumentNullException(nameof(jobTypeFullName));

        // 加载 GAC 全局应用程序缓存中的程序集及类型
        var jobType = Assembly.Load(assemblyName)
            .GetType(jobTypeFullName);

        return SetJobType(jobType);
    }

    /// <summary>
    /// 设置作业类型
    /// </summary>
    /// <param name="jobType">作业类型</param>
    /// <returns><see cref="JobBuilder"/></returns>
    public JobBuilder SetJobType(Type jobType)
    {
        // 检查 jobType 类型是否实现 IJob 接口
        if (!typeof(IJob).IsAssignableFrom(jobType)
            || jobType.IsInterface
            || jobType.IsAbstract) throw new InvalidOperationException("The <jobType> does not implement IJob interface.");

        AssemblyName = jobType.Assembly.GetName().Name;
        JobType = jobType.FullName;
        RuntimeJobType = jobType;

        return this;
    }

    /// <summary>
    /// 设置描述信息
    /// </summary>
    /// <param name="description">描述信息</param>
    /// <returns><see cref="JobBuilder"/></returns>
    public JobBuilder SetDescription(string description)
    {
        Description = description;

        return this;
    }

    /// <summary>
    /// 设置是否采用并发执行
    /// </summary>
    /// <param name="concurrent">是否并发执行</param>
    /// <returns><see cref="JobBuilder"/></returns>
    public JobBuilder SetConcurrent(bool concurrent)
    {
        Concurrent = concurrent;

        return this;
    }

    /// <summary>
    /// 设置是否扫描 IJob 实现类 [Trigger] 特性触发器
    /// </summary>
    /// <param name="includeAnnotations">是否扫描 IJob 实现类 [Trigger] 特性触发器</param>
    /// <returns><see cref="JobBuilder"/></returns>
    public JobBuilder SetIncludeAnnotations(bool includeAnnotations)
    {
        IncludeAnnotations = includeAnnotations;

        return this;
    }

    /// <summary>
    /// 设置作业额外数据
    /// </summary>
    /// <param name="properties">作业额外数据</param>
    /// <remarks>必须是 Dictionary{string, object} 类型序列化的结果</remarks>
    /// <returns><see cref="JobBuilder"/></returns>
    public JobBuilder SetProperties(string properties)
    {
        if (string.IsNullOrWhiteSpace(properties)) properties = "{}";

        var runtimeProperties = Penetrates.Deserialize<Dictionary<string, object>>(properties);
        RuntimeProperties = runtimeProperties;

        return this;
    }

    /// <summary>
    /// 添加作业额外数据
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns><see cref="JobBuilder"/></returns>
    public new JobBuilder AddProperty(string key, object value)
    {
        return base.AddProperty(key, value) as JobBuilder;
    }

    /// <summary>
    /// 添加或更新作业额外数据
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns><see cref="JobBuilder"/></returns>
    public new JobBuilder AddOrUpdateProperty(string key, object value)
    {
        return base.AddOrUpdateProperty(key, value) as JobBuilder;
    }

    /// <summary>
    /// 删除作业额外数据
    /// </summary>
    /// <param name="key">键</param>
    /// <returns><see cref="JobBuilder"/></returns>
    public new JobBuilder RemoveProperty(string key)
    {
        return base.RemoveProperty(key) as JobBuilder;
    }

    /// <summary>
    /// 清空作业额外数据
    /// </summary>
    /// <returns><see cref="JobBuilder"/></returns>
    public new JobBuilder ClearProperties()
    {
        return base.ClearProperties() as JobBuilder;
    }

    /// <summary>
    /// 隐藏作业信息公开方法
    /// </summary>
    /// <param name="key">键</param>
    /// <returns><see cref="object"/></returns>
    public new object GetProperty(string key) => throw new NotImplementedException();

    /// <summary>
    /// 隐藏作业信息公开方法
    /// </summary>
    /// <typeparam name="T">结果泛型类型</typeparam>
    /// <param name="key">键</param>
    /// <returns>T 类型</returns>
    public new T GetProperty<T>(string key) => throw new NotImplementedException();

    /// <summary>
    /// 将 <see cref="JobDetail"/> 转换成 <see cref="JobBuilder"/>
    /// </summary>
    /// <param name="jobDetail"></param>
    /// <returns></returns>
    internal static JobBuilder From(JobDetail jobDetail)
    {
        return jobDetail.MapTo<JobBuilder>();
    }

    /// <summary>
    /// 构建 <see cref="JobDetail"/> 对象
    /// </summary>
    /// <returns><see cref="JobDetail"/></returns>
    internal JobDetail Build()
    {
        // 空检查
        if (string.IsNullOrWhiteSpace(JobId)) throw new ArgumentNullException(nameof(JobId));

        return this.MapTo<JobDetail>();
    }
}