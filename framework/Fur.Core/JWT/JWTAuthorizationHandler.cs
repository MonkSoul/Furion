using Fur.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;

namespace Fur.Core
{
    /// <summary>
    /// JWT 授权自定义处理程序
    /// </summary>
    /// <remarks>
    /// 可以在这里自定义自己的权限
    /// </remarks>
    public class JWTAuthorizationHandler : AppAuthorizeHandler
    {
        /// <summary>
        /// JWT 配置
        /// </summary>
        private readonly JWTSettingsOptions _jwtSettings;

        /// <summary>
        /// 请求上下文访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="options"></param>
        public JWTAuthorizationHandler(IHttpContextAccessor httpContextAccessor
            , IOptions<JWTSettingsOptions> options)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtSettings = options.Value;
        }

        public override bool Pipeline(AuthorizationHandlerContext context, ControllerActionDescriptor actionDescriptor)
        {
            // 判断请求报文头中是否有 "Authorization" 报文头
            var bearerToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(bearerToken)) return false;

            // 获取 token
            var accessToken = bearerToken[7..];

            // 验证token
            var (IsValid, Token) = JWTEncryption.Validate(accessToken, _jwtSettings);
            if (!IsValid) return false;

            return true;
        }
    }
}