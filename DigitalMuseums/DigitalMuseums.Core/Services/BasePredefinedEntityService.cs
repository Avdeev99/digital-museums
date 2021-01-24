using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DigitalMuseums.Core.Data.Contracts;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Core.Services.Contracts;

namespace DigitalMuseums.Core.Services
{
    /// <inheritdoc />
    public class BasePredefinedEntityService<TPredefinedEntity> : IBasePredefinedEntityService<TPredefinedEntity>
        where TPredefinedEntity : BasePredefinedEntity
    {
        /// <summary>
        /// The unitOfWork.
        /// </summary>
        private IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePredefinedEntityService{TPredefinedEntity}" /> class.
        /// </summary>
        /// <param name="unitOfWork">The Unit of Work.</param>
        public BasePredefinedEntityService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<List<TPredefinedEntity>> GetAllAsync()
        {
            IBaseRepository<TPredefinedEntity> repository = unitOfWork.GetRepository<TPredefinedEntity>();
            return await repository.GetAllAsync();
        }

        public async Task<List<TPredefinedEntity>> GetAllAsync(Expression<Func<TPredefinedEntity, bool>> filter)
        {
            IBaseRepository<TPredefinedEntity> repository = unitOfWork.GetRepository<TPredefinedEntity>();
            return await repository.GetAllAsync(filter);
        }

        /// <inheritdoc />
        public async Task<TPredefinedEntity> GetAsync(int id)
        {
            IBaseRepository<TPredefinedEntity> repository = unitOfWork.GetRepository<TPredefinedEntity>();
            
            var predefinedEntity = await repository.GetAsync(entity => entity.Id == id);
            if (predefinedEntity == null)
            {
                throw new ArgumentException($"{nameof(TPredefinedEntity)} with Id: {id} not found. Operation={nameof(this.GetAsync)}");
            }

            return predefinedEntity;
        }

        /// <inheritdoc />
        public async Task<List<TPredefinedEntity>> SearchAsync(string searchText)
        {
            IBaseRepository<TPredefinedEntity> repository = unitOfWork.GetRepository<TPredefinedEntity>();
            searchText = searchText.ToLower();
            return await repository.GetAllAsync(x => x.Name.ToLower().StartsWith(searchText));
        }
    }
}