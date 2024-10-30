using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoneyOrderDetails.Commands.Request;
using MoneyOrderDetails.Queries.Request;

namespace FinanceOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyOrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MoneyOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        //[Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Add([FromQuery] CreateMoneyOrderCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        //[Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Delete([FromBody] DeleteMoneyOrderCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPut]
        //[Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Update([FromQuery] UpdateMoneyOrderCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        //[Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllMoneyOrderQueryRequest request)
        {
            var MoneyOrders = await _mediator.Send(request);

            return Ok(MoneyOrders);
        }
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdMoneyOrderQueryRequest { Id = id };
            var MoneyOrder = await _mediator.Send(requestModel);

            return MoneyOrder != null
                ? (IActionResult)Ok(MoneyOrder)
                : NotFound(new { Message = "MoneyOrder not found." });
        }
        [HttpGet("moneyorderhistory")]
        //[Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetHistory([FromQuery] GetMoneyOrderHistoryQueryRequest request)
        {
            var moneyOrderHistory = await _mediator.Send(request);

            if (moneyOrderHistory == null)
            {
                return NotFound();
            }

            return Ok(moneyOrderHistory);
        }
    }
}
