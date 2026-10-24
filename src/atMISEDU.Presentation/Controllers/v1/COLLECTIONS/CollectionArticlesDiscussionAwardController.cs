using Asp.Versioning;
using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionAward;
using atMISEDU.Presentation.Abstractions;
using atMISEDU.Presentation.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace atMISEDU.Presentation.Controllers.v1.COLLECTIONS
{
    /// <summary>
    /// Controller version 1 for CollectionArticlesDiscussionAward apis
    /// </summary>
    [ApiVersion(1)]
    [Route(RouteConstant.API_PREFIX + RouteConstant.COLLECTION_ARTICLES_DISCUSSION_AWARD_ROUTE)]
    public class CollectionArticlesDiscussionAwardController : ApiController
    {
        private readonly IMediator mediator;

        public CollectionArticlesDiscussionAwardController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Api version 1 for create CollectionArticlesDiscussionAward
        /// </summary>
        /// <param name="request">Request to create CollectionArticlesDiscussionAward</param>
        /// <returns>Action result with id of created CollectionArticlesDiscussionAward</returns>
        [MapToApiVersion(1)]
        [HttpPost]
        public async Task<IActionResult> CreateV1(CreateCollectionArticlesDiscussionAwardRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for update CollectionArticlesDiscussionAward
        /// </summary>
        /// <param name="id">Id of CollectionArticlesDiscussionAward need to be updated</param>
        /// <param name="request">Request body contains content to update</param>
        /// <returns></returns>
        [MapToApiVersion(1)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateV1(int id, [FromBody] UpdateCollectionArticlesDiscussionAwardRequest request)
        {
            request.Id = id;
            var result = await mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for delete CollectionArticlesDiscussionAward
        /// </summary>
        /// <param name="id">id of CollectionArticlesDiscussionAward</param>
        /// <returns>Action result</returns>
        [MapToApiVersion(1)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteV1(int id)
        {
            var command = new DeleteCollectionArticlesDiscussionAwardRequest()
            {
                Id = id
            };
            var result = await mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for get CollectionArticlesDiscussionAward by id
        /// </summary>
        /// <param name="id">ID of CollectionArticlesDiscussionAward</param>
        /// <returns>Action result with CollectionArticlesDiscussionAward as data</returns>
        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailV1(int id)
        {
            var query = new GetDetailCollectionArticlesDiscussionAwardRequest()
            {
                Id = id,
            };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for get all CollectionArticlesDiscussionAward
        /// </summary>
        /// <param name="query">Filters sent by query string</param>
        /// <returns>Action result with list of CollectionArticlesDiscussionAward as data</returns>
        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAllV1([FromQuery] GetAllCollectionArticlesDiscussionAwardRequest query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
