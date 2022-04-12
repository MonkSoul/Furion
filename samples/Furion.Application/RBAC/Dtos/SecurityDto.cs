namespace Furion.Application;

public class SecurityDto
{
    /// <summary>
    /// 权限Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 权限唯一名（每一个接口）
    /// </summary>
    public string UniqueName { get; set; }
}
