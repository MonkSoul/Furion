// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.DistributedIDGenerator;

/// <summary>
/// ID 生成器
/// </summary>
[SuppressSniffer]
public static class IDGen
{
    /// <summary>
    /// 生成唯一 ID
    /// </summary>
    /// <param name="idGeneratorOptions"></param>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static object NextID(object idGeneratorOptions, IServiceProvider serviceProvider = default)
    {
        return App.GetService<IDistributedIDGenerator>(serviceProvider ?? App.RootServices).Create(idGeneratorOptions);
    }

    /// <summary>
    /// 生成连续 GUID
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static Guid NextID(IServiceProvider serviceProvider = default)
    {
        var sequentialGuid = App.GetService(typeof(SequentialGuidIDGenerator), serviceProvider ?? App.RootServices) as IDistributedIDGenerator;
        return (Guid)sequentialGuid.Create();
    }
}