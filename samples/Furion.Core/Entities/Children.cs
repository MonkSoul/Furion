using Furion.DatabaseAccessor;
using System;

namespace Furion.Core;

public class Children : Entity
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public Children()
    {
        CreatedTime = DateTimeOffset.Now;
    }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// 外键
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// 主表
    /// </summary>
    public Person Person { get; set; }
}