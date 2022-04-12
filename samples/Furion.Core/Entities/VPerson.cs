using Furion.DatabaseAccessor;

namespace Furion.Core;

public class VPerson : EntityNotKey
{
    /// <summary>
    /// 配置视图名
    /// </summary>
    public VPerson() : base("V_Person")
    {
    }

    /// <summary>
    /// 主键Id
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
}
