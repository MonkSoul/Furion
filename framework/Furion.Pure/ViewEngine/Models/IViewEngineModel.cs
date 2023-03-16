// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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