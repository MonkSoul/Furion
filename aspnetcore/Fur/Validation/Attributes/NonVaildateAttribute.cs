using Fur.AppCore.Attributes;
using System;

namespace Fur.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class), NonInflated]
    public sealed class NonVaildateAttribute : Attribute
    {
    }
}