using Fur.DynamicApiController;
using System;

namespace Fur.Application
{
    public class FurAppService : IDynamicApiController
    {
        public string RouteSeat(int id, string name)
        {
            return "配置路由参数位置";
        }

        public string RouteSeat(
            [ApiSeat(ApiSeats.ControllerStart)] int id, // 控制器名称之前
            [ApiSeat(ApiSeats.ControllerEnd)] string name, // 控制器名称之后
            [ApiSeat(ApiSeats.ControllerEnd)] int age, // 控制器名称之后
            [ApiSeat(ApiSeats.ActionStart)] decimal weight, // 动作方法名称之前
            [ApiSeat(ApiSeats.ActionStart)] float height, // 动作方法名称之前
            [ApiSeat(ApiSeats.ActionEnd)] DateTime birthday) // 动作方法名称之后（默认值）
        {
            return "配置路由参数位置";
        }
    }
}