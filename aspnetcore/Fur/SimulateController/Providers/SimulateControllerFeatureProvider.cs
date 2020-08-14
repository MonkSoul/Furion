using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace Fur.SimulateController
{
    /// <summary>
    /// 自定义模刻控制器提供器
    /// </summary>
    public sealed class SimulateControllerFeatureProvider : ControllerFeatureProvider
    {
        /// <summary>
        /// 扫描控制器
        /// </summary>
        /// <param name="typeInfo">类型</param>
        /// <returns>bool</returns>
        protected override bool IsController(TypeInfo typeInfo)
            => Penetrates.IsController(typeInfo);
    }
}