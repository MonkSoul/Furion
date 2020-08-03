using System;

namespace Fur.MirrorController.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class RouteSeatAttribute : Attribute
    {
        public RouteSeatAttribute(RouteSeatOptions routeSeatOptions = RouteSeatOptions.ActionEnd)
        {
            RouteSeat = routeSeatOptions;
        }

        public RouteSeatOptions RouteSeat { get; set; }
    }

    /// <summary>
    /// 参数路由位置
    /// </summary>
    public enum RouteSeatOptions
    {
        ActionStart,
        ActionEnd
    }
}
