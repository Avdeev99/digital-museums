using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DigitalMuseums.Core.Domain.Models;

namespace DigitalMuseums.Core.Data.Contracts
{
    /// <summary>
    /// Defines interface for Repository base logic.
    /// </summary>
    /// <typeparam name="TEntity">The entity.</typeparam>
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="trackingState">The tracking state mode.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        Task<List<TEntity>> GetAllAsync(TrackingState trackingState = TrackingState.Disabled);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="trackingState">The tracking state mode.</param>
        /// <returns>The <see cref="List"/>.</returns>
        Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter,
            TrackingState trackingState = TrackingState.Disabled);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includes">The includes.</param>
        /// <returns>The <see cref="List{TEntity}"/>.</returns>
        /// <param name="trackingState">The tracking state mode.</param> 
        Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter,
            List<Expression<Func<TEntity, object>>> includes,
            TrackingState trackingState = TrackingState.Disabled);

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The <see cref="TEntity"/>.</returns>
        /// <param name="trackingState">The tracking state mode.</param>
        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> filter,
            TrackingState trackingState = TrackingState.Disabled);

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includes">The include.</param>
        /// <param name="trackingState">The tracking state mode.</param>
        /// <returns>The <see cref="TEntity"/>.</returns>
        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> filter,
            List<Expression<Func<TEntity, object>>> includes,
            TrackingState trackingState = TrackingState.Disabled);

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The <see cref="TEntity"/>.</returns>
        TEntity Create(TEntity entity);

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The <see cref="TEntity"/>.</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <returns>The <see cref="TEntity"/>.</returns>
        void Delete(TEntity entity);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">The id.</param>
        void DeleteAsync(string id);

        /// <summary>
        /// The check entity existence.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The <see cref="TEntity"/>.</returns>
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter);
    }
}
