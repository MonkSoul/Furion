// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DatabaseAccessor;

/// <summary>
/// 基于数据库表的多租户模式
/// </summary>
public interface IMultiTenantOnTable : IPrivateMultiTenant
{
    /// <summary>
    /// 获取租户Id
    /// </summary>
    /// <returns></returns>
    object GetTenantId();
}