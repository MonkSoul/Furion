using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace Fur.MirrorController.Providers
{
    internal sealed class MirrorControllerFeatureProvider : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
            => App.IsControllerType(typeInfo, true);
    }
}