// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// Sql 执行代理依赖接口
/// </summary>
public interface ISqlDispatchProxy
{
    /// <summary>
    /// 切换数据库上下文定位器
    /// </summary>
    /// <typeparam name="TDbContextLocator"></typeparam>
    /// <returns></returns>
    public void Change<TDbContextLocator>()
        where TDbContextLocator : IDbContextLocator
    { }

    /// <summary>
    /// 重置运行时数据库上下文定位器
    /// </summary>
    public void ResetIt() { }
}