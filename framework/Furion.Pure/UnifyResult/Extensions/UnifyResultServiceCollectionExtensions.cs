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

using Furion.DependencyInjection;
using Furion.UnifyResult;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 规范化结果服务拓展
    /// </summary>
    [SuppressSniffer]
    public static class UnifyResultServiceCollectionExtensions
    {
        /// <summary>
        /// 添加规范化结果服务
        /// </summary>
        /// <param name="mvcBuilder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddUnifyResult(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddUnifyResult<RESTfulResultProvider>();

            return mvcBuilder;
        }

        /// <summary>
        /// 添加规范化结果服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUnifyResult(this IServiceCollection services)
        {
            services.AddUnifyResult<RESTfulResultProvider>();

            return services;
        }

        /// <summary>
        /// 添加规范化结果服务
        /// </summary>
        /// <typeparam name="TUnifyResultProvider"></typeparam>
        /// <param name="mvcBuilder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddUnifyResult<TUnifyResultProvider>(this IMvcBuilder mvcBuilder)
            where TUnifyResultProvider : class, IUnifyResultProvider
        {
            // 是否启用规范化结果
            UnifyContext.IsEnabledUnifyHandle = true;

            // 获取规范化提供器模型
            UnifyContext.RESTfulResultType = typeof(TUnifyResultProvider).GetCustomAttribute<UnifyModelAttribute>().ModelType;

            // 添加规范化提供器
            mvcBuilder.Services.AddSingleton<IUnifyResultProvider, TUnifyResultProvider>();

            // 添加成功规范化结果筛选器
            mvcBuilder.AddMvcFilter<SucceededUnifyResultFilter>();

            return mvcBuilder;
        }

        /// <summary>
        /// 添加规范化结果服务
        /// </summary>
        /// <typeparam name="TUnifyResultProvider"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUnifyResult<TUnifyResultProvider>(this IServiceCollection services)
            where TUnifyResultProvider : class, IUnifyResultProvider
        {
            // 是否启用规范化结果
            UnifyContext.IsEnabledUnifyHandle = true;

            // 获取规范化提供器模型
            UnifyContext.RESTfulResultType = typeof(TUnifyResultProvider).GetCustomAttribute<UnifyModelAttribute>().ModelType;

            // 添加规范化提供器
            services.AddSingleton<IUnifyResultProvider, TUnifyResultProvider>();

            // 添加成功规范化结果筛选器
            services.AddMvcFilter<SucceededUnifyResultFilter>();

            return services;
        }
    }
}