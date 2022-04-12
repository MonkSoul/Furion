using System.ComponentModel.DataAnnotations;

namespace Furion.Application;

/// <summary>
/// 登录输出参数
/// </summary>
public class LoginOutput
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [Required, MinLength(3)]
    public string Account { get; set; }

    /// <summary>
    /// Token
    /// </summary>
    public string AccessToken { get; set; }
}
