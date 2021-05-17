// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.6.0
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Furion.ViewEngine
{
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