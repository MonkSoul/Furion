using System.Collections.Generic;
using System.Reflection;

namespace Fur.ApplicationSystem.Models
{
    /// <summary>
    /// 应用程序集信息类
    /// </summary>
    public sealed class ApplicationAssemblyInfo
    {
        /// <summary>
        /// 程序集
        /// </summary>
        public Assembly Assembly { get; set; }
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 程序集完整名称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 程序集公开类型
        /// </summary>
        public IEnumerable<ApplicationTypeInfo> PublicClassTypes { get; set; }
    }
}
