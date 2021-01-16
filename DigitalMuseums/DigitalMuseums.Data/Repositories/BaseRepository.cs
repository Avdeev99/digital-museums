using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Data.DbContext;
using DigitalMuseums.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DigitalMuseums.Data.Repositories
{
    /// <inheritdoc />
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> databaseSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> reference.</param>
        public BaseRepository(DigitalMuseumsContext context)
        {
            databaseSet = context.Set<TEntity>();
        }

        /// <inheritdoc />
        public async Task<List<TEntity>> GetAllAsync(TrackingState trackingState = TrackingState.Disabled)
        {
            var query = databaseSet.AsQueryable();

            if (trackingState == TrackingState.Disabled)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter,
            TrackingState trackingState = TrackingState.Disabled)
        {
            var query = databaseSet.AsQueryable();

            if (trackingState == TrackingState.Disabled)
            {
                query = query.AsNoTracking();
            }

            query = query.Where(filter);

            return await query.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter,
            List<Expression<Func<TEntity, object>>> includes,
            TrackingState trackingState = TrackingState.Disabled)
        {
            IQueryable<TEntity> query = databaseSet.AsQueryable();

            if (trackingState == TrackingState.Disabled)
            {
                query = query.AsNoTracking();
            }

            query = query.Where(filter);

            foreach (var includeExpression in includes)
            {
                query = query.IncludeProperty(includeExpression);
            }

            return await query.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> filter,
            TrackingState trackingState = TrackingState.Disabled)
        {
            var query = databaseSet.AsQueryable();

            if (trackingState == TrackingState.Disabled)
            {
                query = query.AsNoTracking();
            }

            query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> filter,
            List<Expression<Func<TEntity, object>>> includes,
            TrackingState trackingState = TrackingState.Disabled)
        {
            var query = databaseSet.AsQueryable();

            if (trackingState == TrackingState.Disabled)
            {
                query = query.AsNoTracking();
            }

            query = query.Where(filter);

            foreach (var includeExpression in includes)
            {
                query = query.IncludeProperty(includeExpression);
            }

            return await query.FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public TEntity Create(TEntity entity)
        {
            var createdEntity = databaseSet.Add(entity).Entity;
            return createdEntity;
        }

        /// <inheritdoc />
        public TEntity Update(TEntity entity)
        {
            var updatedEntity = databaseSet.Update(entity).Entity;
            return updatedEntity;
        }

        /// <inheritdoc />
        public void Delete(TEntity entity)
        {
            databaseSet.Remove(entity);
        }

        /// <inheritdoc />
        public async void DeleteAsync(string id)
        {
            var entity = await databaseSet.FindAsync(id);
            if (entity != null)
            {
                databaseSet.Remove(entity);
            }
        }

        /// <inheritdoc />
        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await databaseSet.AnyAsync(condition);
        }
    }
}
