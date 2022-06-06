namespace Furion.Application.Persons;

public class PersonInputDto
{
    /// <summary>
    /// Id
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// 年龄
    /// </summary>
    [Required, Range(10, 110)]
    public int Age { get; set; }

    /// <summary>
    /// 住址
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 电话号码
    /// </summary>
    [DataValidation(ValidationTypes.PhoneNumber)]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// QQ 号码
    /// </summary>
    public string QQ { get; set; }

    /// <summary>
    /// 孩子
    /// </summary>
    public List<ChildrenInputDto> Childrens { get; set; }

    /// <summary>
    /// 孩子
    /// </summary>
    public List<PostInputDto> Posts { get; set; }
}