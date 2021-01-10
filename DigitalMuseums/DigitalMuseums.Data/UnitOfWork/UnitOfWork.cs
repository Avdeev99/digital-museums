﻿using DigitalMuseums.Core.Data.Contracts;

namespace DigitalMuseums.Data.UnitOfWork
{
    /// <inheritdoc />
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly Microsoft.EntityFrameworkCore.DbContext dbContext;

        /// <summary>
        /// The repository factory.
        /// </summary>
        private readonly IRepositoryFactory repositoryFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="dbContext">The <see cref="DbContext"/> reference.</param>
        /// <param name="repositoryFactory">The <see cref="IRepositoryFactory"/> reference.</param>
        public UnitOfWork(Microsoft.EntityFrameworkCore.DbContext dbContext, IRepositoryFactory repositoryFactory)
        {
            this.dbContext = dbContext;
            this.repositoryFactory = repositoryFactory;
        }

        /// <inheritdoc />
        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return repositoryFactory.GetRepository<TEntity>();
        }

        /// <inheritdoc />
        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        /// <inheritdoc />
        public async void SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
