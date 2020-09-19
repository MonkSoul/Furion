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

using Fur.DependencyInjection;
using System.Text;
using System.Threading.Tasks;

namespace Fur.ViewEngine
{
    /// <summary>
    /// 视图模板实现类
    /// </summary>
    [NonBeScan]
    public abstract class ViewEngineTemplate : IViewEngineTemplate
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        private string attributeSuffix;

        public dynamic Model { get; set; }

        public virtual void WriteLiteral(string literal = null)
        {
            stringBuilder.Append(literal);
        }

        public virtual void Write(object obj = null)
        {
            stringBuilder.Append(obj);
        }

        public virtual void BeginWriteAttribute(string name, string prefix, int prefixOffset, string suffix, int suffixOffset, int attributeValuesCount)
        {
            attributeSuffix = suffix;
            stringBuilder.Append(prefix);
        }

        public virtual void WriteAttributeValue(string prefix, int prefixOffset, object value, int valueOffset, int valueLength, bool isLiteral)
        {
            stringBuilder.Append(prefix);
            stringBuilder.Append(value);
        }

        public virtual void EndWriteAttribute()
        {
            stringBuilder.Append(attributeSuffix);
            attributeSuffix = null;
        }

        public virtual Task ExecuteAsync()
        {
            return Task.CompletedTask;
        }

        public virtual string Result()
        {
            return stringBuilder.ToString();
        }
    }

    /// <summary>
    /// 视图模板实现类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [NonBeScan]
    public abstract class ViewEngineTemplateBase<T> : ViewEngineTemplate
    {
        public new T Model { get; set; }
    }
}