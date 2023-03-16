// MIT 许可证
//
// 版权 © 2020-present 百小僧, 百签科技（广东）有限公司 和所有贡献者
//
// 特此免费授予任何获得本软件副本和相关文档文件（下称“软件”）的人不受限制地处置该软件的权利，
// 包括不受限制地使用、复制、修改、合并、发布、分发、转授许可和/或出售该软件副本，
// 以及再授权被配发了本软件的人如上的权利，须在下列条件下：
//
// 上述版权声明和本许可声明应包含在该软件的所有副本或实质成分中。
//
// 本软件是“如此”提供的，没有任何形式的明示或暗示的保证，包括但不限于对适销性、特定用途的适用性和不侵权的保证。
// 在任何情况下，作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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