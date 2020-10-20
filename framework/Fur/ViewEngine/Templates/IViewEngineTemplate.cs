// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下企业应用开发最佳实践框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0-rc.final.17
// 官方网站：https://chinadot.net
// 源码地址：Gitee：https://gitee.com/monksoul/Fur
// 				    Github：https://github.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using System.Threading.Tasks;

// reference：https://github.com/adoconnection/RazorEngineCore
namespace Fur.ViewEngine
{
    /// <summary>
    /// 视图模板接口
    /// </summary>
    public interface IViewEngineTemplate
    {
        /// <summary>
        /// 模型
        /// </summary>
        dynamic Model { get; set; }

        /// <summary>
        /// 插入字面量
        /// </summary>
        /// <param name="literal"></param>
        void WriteLiteral(string literal = null);

        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="obj"></param>
        void Write(object obj = null);

        /// <summary>
        /// 插入属性
        /// </summary>
        /// <param name="name"></param>
        /// <param name="prefix"></param>
        /// <param name="prefixOffset"></param>
        /// <param name="suffix"></param>
        /// <param name="suffixOffset"></param>
        /// <param name="attributeValuesCount"></param>
        void BeginWriteAttribute(string name, string prefix, int prefixOffset, string suffix, int suffixOffset, int attributeValuesCount);

        /// <summary>
        /// 插入属性值
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="prefixOffset"></param>
        /// <param name="value"></param>
        /// <param name="valueOffset"></param>
        /// <param name="valueLength"></param>
        /// <param name="isLiteral"></param>
        void WriteAttributeValue(string prefix, int prefixOffset, object value, int valueOffset, int valueLength, bool isLiteral);

        /// <summary>
        /// 结束插入属性
        /// </summary>
        void EndWriteAttribute();

        /// <summary>
        /// 执行
        /// </summary>
        /// <returns></returns>
        Task ExecuteAsync();

        /// <summary>
        /// 返回结果
        /// </summary>
        /// <returns></returns>
        string Result();
    }
}