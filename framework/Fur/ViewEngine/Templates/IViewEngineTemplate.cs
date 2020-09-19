// -----------------------------------------------------------------------------
// Fur 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。
// Copyright © 2020 Fur, Baiqian Co.,Ltd.
//
// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 源码地址：https://gitee.com/monksoul/Fur
// 开源协议：Apache-2.0（http://www.apache.org/licenses/LICENSE-2.0）
// -----------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Fur.ViewEngine
{
    /// <summary>
    /// 视图模板接口
    /// </summary>
    public interface IViewEngineTemplate
    {
        dynamic Model { get; set; }

        void WriteLiteral(string literal = null);

        void Write(object obj = null);

        void BeginWriteAttribute(string name, string prefix, int prefixOffset, string suffix, int suffixOffset, int attributeValuesCount);

        void WriteAttributeValue(string prefix, int prefixOffset, object value, int valueOffset, int valueLength, bool isLiteral);

        void EndWriteAttribute();

        Task ExecuteAsync();

        string Result();
    }
}