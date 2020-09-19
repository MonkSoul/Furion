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

using System;
using System.IO;
using System.Threading.Tasks;

namespace Fur.ViewEngine
{
    /// <summary>
    /// 视图编译模板接口
    /// </summary>
    public interface IViewEngineCompiledTemplate
    {
        void SaveToStream(Stream stream);

        Task SaveToStreamAsync(Stream stream);

        void SaveToFile(string fileName);

        Task SaveToFileAsync(string fileName);

        string Run(object model = null);

        Task<string> RunAsync(object model = null);
    }

    /// <summary>
    /// 泛型视图编译模板接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IViewEngineCompiledTemplate<out T>
        where T : IViewEngineTemplate
    {
        void SaveToStream(Stream stream);

        Task SaveToStreamAsync(Stream stream);

        void SaveToFile(string fileName);

        Task SaveToFileAsync(string fileName);

        string Run(Action<T> initializer);

        Task<string> RunAsync(Action<T> initializer);
    }
}