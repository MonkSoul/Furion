// MIT License
//
// Copyright (c) 2020-2022 百小僧, Baiqian Co.,Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Furion.DatabaseAccessor;

/// <summary>
/// 租户实体
/// </summary>
public class Tenant : IEntity<MultiTenantDbContextLocator>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public Tenant()
    {
        CreatedTime = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// 租户Id
    /// </summary>
    [Key]
    [ScaffoldColumn(false)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual Guid TenantId { get; set; }

    /// <summary>
    /// 租户名
    /// </summary>
    [Required, MaxLength(128)]
    public virtual string Name { get; set; }

    /// <summary>
    /// 来源主机地址
    /// </summary>
    [MaxLength(256)]
    public virtual string Host { get; set; }

    /// <summary>
    /// 电子邮箱
    /// </summary>
    [EmailAddress, MaxLength(256)]
    public virtual string EmailAddress { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    [Phone, MaxLength(32)]
    public virtual string PhoneNumber { get; set; }

    /// <summary>
    /// 架构名
    /// </summary>
    [MaxLength(32)]
    public virtual string Schema { get; set; }

    /// <summary>
    /// 数据库链接字符串
    /// </summary>
    [MaxLength(256)]
    public virtual string ConnectionString { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public virtual DateTimeOffset CreatedTime { get; set; }
}