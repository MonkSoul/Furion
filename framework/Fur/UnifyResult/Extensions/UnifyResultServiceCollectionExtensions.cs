using Fur.DependencyInjection;
using Fur.UnifyResult;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 规范化结果服务拓展
    /// </summary>
    [SkipScan]
    public static class UnifyResultServiceCollectionExtensions
    {
        /// <summary>
        /// 添加规范化结果服务
        /// </summary>
        /// <param name="mvcBuilder"></param>
        /// <param name="restfulResultType">RESTful结果类型，泛型类型</param>
        /// <returns></returns>
        public static IMvcBuilder AddUnifyResult(this IMvcBuilder mvcBuilder, Type restfulResultType = default)
        {
            mvcBuilder.AddUnifyResult<RESTfulResultProvider>(restfulResultType);

            return mvcBuilder;
        }

        /// <summary>
        /// 添加规范化结果服务
        /// </summary>
        /// <typeparam name="TUnifyResultProvider"></typeparam>
        /// <param name="mvcBuilder"></param>
        /// <param name="restfulResultType">RESTful结果类型，泛型类型</param>
        /// <returns></returns>
        public static IMvcBuilder AddUnifyResult<TUnifyResultProvider>(this IMvcBuilder mvcBuilder, Type restfulResultType = default)
            where TUnifyResultProvider : class, IUnifyResultProvider
        {
            if (restfulResultType != null) UnifyResultContext.RESTfulResultType = restfulResultType;

            // 添加规范化提供器
            mvcBuilder.Services.AddSingleton<IUnifyResultProvider, TUnifyResultProvider>();

            // 添加成功规范化结果
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.Add<SuccessUnifyResultFilter>();
            });

            return mvcBuilder;
        }
    }
}