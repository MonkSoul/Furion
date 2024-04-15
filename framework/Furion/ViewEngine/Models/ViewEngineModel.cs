// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

using System.Text;

namespace Furion.ViewEngine;

/// <summary>
/// 视图引擎模板模型实现类
/// </summary>
[SuppressSniffer]
public abstract class ViewEngineModel : IViewEngineModel
{
    /// <summary>
    /// 字符串构建器
    /// </summary>
    private readonly StringBuilder stringBuilder = new();

    /// <summary>
    /// 特性前缀
    /// </summary>
    private string attributeSuffix = null;

    /// <summary>
    /// 模型
    /// </summary>
    public dynamic Model { get; set; }

    /// <summary>
    /// 写入字面量
    /// </summary>
    /// <param name="literal"></param>
    public void WriteLiteral(string literal = null)
    {
        WriteLiteralAsync(literal).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 写入字面量
    /// </summary>
    /// <param name="literal"></param>
    /// <returns></returns>
    public virtual Task WriteLiteralAsync(string literal = null)
    {
        stringBuilder.Append(literal);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 写入对象
    /// </summary>
    /// <param name="obj"></param>
    public void Write(object obj = null)
    {
        WriteAsync(obj).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 写入对象
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public virtual Task WriteAsync(object obj = null)
    {
        stringBuilder.Append(obj);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 写入特性
    /// </summary>
    /// <param name="name"></param>
    /// <param name="prefix"></param>
    /// <param name="prefixOffset"></param>
    /// <param name="suffix"></param>
    /// <param name="suffixOffset"></param>
    /// <param name="attributeValuesCount"></param>
    public void BeginWriteAttribute(string name, string prefix, int prefixOffset, string suffix, int suffixOffset,
        int attributeValuesCount)
    {
        BeginWriteAttributeAsync(name, prefix, prefixOffset, suffix, suffixOffset, attributeValuesCount).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 写入特性
    /// </summary>
    /// <param name="name"></param>
    /// <param name="prefix"></param>
    /// <param name="prefixOffset"></param>
    /// <param name="suffix"></param>
    /// <param name="suffixOffset"></param>
    /// <param name="attributeValuesCount"></param>
    /// <returns></returns>
    public virtual Task BeginWriteAttributeAsync(string name, string prefix, int prefixOffset, string suffix, int suffixOffset, int attributeValuesCount)
    {
        attributeSuffix = suffix;
        stringBuilder.Append(prefix);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 写入特性值
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="prefixOffset"></param>
    /// <param name="value"></param>
    /// <param name="valueOffset"></param>
    /// <param name="valueLength"></param>
    /// <param name="isLiteral"></param>
    public void WriteAttributeValue(string prefix, int prefixOffset, object value, int valueOffset, int valueLength,
        bool isLiteral)
    {
        WriteAttributeValueAsync(prefix, prefixOffset, value, valueOffset, valueLength, isLiteral).GetAwaiter().GetResult();
    }

    /// <summary>
    /// 写入特性值
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="prefixOffset"></param>
    /// <param name="value"></param>
    /// <param name="valueOffset"></param>
    /// <param name="valueLength"></param>
    /// <param name="isLiteral"></param>
    /// <returns></returns>
    public virtual Task WriteAttributeValueAsync(string prefix, int prefixOffset, object value, int valueOffset, int valueLength, bool isLiteral)
    {
        stringBuilder.Append(prefix);
        stringBuilder.Append(value);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 结束写入特性
    /// </summary>
    public void EndWriteAttribute()
    {
        EndWriteAttributeAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// 结束写入特性
    /// </summary>
    /// <returns></returns>
    public virtual Task EndWriteAttributeAsync()
    {
        stringBuilder.Append(attributeSuffix);
        attributeSuffix = null;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 执行
    /// </summary>
    public void Execute()
    {
        ExecuteAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <returns></returns>
    public virtual Task ExecuteAsync()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获取结果
    /// </summary>
    /// <returns></returns>
    public virtual string Result()
    {
        return ResultAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// 获取结果
    /// </summary>
    /// <returns></returns>
    public virtual Task<string> ResultAsync()
    {
        return Task.FromResult<string>(stringBuilder.ToString());
    }
}

/// <summary>
/// 视图引擎模板模型实现类
/// </summary>
/// <typeparam name="T"></typeparam>
[SuppressSniffer]
public abstract class ViewEngineModel<T> : ViewEngineModel
{
    /// <summary>
    /// 强类型
    /// </summary>
    public new T Model { get; set; }
}