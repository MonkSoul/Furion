using Fur.AppCore.Attributes;
using System;

namespace Fur.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class), NonWrapper]
    public class NonVaildateAttribute : Attribute
    {
    }
}