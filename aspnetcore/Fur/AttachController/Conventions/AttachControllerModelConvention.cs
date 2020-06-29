using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Fur.AttachController.Conventions
{
    internal sealed class AttachControllerModelConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controllerModel in application.Controllers)
            {
                var controllerTypeInfo = controllerModel.ControllerType;
            }
        }
    }
}
