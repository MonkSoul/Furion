// ------------------------------------------------------------------------
// 版权信息
// 版权归百小僧及百签科技（广东）有限公司所有。
// 所有权利保留。
// 官方网站：https://baiqian.com
//
// 许可证信息
// Furion 项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。
// 许可证的完整文本可以在源代码树根目录中的 LICENSE-APACHE 和 LICENSE-MIT 文件中找到。
// 官方网站：https://furion.net
//
// 使用条款
// 使用本代码应遵守相关法律法规和许可证的要求。
//
// 免责声明
// 对于因使用本代码而产生的任何直接、间接、偶然、特殊或后果性损害，我们不承担任何责任。
//
// 其他重要信息
// Furion 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。
// 有关 Furion 项目的其他详细信息，请参阅位于源代码树根目录中的 COPYRIGHT 和 DISCLAIMER 文件。
//
// 更多信息
// 请访问 https://gitee.com/dotnetchina/Furion 获取更多关于 Furion 项目的许可证和版权信息。
// ------------------------------------------------------------------------

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