// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd and Contributors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Furion.VirtualFileServer;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 虚拟文件服务服务拓展
/// </summary>
[SuppressSniffer]
public static class VirtualFileServerServiceCollectionExtensions
{
    /// <summary>
    /// 文件提供器系统服务拓展
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddVirtualFileServer(this IServiceCollection services)
    {
        // 解析文件提供器
        services.AddSingleton(provider =>
        {
            static IFileProvider fileProviderResolve(FileProviderTypes fileProviderTypes, object args)
            {
                // 根据类型创建对应 提供器
                IFileProvider fileProvider = fileProviderTypes switch
                {
                    FileProviderTypes.Embedded => new EmbeddedFileProvider(args as Assembly),
                    FileProviderTypes.Physical => new PhysicalFileProvider(args as string),
                    _ => throw new NotSupportedException()
                };

                return fileProvider;
            }

            // 转换成委托
            return (Func<FileProviderTypes, object, IFileProvider>)fileProviderResolve;
        });

        return services;
    }
}