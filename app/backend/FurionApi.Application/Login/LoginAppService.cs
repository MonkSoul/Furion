using Furion.DataEncryption;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurionApi.Application;

/// <summary>
/// 登录服务
/// </summary>
[AllowAnonymous]
public class LoginAppService : IDynamicApiController
{
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="_httpContextAccessor"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PublicUser> Post([FromServices] IHttpContextAccessor _httpContextAccessor
        , LoginInput input)
    {
        if (!(input.UserName.Trim() == "furion" && input.Password.Trim() == "furion"))
            throw Oops.Bah(ErrorCodes.T1000);

        var publicUser = new PublicUser
        {
            Id = 1,
            UserName = input.UserName
        };

        var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
        {
            { nameof(PublicUser.Id), publicUser.Id },
            { nameof(PublicUser.UserName),publicUser.UserName }
        });

        var refreshToken = JWTEncryption.GenerateRefreshToken(accessToken, 43200);
        _httpContextAccessor.HttpContext.SetTokensOfResponseHeaders(accessToken, refreshToken);

        return await Task.FromResult(publicUser);
    }
}