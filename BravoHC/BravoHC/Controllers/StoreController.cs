using EmployeeDetails.Commands.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SectionDetails.Queries.Request;
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
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Add([FromBody] CreateStoreCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Delete([FromBody] DeleteStoreCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Update([FromBody] UpdateStoreCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllStoreQueryRequest request)
        {
            var stores = await _mediator.Send(request);

            return Ok(stores);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdStoreQueryRequest { Id = id };
            var store = await _mediator.Send(requestModel);

            return store != null
                ? (IActionResult)Ok(store)
                : NotFound(new { Message = "Store not found." });
        }

        [HttpGet("storehistory")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetHistory([FromQuery] GetStoreHistoryQueryRequest request)
        {
            var storeHistory = await _mediator.Send(request);

            if (storeHistory == null)
            {
                return NotFound();
            }

            return Ok(storeHistory);
        }
    }
}
