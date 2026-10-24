using Asp.Versioning;
using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticles;
using atMISEDU.Presentation.Abstractions;
using atMISEDU.Presentation.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace atMISEDU.Presentation.Controllers.v1.COLLECTIONS
{
    /// <summary>
    /// Controller version 1 for CollectionArticles apis
    /// </summary>
    [ApiVersion(1)]
    [Route(RouteConstant.API_PREFIX + RouteConstant.COLLECTION_ARTICLES_ROUTE)]
    public class CollectionArticlesController : ApiController
    {
        private readonly IMediator mediator;

        public CollectionArticlesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Api version 1 for create CollectionArticles
        /// </summary>
        /// <param name="request">Request to create CollectionArticles</param>
        /// <returns>Action result with id of created CollectionArticles</returns>
        [MapToApiVersion(1)]
        [HttpPost]
        public async Task<IActionResult> CreateV1(CreateCollectionArticlesRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for update CollectionArticles
        /// </summary>
        /// <param name="id">Id of CollectionArticles need to be updated</param>
        /// <param name="request">Request body contains content to update</param>
        /// <returns></returns>
        [MapToApiVersion(1)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateV1(int id, [FromBody] UpdateCollectionArticlesRequest request)
        {
            request.Id = id;
            var result = await mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for delete CollectionArticles
        /// </summary>
        /// <param name="id">id of CollectionArticles</param>
        /// <returns>Action result</returns>
        [MapToApiVersion(1)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteV1(int id)
        {
            var command = new DeleteCollectionArticlesRequest()
            {
                Id = id
            };
            var result = await mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for get CollectionArticles by id
        /// </summary>
        /// <param name="id">ID of CollectionArticles</param>
        /// <returns>Action result with CollectionArticles (tags and counts included) as data</returns>
        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailV1(int id)
        {
            var query = new GetDetailCollectionArticlesRequest()
            {
                Id = id,
            };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for get all CollectionArticles
        /// </summary>
        /// <param name="query">Filters sent by query string</param>
        /// <returns>Action result with list of CollectionArticles as data</returns>
        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAllV1([FromQuery] GetAllCollectionArticlesRequest query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
