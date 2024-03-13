// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Furion.Authorization;

/// <summary>
/// Jwt 配置
/// </summary>
public sealed class JWTSettingsOptions
{
    /// <summary>
    /// 验证签发方密钥
    /// </summary>
    public bool? ValidateIssuerSigningKey { get; set; }

    /// <summary>
    /// 签发方密钥
    /// </summary>
    public string IssuerSigningKey { get; set; }

    /// <summary>
    /// 验证签发方
    /// </summary>
    public bool? ValidateIssuer { get; set; }

    /// <summary>
    /// 签发方
    /// </summary>
    public string ValidIssuer { get; set; }

    /// <summary>
    /// 验证签收方
    /// </summary>
    public bool? ValidateAudience { get; set; }

    /// <summary>
    /// 签收方
    /// </summary>
    public string ValidAudience { get; set; }

    /// <summary>
    /// 验证生存期
    /// </summary>
    public bool? ValidateLifetime { get; set; }

    /// <summary>
    /// 过期时间容错值，解决服务器端时间不同步问题（秒）
    /// </summary>
    public long? ClockSkew { get; set; }

    /// <summary>
    /// 过期时间（分钟）
    /// </summary>
    public long? ExpiredTime { get; set; }

    /// <summary>
    /// 加密算法
    /// </summary>
    public string Algorithm { get; set; }

    /// <summary>
    /// 验证过期时间，设置 false 永不过期
    /// </summary>
    public bool RequireExpirationTime { get; set; } = true;
}