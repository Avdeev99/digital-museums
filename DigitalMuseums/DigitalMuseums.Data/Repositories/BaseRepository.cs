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
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> _databaseSet;
        
        public BaseRepository(DigitalMuseumsContext context)
        {
            _databaseSet = context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync(TrackingState trackingState = TrackingState.Disabled)
        {
            var query = _databaseSet.AsQueryable();

            if (trackingState == TrackingState.Disabled)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter,
            TrackingState trackingState = TrackingState.Disabled)
        {
            var query = _databaseSet.AsQueryable();

            if (trackingState == TrackingState.Disabled)
            {
                query = query.AsNoTracking();
            }

            query = query.Where(filter);

            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter,
            List<Expression<Func<TEntity, object>>> includes,
            TrackingState trackingState = TrackingState.Disabled)
        {
            IQueryable<TEntity> query = _databaseSet.AsQueryable();

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

        public async Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> filter,
            TrackingState trackingState = TrackingState.Disabled)
        {
            var query = _databaseSet.AsQueryable();

            if (trackingState == TrackingState.Disabled)
            {
                query = query.AsNoTracking();
            }

            query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> filter,
            List<Expression<Func<TEntity, object>>> includes,
            TrackingState trackingState = TrackingState.Disabled)
        {
            var query = _databaseSet.AsQueryable();

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

        public TEntity Create(TEntity entity)
        {
            var createdEntity = _databaseSet.Add(entity).Entity;
            return createdEntity;
        }

        public TEntity Update(TEntity entity)
        {
            var updatedEntity = _databaseSet.Update(entity).Entity;
            return updatedEntity;
        }

        public void Delete(TEntity entity)
        {
            _databaseSet.Remove(entity);
        }

        public async void DeleteAsync(string id)
        {
            var entity = await _databaseSet.FindAsync(id);
            if (entity != null)
            {
                _databaseSet.Remove(entity);
            }
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _databaseSet.CountAsync(filter);
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await _databaseSet.AnyAsync(condition);
        }
    }
}
