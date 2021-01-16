using System.Threading.Tasks;

namespace DigitalMuseums.Core.Data.Contracts
{
    /// <summary>
    /// The UnitOfWork interface.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets entity repository.
        /// </summary>
        /// <typeparam name="TEntity">Any entity.</typeparam>
        /// <returns>
        /// The <see cref="IBaseRepository{TEntity}"/>.</returns>
        IBaseRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;

        /// <summary>
        /// The save changes.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// The save changes.
        /// </summary>
        Task SaveChangesAsync();
    }
}
