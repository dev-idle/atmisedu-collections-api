using Asp.Versioning;
using atMISEDU.Application.Requests.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Presentation.Abstractions;
using atMISEDU.Presentation.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace atMISEDU.Presentation.Controllers.v1.COLLECTIONS
{
    /// <summary>
    /// Controller version 1 for CollectionAwardCatalog apis
    /// </summary>
    [ApiVersion(1)]
    [Route(RouteConstant.API_PREFIX + RouteConstant.COLLECTION_AWARD_CATALOG_ROUTE)]
    public class CollectionAwardCatalogController : ApiController
    {
        private readonly IMediator mediator;

        public CollectionAwardCatalogController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Api version 1 for create CollectionAwardCatalog
        /// </summary>
        /// <param name="request">Request to create CollectionAwardCatalog</param>
        /// <returns>Action result with id of created CollectionAwardCatalog</returns>
        [MapToApiVersion(1)]
        [HttpPost]
        public async Task<IActionResult> CreateV1(CreateCollectionAwardCatalogRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for update CollectionAwardCatalog
        /// </summary>
        /// <param name="id">Id of CollectionAwardCatalog need to be updated</param>
        /// <param name="request">Request body contains content to update</param>
        /// <returns></returns>
        [MapToApiVersion(1)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateV1(int id, [FromBody] UpdateCollectionAwardCatalogRequest request)
        {
            request.Id = id;
            var result = await mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for delete CollectionAwardCatalog
        /// </summary>
        /// <param name="id">id of CollectionAwardCatalog</param>
        /// <returns>Action result</returns>
        [MapToApiVersion(1)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteV1(int id)
        {
            var command = new DeleteCollectionAwardCatalogRequest()
            {
                Id = id
            };
            var result = await mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for get CollectionAwardCatalog by id
        /// </summary>
        /// <param name="id">ID of CollectionAwardCatalog</param>
        /// <returns>Action result with CollectionAwardCatalog as data</returns>
        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailV1(int id)
        {
            var query = new GetDetailCollectionAwardCatalogRequest()
            {
                Id = id,
            };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for get all CollectionAwardCatalog
        /// </summary>
        /// <param name="query">Filters sent by query string</param>
        /// <returns>Action result with list of CollectionAwardCatalog as data</returns>
        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAllV1([FromQuery] GetAllCollectionAwardCatalogRequest query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
