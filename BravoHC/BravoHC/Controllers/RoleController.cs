using ApplicationUserDetails.AppUserRoleDetails.Commands.Request;
using ApplicationUserDetails.AppUserRoleDetails.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateRoleCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteRoleCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllRoleQueryRequest request)
        {
            var roles = await _mediator.Send(request);

            return Ok(roles);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateRoleCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdRoleQueryRequest { Id = id };

            var role = await _mediator.Send(requestModel);

            return role != null
                ? (IActionResult)Ok(role)
                : NotFound(new { Message = "Role not found." });
        }
    }
}
