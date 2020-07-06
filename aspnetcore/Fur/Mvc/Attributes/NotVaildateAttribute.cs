using System;

namespace Fur.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class NotVaildateAttribute : Attribute
    {
    }
}
