namespace Furion.Application.Persons;

public class PersonDto
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 年龄
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// 住址
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 电话号码
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// QQ 号码
    /// </summary>
    public string QQ { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTimeOffset CreatedTime { get; set; }

    /// <summary>
    /// 孩子
    /// </summary>
    public List<ChildrenDto> Childrens { get; set; }

    /// <summary>
    /// 孩子
    /// </summary>
    public List<PostDto> Posts { get; set; }
}