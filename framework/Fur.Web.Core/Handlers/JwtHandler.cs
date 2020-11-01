using Fur.Authorization;
using Fur.Core;
using Fur.DataEncryption;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Fur.Web.Core
{
    /// <summary>
    /// JWT 授权自定义处理程序
    /// </summary>
    public class JwtHandler : AppAuthorizeHandler
    {
        /// <summary>
        /// 请求管道
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override bool Pipeline(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
        {
            // 获取 token
            var accessToken = httpContext.GetJWTToken();
            if (string.IsNullOrEmpty(accessToken)) return false;

            // 验证token
            var (IsValid, _) = JWTEncryption.Validate(accessToken, App.GetOptions<JWTSettingsOptions>());
            if (!IsValid) return false;

            // 检查权限
            return CheckAuthorzie(httpContext);
        }

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private static bool CheckAuthorzie(DefaultHttpContext httpContext)
        {
            // 获取权限特性
            var securityDefineAttribute = httpContext.GetMetadata<SecurityDefineAttribute>();
            if (securityDefineAttribute == null) return true;

            return App.GetService<IAuthorizationManager>().CheckSecurity(securityDefineAttribute.ResourceId);
        }
    }
}