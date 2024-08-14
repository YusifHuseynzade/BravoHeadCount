﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using ResidentalAreaDetails.Commands.Request;
using ResidentalAreaDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentalAreaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ResidentalAreaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateResidentalAreaCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteResidentalAreaCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllResidentalAreaQueryRequest request)
        {
            var districts = await _mediator.Send(request);

            return Ok(districts);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateResidentalAreaCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdResidentalAreaQueryRequest { Id = id };
            var residentalArea = await _mediator.Send(requestModel);

            return residentalArea != null
                ? (IActionResult)Ok(residentalArea)
                : NotFound(new { Message = "ResidentalArea not found." });
        }

        [HttpGet("ResidentalAreaEmployees")]
        public async Task<IActionResult> GetFunctionalAreaProjects([FromQuery] GetResidentalAreaEmployeesQueryRequest request)
        {
            var employees = await _mediator.Send(request);

            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

    }
}
