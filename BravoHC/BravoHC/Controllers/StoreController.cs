using EmployeeDetails.Commands.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreDetails.Commands.Request;
using StoreDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StoreController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateStoreCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteStoreCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateStoreCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllStoreQueryRequest request)
        {
            var stores = await _mediator.Send(request);

            return Ok(stores);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdStoreQueryRequest { Id = id };
            var store = await _mediator.Send(requestModel);

            return store != null
                ? (IActionResult)Ok(store)
                : NotFound(new { Message = "Store not found." });
        }
    }
}
