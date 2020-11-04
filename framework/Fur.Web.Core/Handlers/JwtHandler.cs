using Fur.Authorization;
using Fur.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

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
            var isValid = context.ValidateJwtBearer(httpContext, out JsonWebToken token);
            if (!isValid) return false;

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