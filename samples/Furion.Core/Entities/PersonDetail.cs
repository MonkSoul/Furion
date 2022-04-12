using Furion.DatabaseAccessor;

namespace Furion.Core;

public class PersonDetail : EntityBase
{
    /// <summary>
    /// 电话号码
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// QQ 号码
    /// </summary>
    public string QQ { get; set; }

    /// <summary>
    /// 外键
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// 主表
    /// </summary>
    public Person Person { get; set; }
}
