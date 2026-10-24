using Asp.Versioning;
using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Presentation.Abstractions;
using atMISEDU.Presentation.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace atMISEDU.Presentation.Controllers.v1.COLLECTIONS
{
    /// <summary>
    /// Controller version 1 for CollectionArticlesDiscussion apis
    /// </summary>
    [ApiVersion(1)]
    [Route(RouteConstant.API_PREFIX + RouteConstant.COLLECTION_ARTICLES_DISCUSSION_ROUTE)]
    public class CollectionArticlesDiscussionController : ApiController
    {
        private readonly IMediator mediator;

        public CollectionArticlesDiscussionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Api version 1 for create CollectionArticlesDiscussion
        /// </summary>
        /// <param name="request">Request to create CollectionArticlesDiscussion</param>
        /// <returns>Action result with id of created CollectionArticlesDiscussion</returns>
        [MapToApiVersion(1)]
        [HttpPost]
        public async Task<IActionResult> CreateV1(CreateCollectionArticlesDiscussionRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for update CollectionArticlesDiscussion
        /// </summary>
        /// <param name="id">Id of CollectionArticlesDiscussion need to be updated</param>
        /// <param name="request">Request body contains content to update</param>
        /// <returns></returns>
        [MapToApiVersion(1)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateV1(int id, [FromBody] UpdateCollectionArticlesDiscussionRequest request)
        {
            request.Id = id;
            var result = await mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for delete CollectionArticlesDiscussion
        /// </summary>
        /// <param name="id">id of CollectionArticlesDiscussion</param>
        /// <returns>Action result</returns>
        [MapToApiVersion(1)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteV1(int id)
        {
            var command = new DeleteCollectionArticlesDiscussionRequest()
            {
                Id = id
            };
            var result = await mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for get CollectionArticlesDiscussion by id
        /// </summary>
        /// <param name="id">ID of CollectionArticlesDiscussion</param>
        /// <returns>Action result with CollectionArticlesDiscussion (include interaction and award count) as data</returns>
        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailV1(int id)
        {
            var query = new GetDetailCollectionArticlesDiscussionRequest()
            {
                Id = id,
            };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for get all CollectionArticlesDiscussion
        /// </summary>
        /// <param name="query">Filters sent by query string</param>
        /// <returns>Action result with list of CollectionArticlesDiscussion as data</returns>
        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAllV1([FromQuery] GetAllCollectionArticlesDiscussionRequest query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
