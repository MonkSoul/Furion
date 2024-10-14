// 版权归百小僧及百签科技（广东）有限公司所有。
// 
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.HttpRemote;

/// <summary>
///     <see cref="ObjectContentConverter{TResult}" /> 工厂
/// </summary>
public interface IObjectContentConverterFactory
{
    /// <summary>
    ///     获取 <see cref="ObjectContentConverter{TResult}" /> 实例
    /// </summary>
    /// <typeparam name="TResult">转换的目标类型</typeparam>
    /// <returns>
    ///     <see cref="ObjectContentConverter{TResult}" />
    /// </returns>
    ObjectContentConverter<TResult> GetConverter<TResult>();
}