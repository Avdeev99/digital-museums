using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DigitalMuseums.Api.Extensions
{
    /// <summary>
    /// Represents extensions for <see cref="ModelStateDictionary"/>
    /// </summary>
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Get model state errors.
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns>Errors</returns>
        public static List<ModelError> GetErrors(this ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(modelState => modelState.Errors);
            
            return errors.ToList();
        }
    }
}