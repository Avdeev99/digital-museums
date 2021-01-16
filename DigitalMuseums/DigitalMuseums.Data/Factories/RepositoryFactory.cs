using System;
using DigitalMuseums.Core.Data.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalMuseums.Data.Factories
{
    /// <inheritdoc />
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFactory"/> class.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> reference.</param>
        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return serviceProvider.GetRequiredService<IBaseRepository<TEntity>>();
        }
    }
}
