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

namespace Furion.ViewEngine;

/// <summary>
/// 视图引擎模板模型接口
/// </summary>
public interface IViewEngineModel
{
    /// <summary>
    /// 模型
    /// </summary>
    dynamic Model { get; set; }

    /// <summary>
    /// 写入字面量
    /// </summary>
    /// <param name="literal"></param>
    void WriteLiteral(string literal = null);

    /// <summary>
    /// 写入字面量
    /// </summary>
    /// <param name="literal"></param>
    /// <returns></returns>
    Task WriteLiteralAsync(string literal = null);

    /// <summary>
    /// 写入对象
    /// </summary>
    /// <param name="obj"></param>
    void Write(object obj = null);

    /// <summary>
    /// 写入对象
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    Task WriteAsync(object obj = null);

    /// <summary>
    /// 开始写入特性
    /// </summary>
    /// <param name="name"></param>
    /// <param name="prefix"></param>
    /// <param name="prefixOffset"></param>
    /// <param name="suffix"></param>
    /// <param name="suffixOffset"></param>
    /// <param name="attributeValuesCount"></param>
    void BeginWriteAttribute(string name, string prefix, int prefixOffset, string suffix, int suffixOffset, int attributeValuesCount);

    /// <summary>
    /// 开始写入特性
    /// </summary>
    /// <param name="name"></param>
    /// <param name="prefix"></param>
    /// <param name="prefixOffset"></param>
    /// <param name="suffix"></param>
    /// <param name="suffixOffset"></param>
    /// <param name="attributeValuesCount"></param>
    /// <returns></returns>
    Task BeginWriteAttributeAsync(string name, string prefix, int prefixOffset, string suffix, int suffixOffset, int attributeValuesCount);

    /// <summary>
    /// 写入特性值
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="prefixOffset"></param>
    /// <param name="value"></param>
    /// <param name="valueOffset"></param>
    /// <param name="valueLength"></param>
    /// <param name="isLiteral"></param>
    void WriteAttributeValue(string prefix, int prefixOffset, object value, int valueOffset, int valueLength, bool isLiteral);

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
    Task WriteAttributeValueAsync(string prefix, int prefixOffset, object value, int valueOffset, int valueLength, bool isLiteral);

    /// <summary>
    /// 结束写入特性
    /// </summary>
    void EndWriteAttribute();

    /// <summary>
    /// 结束写入特性
    /// </summary>
    /// <returns></returns>
    Task EndWriteAttributeAsync();

    /// <summary>
    /// 执行
    /// </summary>
    void Execute();

    /// <summary>
    /// 执行
    /// </summary>
    /// <returns></returns>
    Task ExecuteAsync();

    /// <summary>
    /// 获取结果
    /// </summary>
    /// <returns></returns>
    string Result();

    /// <summary>
    /// 获取结果
    /// </summary>
    /// <returns></returns>
    Task<string> ResultAsync();
}