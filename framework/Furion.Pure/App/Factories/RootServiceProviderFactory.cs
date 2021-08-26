// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2. 
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE 
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.  
// See the Mulan PSL v2 for more details.

using Furion.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Furion
{
    /// <summary>
    /// 根服务工厂
    /// </summary>
    [SuppressSniffer]
    public class RootServiceProviderFactory : IServiceProviderFactory<IServiceProvider>
    {
        /// <summary>
        /// 构建根服务提供器
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider CreateBuilder(IServiceCollection services)
        {
            return InternalApp.RootServices = services.BuildServiceProvider(true);
        }

        /// <summary>
        /// 创建根服务提供器
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <returns></returns>
        public IServiceProvider CreateServiceProvider(IServiceProvider containerBuilder)
        {
            return containerBuilder;
        }
    }
}