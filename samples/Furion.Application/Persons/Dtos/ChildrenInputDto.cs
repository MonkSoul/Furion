using System.Text.Json.Serialization;

namespace Furion.Application.Persons;

public class ChildrenInputDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    [Required, JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender Gender { get; set; }
}