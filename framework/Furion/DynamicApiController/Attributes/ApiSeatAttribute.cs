// 版权归百小僧及百签科技（广东）有限公司所有。
//
// 此源代码遵循位于源代码树根目录中的 LICENSE 文件的许可证。

namespace Microsoft.AspNetCore.Mvc;

/// <summary>
/// 接口参数位置设置
/// </summary>
[SuppressSniffer, AttributeUsage(AttributeTargets.Parameter)]
public class ApiSeatAttribute : Attribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="seat"></param>
    public ApiSeatAttribute(ApiSeats seat = ApiSeats.ActionEnd)
    {
        Seat = seat;
    }

    /// <summary>
    /// 参数位置
    /// </summary>
    public ApiSeats Seat { get; set; }
}