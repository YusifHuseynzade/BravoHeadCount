using GeneralSettingDetails.Commands.Request;
using GeneralSettingDetails.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralSettingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GeneralSettingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPut]
        //[Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Update([FromBody] UpdateGeneralSettingCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet]
        //[Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllGeneralSettingQueryRequest request)
        {
            var GeneralSettings = await _mediator.Send(request);

            return Ok(GeneralSettings);
        }
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdGeneralSettingQueryRequest { Id = id };
            var GeneralSetting = await _mediator.Send(requestModel);

            return GeneralSetting != null
                ? (IActionResult)Ok(GeneralSetting)
                : NotFound(new { Message = "GeneralSetting not found." });
        }
    }
}
