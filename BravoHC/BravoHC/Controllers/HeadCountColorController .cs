using Common.Interfaces;
using EmployeeDetails.Commands.Request;
using EmployeeDetails.Queries.Request;
using HeadCountBackGroundColorDetails.Commands.Request;
using HeadCountBackGroundColorDetails.Queries.Request;
using HeadCountDetails.Commands.Request;
using HeadCountDetails.ExcelImportService;
using HeadCountDetails.HeadCountExportedService;
using HeadCountDetails.Queries.Request;
using Infrastructure;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadCountColorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public HeadCountColorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Add([FromBody] CreateColorCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Delete([FromBody] DeleteColorCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Update([FromBody] UpdateColorCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllColorQueryRequest request)
        {
            var headCount = await _mediator.Send(request);

            return Ok(headCount);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdColorQueryRequest { Id = id };
            var headCount = await _mediator.Send(requestModel);

            return headCount != null
                ? (IActionResult)Ok(headCount)
                : NotFound(new { Message = "Color not found." });
        }
    }
}
