using Fur.ApplicationBase;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace Fur.AttachController.Providers
{
    internal sealed class AttachControllerFeatureProvider : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
            => ApplicationCore.IsControllerType(typeInfo, true);
    }
}