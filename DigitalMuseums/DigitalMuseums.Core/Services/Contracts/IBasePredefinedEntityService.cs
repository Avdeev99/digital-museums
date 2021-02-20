using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DigitalMuseums.Core.Domain.Models;

namespace DigitalMuseums.Core.Services.Contracts
{
    /// <summary>
    /// The Predefined Entity Service interface.
    /// </summary>
    /// <typeparam name="TPredefinedEntity">The predefined entity.</typeparam>
    public interface IBasePredefinedEntityService<TPredefinedEntity>
        where TPredefinedEntity : BasePredefinedEntity
    {
        /// <summary>
        /// Gets all predefined entities.
        /// </summary>
        /// <returns>
        /// <see cref="List{T}"/>.
        /// </returns>
        Task<List<TPredefinedEntity>> GetAllAsync();
        
        /// <summary>
        /// Get one.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The predefined entity list.</returns>
        Task<List<TPredefinedEntity>> GetAllAsync(Expression<Func<TPredefinedEntity, bool>> filter);

        /// <summary>
        /// Get one.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The predefined entity.</returns>
        Task<TPredefinedEntity> GetAsync(int id);

        /// <summary>
        /// Searches the specified predefined entities by search text.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns>
        /// <see cref="List{TTPredefinedEntity}"/>.
        /// </returns>
        Task<List<TPredefinedEntity>> SearchAsync(string searchText);
    }

}