// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.FriendlyException;

/// <summary>
/// 异常错误代码提供器
/// </summary>
public interface IErrorCodeTypeProvider
{
    /// <summary>
    /// 错误代码定义类型
    /// </summary>
    Type[] Definitions { get; }
}