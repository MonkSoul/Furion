using Furion.Core;

namespace Furion.Application.Persons;

public class ChildrenDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public Gender Gender { get; set; }
}
