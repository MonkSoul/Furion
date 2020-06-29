using System;

namespace Fur.DependencyInjection.Attributes
{
    /// <summary>
    /// 禁止扫描进行依赖注册
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class NotInjectAttribute : Attribute { }
}
