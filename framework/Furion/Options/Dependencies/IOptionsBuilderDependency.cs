// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Options;

/// <summary>
/// 选项构建器依赖接口
/// </summary>
/// <typeparam name="TOptions">选项类型</typeparam>
public interface IOptionsBuilderDependency<TOptions>
    where TOptions : class
{
}