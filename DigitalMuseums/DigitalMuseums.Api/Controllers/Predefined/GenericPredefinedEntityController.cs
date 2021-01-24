using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DigitalMuseums.Api.Attributes;
using DigitalMuseums.Api.Contracts.Requests;
using DigitalMuseums.Api.Contracts.Responses;
using DigitalMuseums.Core.Domain.Models;
using DigitalMuseums.Core.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalMuseums.Api.Controllers.Predefined
{
    /// <summary>
    /// The generic predefined entity controller.
    /// </summary>
    /// <typeparam name="TEntity">Any predefined entity.</typeparam>
    /// <typeparam name="TEntityResponse">Corresponding predefined API model.</typeparam>
    [ApiController]
    [Route("api/[controller]")]
    [GenericControllerName]
    public class GenericPredefinedEntityController<TEntity, TEntityResponse> : ControllerBase
        where TEntity : BasePredefinedEntity
        where TEntityResponse : BasePredefinedEntityResponse
    {
        /// <summary>
        /// The entity service.
        /// </summary>
        protected readonly IBasePredefinedEntityService<TEntity> entityService;

        /// <summary>
        /// The mapper.
        /// </summary>
        protected readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericPredefinedEntityController{TEntity,TEntityResponse}"/> class.
        /// </summary>
        /// <param name="entityService">The entity service.</param>
        /// <param name="mapper">The mapper.</param>
        public GenericPredefinedEntityController(IBasePredefinedEntityService<TEntity> entityService, IMapper mapper)
        {
            this.entityService = entityService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns>A <see cref="ActionResult"/> that contains <see cref="List{T}"/>.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetAllAsync()
        {
            var allEntities = await this.entityService.GetAllAsync();

            var allApiModels = this.mapper.Map<List<TEntityResponse>>(allEntities);
            return this.Ok(allApiModels);
        }

        /// <summary>
        /// Get one by Id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A <see cref="ActionResult" /> that contains a <see cref="TEntityResponse"/>.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> Get(int id)
        {
            var entity = await this.entityService.GetAsync(id);

            var apiModel = this.mapper.Map<TEntityResponse>(entity);
            return this.Ok(apiModel);
        }

        /// <summary>
        /// Endpoint for search predefined entities.
        /// </summary>
        /// <param name="model">Search model.</param>
        /// <returns>Searched predefined entities.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchRequest model)
        {
            var allEntities = await this.entityService.SearchAsync(model.SearchTerm);

            var result = this.mapper.Map<List<TEntityResponse>>(allEntities);
            return this.Ok(result);
        }
    }

}