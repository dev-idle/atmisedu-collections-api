using Asp.Versioning;
using atMISEDU.Application.Requests.COLLECTIONS.CollectionTags;
using atMISEDU.Presentation.Abstractions;
using atMISEDU.Presentation.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace atMISEDU.Presentation.Controllers.v1.COLLECTIONS
{
    /// <summary>
    /// Controller version 1 for CollectionTags apis
    /// </summary>
    [ApiVersion(1)]
    [Route(RouteConstant.API_PREFIX + RouteConstant.COLLECTION_TAGS_ROUTE)]
    public class CollectionTagsController : ApiController
    {
        private readonly IMediator mediator;

        public CollectionTagsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Api version 1 for create CollectionTags
        /// </summary>
        /// <param name="request">Request to create CollectionTags</param>
        /// <returns>Action result with id of created CollectionTags</returns>
        [MapToApiVersion(1)]
        [HttpPost]
        public async Task<IActionResult> CreateV1(CreateCollectionTagsRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for update CollectionTags
        /// </summary>
        /// <param name="id">Id of CollectionTags need to be updated</param>
        /// <param name="request">Request body contains content to update</param>
        /// <returns></returns>
        [MapToApiVersion(1)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateV1(int id, [FromBody] UpdateCollectionTagsRequest request)
        {
            request.Id = id;
            var result = await mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for delete CollectionTags
        /// </summary>
        /// <param name="id">id of CollectionTags</param>
        /// <returns>Action result</returns>
        [MapToApiVersion(1)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteV1(int id)
        {
            var command = new DeleteCollectionTagsRequest()
            {
                Id = id
            };
            var result = await mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for get CollectionTags by id
        /// </summary>
        /// <param name="id">ID of CollectionTags</param>
        /// <returns>Action result with CollectionTags as data</returns>
        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailV1(int id)
        {
            var query = new GetDetailCollectionTagsRequest()
            {
                Id = id,
            };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for get all CollectionTags
        /// </summary>
        /// <param name="query">Filters sent by query string</param>
        /// <returns>Action result with list of CollectionTags as data</returns>
        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAllV1([FromQuery] GetAllCollectionTagsRequest query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
