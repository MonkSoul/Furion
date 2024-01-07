// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

using System.Diagnostics.CodeAnalysis;

namespace Furion.Schedule;

/// <summary>
/// 支持重复 Key 的字典比较器
/// </summary>
internal class RepeatKeyEqualityComparer : IEqualityComparer<JobDetail>
{
    /// <summary>
    /// 相等比较
    /// </summary>
    /// <param name="x"><see cref="JobDetail"/></param>
    /// <param name="y"><see cref="JobDetail"/></param>
    /// <returns><see cref="bool"/></returns>
    public bool Equals(JobDetail x, JobDetail y)
    {
        return x != y;
    }

    /// <summary>
    /// 获取哈希值
    /// </summary>
    /// <param name="obj"><see cref="JobDetail"/></param>
    /// <returns><see cref="int"/></returns>
    public int GetHashCode([DisallowNull] JobDetail obj)
    {
        return obj.GetHashCode();
    }
}