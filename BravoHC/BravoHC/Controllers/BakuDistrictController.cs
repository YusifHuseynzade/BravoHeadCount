﻿using BakuDistrictDetails.Commands.Request;
using BakuDistrictDetails.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ResidentalAreaDetails.Commands.Request;
using ResidentalAreaDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BakuDistrictController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BakuDistrictController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBakuDistrictCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteBakuDistrictCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllBakuDistrictQueryRequest request)
        {
            var districts = await _mediator.Send(request);

            return Ok(districts);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBakuDistrictCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdBakuDistrictQueryRequest { Id = id };
            var residentalArea = await _mediator.Send(requestModel);

            return residentalArea != null
                ? (IActionResult)Ok(residentalArea)
                : NotFound(new { Message = "BakuDistrict not found." });
        }

        [HttpGet("BakuDistrictEmployees")]
        public async Task<IActionResult> GetBakuDistrictEmployees([FromQuery] GetBakuDistrictEmployeesQueryRequest request)
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