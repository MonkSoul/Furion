// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.11.1
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using System;
using System.IO;
using System.Threading.Tasks;

namespace Furion.ViewEngine
{
    /// <summary>
    /// 视图引擎模板（编译后）
    /// </summary>
    public interface IViewEngineTemplate
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
    public interface IViewEngineTemplate<out T>
        where T : IViewEngineModel
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