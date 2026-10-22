using Asp.Versioning;
using atMISEDU.Application.Requests.CATALOG.CatalogUnit;
using atMISEDU.Presentation.Abstractions;
using atMISEDU.Presentation.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace atMISEDU.Presentation.Controllers.v1.CATALOG
{
    /// <summary>
    /// Controller version 1 for CatalogUnit apis
    /// </summary>
    [ApiVersion(1)]
    [Route(RouteConstant.API_PREFIX + RouteConstant.CATALOG_UNIT_ROUTE)]
    public class CatalogUnitController : ApiController
    {
        private readonly IMediator mediator;

        public CatalogUnitController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Api version 1 for create CatalogUnit
        /// </summary>
        /// <param name="request">Request to create CatalogUnit</param>
        /// <returns>Action result</returns>
        [MapToApiVersion(1)]
        [HttpPost]
        public async Task<IActionResult> CreateV1(CreateCatalogUnitRequest request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for update CatalogUnit
        /// </summary>
        /// <param name="id">Id of CatalogUnit need to be updated</param>
        /// <param name="request">Request body contains content to update</param>
        /// <returns></returns>
        [MapToApiVersion(1)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateV1(int id, [FromBody] UpdateCatalogUnitRequest request)
        {
            request.Id = id;
            var result = await mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for delete CatalogUnit
        /// </summary>
        /// <param name="id">id of CatalogUnit</param>
        /// <returns>Action result</returns>
        //[MapToApiVersion(1)]
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteV1(int id)
        //{
        //    var command = new DeleteCatalogUnitRequest()
        //    {
        //        Id = id
        //    };
        //    var result = await mediator.Send(command);
        //    return Ok(result);
        //}

        /// <summary>
        /// Api version 1 for get CatalogUnit by id
        /// </summary>
        /// <param name="id">ID of CatalogUnit</param>
        /// <returns>Action result with CatalogUnit as data</returns>
        [MapToApiVersion(1)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailV1(int id)
        {
            var query = new GetDetailCatalogUnitRequest()
            {
                Id = id,
            };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Api version 1 for get all CatalogUnit
        /// </summary>
        /// <returns>Action result with list of CatalogUnit as data</returns>
        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<IActionResult> GetAllV1()
        {
            var query = new GetAllCatalogUnitRequest();
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
