using System.ComponentModel.DataAnnotations;

namespace FurionApi.Application;

public class LoginInput
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public bool RememberMe { get; set; }
}