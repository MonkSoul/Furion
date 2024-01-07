// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion;

/// <summary>
/// 注册服务启动配置
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Class)]
public class AppStartupAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="order"></param>
    public AppStartupAttribute(int order)
    {
        Order = order;
    }

    /// <summary>
    /// 排序
    /// </summary>
    public int Order { get; set; }
}