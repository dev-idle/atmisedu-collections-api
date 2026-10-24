using Asp.Versioning;
using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Presentation.Abstractions;
using atMISEDU.Presentation.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace atMISEDU.Presentation.Controllers.v1.COLLECTIONS
{
    /// <summary>
    /// Controller version 1 for CollectionArticlesDiscussionInteraction apis
    /// </summary>
    [ApiVersion(1)]
    [Route(RouteConstant.API_PREFIX + RouteConstant.COLLECTION_ARTICLES_DISCUSSION_INTERACTION_ROUTE)]
    public class CollectionArticlesDiscussionInteractionController : ApiController
    {
        private readonly IMediator mediator;

        public CollectionArticlesDiscussionInteractionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Api version 1 for create CollectionArticlesDiscussionInteraction
        /// </summary>
        /// <param name="request">Request to create CollectionArticlesDiscussionInteraction</param>
        /// <returns>Action result with id of created CollectionArticlesDiscussionInteraction</returns>
        [MapToApiVersion(1)]
        [HttpPost]
        public async Task<IActionResult> CreateV1(CreateCollectionArticlesDiscussionInteractionRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for update CollectionArticlesDiscussionInteraction
        /// </summary>
        /// <param name="id">Id of CollectionArticlesDiscussionInteraction need to be updated</param>
        /// <param name="request">Request body contains content to update</param>
        /// <returns></returns>
        [MapToApiVersion(1)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateV1(int id, [FromBody] UpdateCollectionArticlesDiscussionInteractionRequest request)
        {
            request.Id = id;
            var result = await mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for delete CollectionArticlesDiscussionInteraction
        /// </summary>
        /// <param name="id">id of CollectionArticlesDiscussionInteraction</param>
        /// <returns>Action result</returns>
        [MapToApiVersion(1)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteV1(int id)
        {
            var command = new DeleteCollectionArticlesDiscussionInteractionRequest()
            {
                Id = id
            };
            var result = await mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for get CollectionArticlesDiscussionInteraction by id
        /// </summary>
        /// <param name="id">ID of CollectionArticlesDiscussionInteraction</param>
        /// <returns>Action result with CollectionArticlesDiscussionInteraction as data</returns>
        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailV1(int id)
        {
            var query = new GetDetailCollectionArticlesDiscussionInteractionRequest()
            {
                Id = id,
            };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for get all CollectionArticlesDiscussionInteraction
        /// </summary>
        /// <param name="query">Filters sent by query string</param>
        /// <returns>Action result with list of CollectionArticlesDiscussionInteraction as data</returns>
        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAllV1([FromQuery] GetAllCollectionArticlesDiscussionInteractionRequest query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
