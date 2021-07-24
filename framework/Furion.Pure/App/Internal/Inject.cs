// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2. 
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2 
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.  
// See the Mulan PSL v2 for more details.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Furion
{
    /// <summary>
    /// 供控制台初始化类
    /// </summary>
    public static class Inject
    {
        /// <summary>
        /// 创建一个服务集合
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection Create()
        {
            // 创建配置构建器
            var configurationBuilder = new ConfigurationBuilder();

            // 加载配置
            InternalApp.AddJsonFiles(configurationBuilder, default);

            // 存储配置对象
            InternalApp.Configuration = configurationBuilder.Build();

            // 创建服务对象
            var services = new ServiceCollection();

            // 添加全局配置和存储服务提供器
            InternalApp.InternalServices = services;

            // 初始化应用服务
            services.AddApp();

            return services;
        }
    }
}
