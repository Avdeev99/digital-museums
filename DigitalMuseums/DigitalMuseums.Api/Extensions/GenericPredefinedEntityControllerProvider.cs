using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DigitalMuseums.Api.Contracts.Responses;
using DigitalMuseums.Api.Controllers.Predefined;
using DigitalMuseums.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace DigitalMuseums.Api.Extensions
{
    public class GenericPredefinedEntityControllerProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        /// <summary>
        /// The API model name suffix.
        /// </summary>
        private const string ApiModelNameSuffix = "Response";
        private const string ControllerSuffix = "Controller";

        /// <inheritdoc />
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var defaultApiModelType = typeof(BasePredefinedEntityResponse);
            var domainTypesAssembly = typeof(BasePredefinedEntity).Assembly;
            var apiTypesAssembly = defaultApiModelType.Assembly;

            Type[] domainTypes = domainTypesAssembly.GetExportedTypes();
            Type[] apiTypes = apiTypesAssembly.GetExportedTypes();

            // This does not guarantee the domain -> api model mapping. We have to ensure the presence of both types manually.
            List<Type> predefinedDomainTypes = domainTypes.Where(type => type.IsSubclassOf(typeof(BasePredefinedEntity))).ToList();
            List<Type> predefinedApiTypes = apiTypes.Where(type => type.IsSubclassOf(typeof(BasePredefinedEntityResponse))).ToList();

            var typesMapping = new Dictionary<Type, Type>();
            foreach (var predefinedDomainType in predefinedDomainTypes)
            {
                var firstMatchingApiModelType = predefinedApiTypes.FirstOrDefault(NamesMatch(predefinedDomainType));

                typesMapping.Add(predefinedDomainType, firstMatchingApiModelType ?? defaultApiModelType);
            }

            foreach (var (domainType, apiModelType) in typesMapping)
            {
                Type controllerType = typeof(GenericPredefinedEntityController<,>);

                controllerType = controllerType.MakeGenericType(domainType, apiModelType);

                var controllerTypeInfo = controllerType.GetTypeInfo();

                var controllerName = domainType.Name + ControllerSuffix;

                if (!feature.Controllers.Any(controller => controller.Name == controllerName))
                {
                    feature.Controllers.Add(controllerTypeInfo);   
                }
            }
        }

        /// <summary>
        /// Names matching predicate.
        /// </summary>
        /// <param name="predefinedDomainType">The predefined domain type.</param>
        /// <returns>The <see cref="Func{Type, bool}"/>.</returns>
        private static Func<Type, bool> NamesMatch(Type predefinedDomainType)
        {
            return apiType => string.Equals(
                apiType.Name,
                predefinedDomainType.Name + ApiModelNameSuffix,
                StringComparison.InvariantCultureIgnoreCase);
        }
    }
}