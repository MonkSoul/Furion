using Furion.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Furion.Core;

public class Person : Entity
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public Person()
    {
        CreatedTime = DateTimeOffset.Now;
    }

    /// <summary>
    /// 姓名
    /// </summary>
    [MaxLength(32)]
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
    /// 从表
    /// </summary>
    public PersonDetail PersonDetail { get; set; }

    /// <summary>
    /// 一对多
    /// </summary>
    public ICollection<Children> Childrens { get; set; }

    /// <summary>
    /// 多对多
    /// </summary>
    public ICollection<Post> Posts { get; set; }
}
