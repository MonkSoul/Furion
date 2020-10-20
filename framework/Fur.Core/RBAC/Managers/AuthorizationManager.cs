using Fur.Authorization;
using Fur.DependencyInjection;
using Fur.FriendlyException;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Fur.Core
{
    /// <summary>
    /// 权限管理器
    /// </summary>
    public class AuthorizationManager : IAuthorizationManager, IScoped
    {
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
        public AuthorizationManager(IHttpContextAccessor httpContextAccessor
            , IOptions<JWTSettingsOptions> options)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtSettings = options.Value;
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <returns></returns>
        public object GetUserId()
        {
            return ReadToken().GetPayloadValue<object>("UserId");
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUserId<T>()
        {
            return ReadToken().GetPayloadValue<T>("UserId");
        }

        /// <summary>
        /// 解析 Token
        /// </summary>
        /// <returns></returns>
        [IfException(1001, ErrorMessage = "非法操作")]
        private JsonWebToken ReadToken()
        {
            // 判断请求报文头中是否有 "Authorization" 报文头
            var bearerToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(bearerToken)) throw Oops.Oh(1001);

            // 获取 token
            var accessToken = bearerToken[7..];

            // 验证token
            var (IsValid, Token) = JWTEncryption.Validate(accessToken, _jwtSettings);
            if (!IsValid) throw Oops.Oh(1001);

            return Token;
        }
    }
}