using Furion.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Furion.DataEncryption
{
    /// <summary>
    /// JWT 加解密
    /// </summary>
    public class JWTEncryption
    {
        /// <summary>
        /// 生成 Token
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="expiredTime"></param>
        /// <returns></returns>
        public static string Encrypt(Dictionary<string, object> payload, long? expiredTime = null)
        {
            var (Payload, JWTSettings) = CombinePayload(payload, expiredTime);
            return Encrypt(JWTSettings.IssuerSigningKey, Payload);
        }

        /// <summary>
        /// 生成 Token
        /// </summary>
        /// <param name="issuerSigningKey"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static string Encrypt(string issuerSigningKey, Dictionary<string, object> payload)
        {
            return Encrypt(issuerSigningKey, JsonSerializer.Serialize(payload));
        }

        /// <summary>
        /// 生成 Token
        /// </summary>
        /// <param name="issuerSigningKey"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static string Encrypt(string issuerSigningKey, string payload)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JsonWebTokenHandler();
            return tokenHandler.CreateToken(payload, credentials);
        }

        /// <summary>
        /// 生成刷新 Token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static string GenerateRefreshToken(string accessToken, int days = 30)
        {
            // 分割Token
            var tokenParagraphs = accessToken.Split('.', StringSplitOptions.RemoveEmptyEntries);

            var rd = new Random();
            var s = rd.Next(10, tokenParagraphs[1].Length / 2 + 2);
            var l = rd.Next(3, 13);

            var payload = new Dictionary<string, object>
            {
                { "f",tokenParagraphs[0] },
                { "e",tokenParagraphs[2] },
                { "s",s },
                { "l",l },
                { "k",tokenParagraphs[1].Substring(s,l) }
            };

            return Encrypt(payload, days * 24 * 60);
        }

        /// <summary>
        /// 通过过期Token 和 刷新Token 换取新的 Token
        /// </summary>
        /// <param name="expiredToken"></param>
        /// <param name="refreshToken"></param>
        /// <param name="expiredTime"></param>
        /// <returns></returns>
        public static string Exchange(string expiredToken, string refreshToken, long? expiredTime = null)
        {
            // 判断刷新Token 是否过期
            var (isValid, token) = Validate(refreshToken);
            if (!isValid) return default;

            // 分割过期Token
            var tokenParagraphs = expiredToken.Split('.', StringSplitOptions.RemoveEmptyEntries);

            // 判断各个部分是否匹配
            if (!token.GetPayloadValue<string>("f").Equals(tokenParagraphs[0])) return default;
            if (!token.GetPayloadValue<string>("e").Equals(tokenParagraphs[2])) return default;
            if (!tokenParagraphs[1].Substring(token.GetPayloadValue<int>("s"), token.GetPayloadValue<int>("l")).Equals(token.GetPayloadValue<string>("k"))) return default;

            var oldToken = ReadJwtToken(expiredToken);
            var payload = oldToken.Claims.Where(u => !StationaryClaimTypes.Contains(u.Type))
                                         .ToDictionary(u => u.Type, u => (object)u.Value);

            return Encrypt(payload, expiredTime);
        }

        /// <summary>
        /// 自动刷新 Token 信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContext"></param>
        /// <param name="expiredTime"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static bool AutoRefreshToken(AuthorizationHandlerContext context, DefaultHttpContext httpContext, long? expiredTime = null, int days = 30)
        {
            // 如果验证有效，则跳过刷新
            if (context.User.Identity.IsAuthenticated) return true;

            // 判断是否含有匿名特性
            if (httpContext.GetEndpoint()?.Metadata?.GetMetadata<AllowAnonymousAttribute>() != null) return true;

            // 获取过期Token 和 刷新Token
            var expiredToken = GetJwtBearerToken(httpContext);
            var refreshToken = GetJwtBearerToken(httpContext, "X-Authorization");
            if (string.IsNullOrEmpty(expiredToken) || string.IsNullOrEmpty(refreshToken)) return false;

            // 交换新的 Token
            var accessToken = Exchange(expiredToken, refreshToken, expiredTime);

            // 读取新的 Token Clamis
            var claims = ReadJwtToken(accessToken)?.Claims;
            if (claims == null) return false;

            // 创建身份信息
            var claimIdentity = new ClaimsIdentity("AuthenticationTypes.Federation");
            claimIdentity.AddClaims(claims);
            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);

            // 设置 HttpContext.User 并登录
            httpContext.User = claimsPrincipal;
            httpContext.SignInAsync(claimsPrincipal);

            // 返回新的 Token
            httpContext.Response.Headers["access-token"] = accessToken;
            // 返回新的 刷新Token
            httpContext.Response.Headers["x-access-token"] = GenerateRefreshToken(accessToken, days);

            return true;
        }

        /// <summary>
        /// 验证 Token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static (bool IsValid, JsonWebToken Token) Validate(string accessToken)
        {
            var jwtSettings = GetJWTSettings();

            // 加密Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 创建Token验证参数
            var tokenValidationParameters = CreateTokenValidationParameters(jwtSettings);
            if (tokenValidationParameters.IssuerSigningKey == null) tokenValidationParameters.IssuerSigningKey = creds.Key;

            // 验证 Token
            var tokenHandler = new JsonWebTokenHandler();
            try
            {
                var tokenValidationResult = tokenHandler.ValidateToken(accessToken, tokenValidationParameters);
                if (!tokenValidationResult.IsValid) return (false, null);

                var jsonWebToken = tokenValidationResult.SecurityToken as JsonWebToken;
                return (true, jsonWebToken);
            }
            catch
            {
                return (false, default);
            }
        }

        /// <summary>
        /// 验证 Token
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="token"></param>
        /// <param name="headerKey"></param>
        /// <returns></returns>
        public static bool ValidateJwtBearerToken(DefaultHttpContext httpContext, out JsonWebToken token, string headerKey = "Authorization")
        {
            // 获取 token
            var accessToken = GetJwtBearerToken(httpContext, headerKey);
            if (string.IsNullOrEmpty(accessToken))
            {
                token = null;
                return false;
            }

            // 验证token
            var (IsValid, Token) = Validate(accessToken);
            token = IsValid ? Token : null;

            return IsValid;
        }

        /// <summary>
        /// 读取 Token，不含验证
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static JsonWebToken ReadJwtToken(string accessToken)
        {
            var tokenHandler = new JsonWebTokenHandler();
            if (tokenHandler.CanReadToken(accessToken))
            {
                return tokenHandler.ReadJsonWebToken(accessToken);
            }

            return default;
        }

        /// <summary>
        /// 获取 JWT Bearer Token
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="headerKey"></param>
        /// <returns></returns>
        public static string GetJwtBearerToken(DefaultHttpContext httpContext, string headerKey = "Authorization")
        {
            // 判断请求报文头中是否有 "Authorization" 报文头
            var bearerToken = httpContext.Request.Headers[headerKey].ToString();
            if (string.IsNullOrEmpty(bearerToken)) return default;

            return bearerToken[7..];
        }

        /// <summary>
        /// 获取 JWT 配置
        /// </summary>
        /// <returns></returns>
        public static JWTSettingsOptions GetJWTSettings()
        {
            return InternalHttpContext.Current()?.RequestServices?.GetService<IOptions<JWTSettingsOptions>>()?.Value;
        }

        /// <summary>
        /// 生成Token验证参数
        /// </summary>
        /// <param name="jwtSettings"></param>
        /// <returns></returns>
        public static TokenValidationParameters CreateTokenValidationParameters(JWTSettingsOptions jwtSettings)
        {
            return new TokenValidationParameters
            {
                // 验证签发方密钥
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey.Value,
                // 签发方密钥
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey)),
                // 验证签发方
                ValidateIssuer = jwtSettings.ValidateIssuer.Value,
                // 设置签发方
                ValidIssuer = jwtSettings.ValidIssuer,
                // 验证签收方
                ValidateAudience = jwtSettings.ValidateAudience.Value,
                // 设置接收方
                ValidAudience = jwtSettings.ValidAudience,
                // 验证生存期
                ValidateLifetime = jwtSettings.ValidateLifetime.Value,
                // 过期时间容错值
                ClockSkew = TimeSpan.FromSeconds(jwtSettings.ClockSkew.Value),
            };
        }

        /// <summary>
        /// 组合 Claims 负荷
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="expiredTime">过期时间，单位：分钟</param>
        /// <returns></returns>
        private static (Dictionary<string, object> Payload, JWTSettingsOptions JWTSettings) CombinePayload(Dictionary<string, object> payload, long? expiredTime = null)
        {
            var jwtSettings = GetJWTSettings();
            var datetimeOffset = DateTimeOffset.Now;

            if (!payload.ContainsKey(JwtRegisteredClaimNames.Iat))
            {
                payload.Add(JwtRegisteredClaimNames.Iat, datetimeOffset.ToUnixTimeSeconds());
            }

            if (!payload.ContainsKey(JwtRegisteredClaimNames.Nbf))
            {
                payload.Add(JwtRegisteredClaimNames.Nbf, datetimeOffset.ToUnixTimeSeconds());
            }

            if (!payload.ContainsKey(JwtRegisteredClaimNames.Exp))
            {
                var minute = expiredTime ?? jwtSettings.ExpiredTime ?? 20;
                payload.Add(JwtRegisteredClaimNames.Exp, DateTimeOffset.Now.AddSeconds(minute * 60).ToUnixTimeSeconds());
            }

            if (!payload.ContainsKey(JwtRegisteredClaimNames.Iss))
            {
                payload.Add(JwtRegisteredClaimNames.Iss, jwtSettings.ValidIssuer);
            }

            if (!payload.ContainsKey(JwtRegisteredClaimNames.Aud))
            {
                payload.Add(JwtRegisteredClaimNames.Aud, jwtSettings.ValidAudience);
            }

            return (payload, jwtSettings);
        }

        /// <summary>
        /// 固定的 Claim 类型
        /// </summary>
        private static readonly string[] StationaryClaimTypes = new[] { JwtRegisteredClaimNames.Iat, JwtRegisteredClaimNames.Nbf, JwtRegisteredClaimNames.Exp, JwtRegisteredClaimNames.Iss, JwtRegisteredClaimNames.Aud };
    }
}