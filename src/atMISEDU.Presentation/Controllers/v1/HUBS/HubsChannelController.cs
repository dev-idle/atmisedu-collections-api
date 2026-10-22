using Asp.Versioning;
using atMISEDU.Application.Requests.HUBS.HubsChannel;
using atMISEDU.Presentation.Abstractions;
using atMISEDU.Presentation.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace atMISEDU.Presentation.Controllers.v1.HUBS
{
    /// <summary>
    /// Controller version 1 for HubsChannel apis
    /// </summary>
    [ApiVersion(1)]
    [Route(RouteConstant.API_PREFIX + RouteConstant.HUBS_CHANNEL_ROUTE)]
    public class HubsChannelController : ApiController
    {
        private readonly IMediator mediator;

        public HubsChannelController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Api version 1 for create HubsChannel
        /// </summary>
        /// <param name="request">Request to create HubsChannel</param>
        /// <returns>Action result</returns>
        [MapToApiVersion(1)]
        [HttpPost]
        public async Task<IActionResult> CreateV1(CreateHubsChannelRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for update HubsChannel
        /// </summary>
        /// <param name="id">Id of HubsChannel need to be updated</param>
        /// <param name="request">Request body contains content to update</param>
        /// <returns></returns>
        [MapToApiVersion(1)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateV1(int id, [FromBody] UpdateHubsChannelRequest request)
        {
            request.Id = id;
            var result = await mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for delete HubsChannel
        /// </summary>
        /// <param name="id">id of HubsChannel</param>
        /// <returns>Action result</returns>
        [MapToApiVersion(1)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteV1(int id)
        {
            var command = new DeleteHubsChannelRequest()
            {
                Id = id
            };
            var result = await mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for get HubsChannel by id
        /// </summary>
        /// <param name="id">ID of HubsChannel</param>
        /// <returns>Action result with HubsChannel as data</returns>
        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailV1(int id)
        {
            var query = new GetDetailHubsChannelRequest()
            {
                Id = id,
            };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for get all HubsChannel
        /// </summary>
        /// <returns>Action result with list of HubsChannel as data</returns>
        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAllV1()
        {
            var query = new GetAllHubsChannelRequest();
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
