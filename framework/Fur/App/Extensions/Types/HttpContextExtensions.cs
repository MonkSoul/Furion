using Fur.DependencyInjection;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// Http 拓展类
    /// </summary>
    [SkipScan]
    public static class HttpContextExtensions
    {
        /// <summary>
        /// 获取 JWT Token
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetJWTToken(this HttpContext httpContext)
        {
            // 判断请求报文头中是否有 "Authorization" 报文头
            var bearerToken = httpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(bearerToken)) return default;

            return bearerToken[7..];
        }

        /// <summary>
        /// 获取 JWT Token
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        public static string GetJWTToken(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext.GetJWTToken();
        }

        /// <summary>
        /// 获取 Action 特性
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static TAttribute GetMetadata<TAttribute>(this HttpContext httpContext)
            where TAttribute : class
        {
            return httpContext.GetEndpoint().Metadata.GetMetadata<TAttribute>();
        }

        /// <summary>
        /// 设置规范化文档自动登录
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="accessToken"></param>
        public static void SigninToSwagger(this HttpContext httpContext, string accessToken)
        {
            // 设置 Swagger 刷新自动授权
            httpContext.Response.Headers["access-token"] = accessToken;
        }

        /// <summary>
        /// 设置规范化文档自动登录
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="accessToken"></param>
        public static void SigninToSwagger(this IHttpContextAccessor httpContextAccessor, string accessToken)
        {
            httpContextAccessor.HttpContext.SigninToSwagger(accessToken);
        }
    }
}