using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Template.Web.Api.Configuration;
using Template.Web.Api.Controllers;

namespace Template.Web.Api.Services.FeatureProviders;

public class TemplateControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
{
    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
    {
        
        
        foreach (var type in InjectedTypes.NotesTypes)
        {
            var types = new[]
            {
                type.Entity,
                type.CreateDto,
                type.EditorDto,
                type.ViewDto,
                type.TableViewDto
            };
            
            var controllerType = typeof(TemplateController<,,,,>).MakeGenericType(types).GetTypeInfo();
            feature.Controllers.Add(controllerType);
        }
    }
}