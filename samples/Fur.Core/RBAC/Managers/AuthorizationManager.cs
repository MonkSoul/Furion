using Fur.Authorization;
using Fur.DatabaseAccessor;
using Fur.DataEncryption;
using Fur.DependencyInjection;
using Fur.FriendlyException;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Linq;

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

        private readonly IRepository<User> _userRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="options"></param>
        /// <param name="userRepository"></param>
        public AuthorizationManager(IHttpContextAccessor httpContextAccessor
            , IOptions<JWTSettingsOptions> options
            , IRepository<User> userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtSettings = options.Value;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <returns></returns>
        public object GetUserId()
        {
            return GetUserId<object>();
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
        /// 检查权限
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns></returns>
        public bool CheckSecurity(string resourceId)
        {
            var userId = GetUserId<int>();

            // ========= 以下代码应该缓存起来 ===========
            // 查询用户拥有的权限
            var securities = _userRepository
                .Include(u => u.Roles, false)
                    .ThenInclude(u => u.Securities)
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Roles
                    .SelectMany(u => u.Securities))
                .Select(u => u.UniqueName);

            if (!securities.Contains(resourceId)) return false;

            return true;
        }

        /// <summary>
        /// 解析 Token
        /// </summary>
        /// <returns></returns>
        [IfException(1001, ErrorMessage = "非法操作")]
        private JsonWebToken ReadToken()
        {
            // 获取 token
            var accessToken = _httpContextAccessor.GetJwtToken() ?? throw Oops.Oh(1001);

            // 验证token
            var (IsValid, Token) = JWTEncryption.Validate(accessToken, _jwtSettings);
            if (!IsValid) throw Oops.Oh(1001);

            return Token;
        }
    }
}