using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DigitalMuseums.Api.Attributes
{
    /// <summary>
    /// The GenericControllerNameAttribute.
    /// </summary>
    public class GenericControllerNameAttribute : Attribute, IControllerModelConvention
    {
        /// <inheritdoc />
        public void Apply(ControllerModel controller)
        {
            if (!controller.ControllerType.IsGenericType)
            {
                return;
            }

            var firstGenericType = controller.ControllerType.GetGenericArguments().First();

            controller.ControllerName = firstGenericType.Name;
        }
    }
}