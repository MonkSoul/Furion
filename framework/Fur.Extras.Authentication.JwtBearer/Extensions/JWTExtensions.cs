using Fur.Authorization;
using Fur.DataEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Microsoft.AspNetCore.Authorization
{
    /// <summary>
    /// Jwt 授权拓展
    /// </summary>
    public static class JWTExtensions
    {
        /// <summary>
        /// 验证 Jwt 授权
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContext"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool ValidateJwtBearer(this AuthorizationHandlerContext context, HttpContext httpContext, out JsonWebToken token)
        {
            // 获取 token
            var accessToken = httpContext.GetJwtToken();
            if (string.IsNullOrEmpty(accessToken))
            {
                token = null;
                return false;
            }

            // 验证token
            var (IsValid, Token) = JWTEncryption.Validate(accessToken, httpContext.RequestServices.GetService<IOptions<JWTSettingsOptions>>().Value);
            token = IsValid ? Token : null;

            return IsValid;
        }
    }
}