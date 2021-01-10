namespace DigitalMuseums.Core.Data.Contracts
{
    /// <summary>
    /// The repository factory interface.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Gets the repository instance.
        /// </summary>
        /// <typeparam name="TEntity">Any entity.</typeparam>
        /// <returns>The <see cref="IBaseRepository{TEntity}"/>.</returns>
        IBaseRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;
    }
}
