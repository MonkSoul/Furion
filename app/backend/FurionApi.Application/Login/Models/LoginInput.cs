using System.ComponentModel.DataAnnotations;

namespace FurionApi.Application;

public class LoginInput
{
    /// <example>furion</example>
    [Required]
    public string UserName { get; set; }

    /// <example>furion</example>
    [Required, MinLength(6)]
    public string Password { get; set; }

    /// <example>false</example>
    public bool RememberMe { get; set; }
}