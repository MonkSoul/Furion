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

using System;
using System.IO;
using System.Threading.Tasks;

// reference：https://github.com/adoconnection/RazorEngineCore
namespace Fur.ViewEngine
{
    /// <summary>
    /// 视图编译模板接口
    /// </summary>
    public interface IViewEngineCompiledTemplate
    {
        /// <summary>
        /// 保存到流中
        /// </summary>
        /// <param name="stream"></param>
        void SaveToStream(Stream stream);

        /// <summary>
        /// 保存到流中
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task SaveToStreamAsync(Stream stream);

        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="fileName"></param>
        void SaveToFile(string fileName);

        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task SaveToFileAsync(string fileName);

        /// <summary>
        /// 执行编译
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string Run(object model = null);

        /// <summary>
        /// 执行编译
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> RunAsync(object model = null);
    }

    /// <summary>
    /// 泛型视图编译模板接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IViewEngineCompiledTemplate<out T>
        where T : IViewEngineTemplate
    {
        /// <summary>
        /// 保存到流中
        /// </summary>
        /// <param name="stream"></param>
        void SaveToStream(Stream stream);

        /// <summary>
        /// 保存到流中
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task SaveToStreamAsync(Stream stream);

        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="fileName"></param>
        void SaveToFile(string fileName);

        /// <summary>
        /// 保存到文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task SaveToFileAsync(string fileName);

        /// <summary>
        /// 执行编译
        /// </summary>
        /// <param name="initializer"></param>
        /// <returns></returns>
        string Run(Action<T> initializer);

        /// <summary>
        /// 执行编译
        /// </summary>
        /// <param name="initializer"></param>
        /// <returns></returns>
        Task<string> RunAsync(Action<T> initializer);
    }
}