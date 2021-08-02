// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             http://license.coscl.org.cn/MulanPSL2
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.

using Furion.FriendlyException;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Furion
{
    /// <summary>
    /// 供非 Web 初始化
    /// </summary>
    public static class Inject
    {
        /// <summary>
        /// 创建初始服务集合
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection Create()
        {
            // 监听全局异常
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

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

        /// <summary>
        /// 监听全局异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception)e.ExceptionObject;
            if (exception is AppFriendlyException) return;

            // 获取调用堆栈信息
            var stackTrace = EnhancedStackTrace.Current();

            // 获取堆栈第一个全局 [IfException] 特性
            var ifExceptionAttribute = stackTrace
                .Where(u => u.MethodInfo.MethodBase != null && u.MethodInfo.MethodBase.IsDefined(typeof(IfExceptionAttribute), true))
                .SelectMany(u => u.MethodInfo.MethodBase.GetCustomAttributes<IfExceptionAttribute>(true))
                .FirstOrDefault(u => u.ErrorCode == null && !string.IsNullOrWhiteSpace(u.ErrorMessage));

            // 打印全局消息
            if (ifExceptionAttribute != null) Console.WriteLine(ifExceptionAttribute.ErrorMessage);
        }
    }
}