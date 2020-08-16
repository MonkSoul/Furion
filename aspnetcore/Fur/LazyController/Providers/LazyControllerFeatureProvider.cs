using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace Fur.LazyController
{
    /// <summary>
    /// 惰性控制器特性提供器
    /// </summary>
    public sealed class LazyControllerFeatureProvider : ControllerFeatureProvider
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