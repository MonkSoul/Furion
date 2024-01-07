// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DistributedIDGenerator;

/// <summary>
/// 分布式 ID 生成器
/// </summary>
public interface IDistributedIDGenerator
{
    /// <summary>
    /// 生成逻辑
    /// </summary>
    /// <param name="idGeneratorOptions"></param>
    /// <returns></returns>
    object Create(object idGeneratorOptions = default);
}