using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Template.Web.Api.Controllers;

namespace Template.Web.Api.Services.Conventions;

[AttributeUsage(AttributeTargets.Class)]
public class NotesControllerConvention : Attribute, IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        if (!controller.ControllerType.IsGenericType ||
            controller.ControllerType.GetGenericTypeDefinition() != typeof(TemplateController<,,,,>))
        {
            return;
        }
        
        var entityType = controller.ControllerType.GenericTypeArguments[0];
        
        controller.Selectors.Clear();
        controller.Selectors.Add(new SelectorModel
        {
            AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(entityType.Name.ToLower() + "s"))
        });
    }
}