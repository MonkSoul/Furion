using Fur.DependencyInjection;
using Fur.UnifyResult;
using Microsoft.AspNetCore.Mvc;

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
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TUnifyResultProvider"></typeparam>
        /// <param name="mvcBuilder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddUnifyResult<TResult, TUnifyResultProvider>(this IMvcBuilder mvcBuilder)
            where TResult : class, new()
            where TUnifyResultProvider : class, IUnifyResultProvider
        {
            // 添加规范化提供器
            mvcBuilder.Services.AddSingleton<IUnifyResultProvider, TUnifyResultProvider>();

            // 添加成功规范化结果
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.Add<SuccessUnifyResultFilter>();
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(TResult), 200));
            });

            return mvcBuilder;
        }
    }
}