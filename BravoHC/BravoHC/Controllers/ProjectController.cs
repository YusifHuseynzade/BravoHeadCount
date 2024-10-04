using HeadCountDetails.ExcelImportService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectDetails.Commands.Request;
using ProjectDetails.ExcelImportService;
using ProjectDetails.Queries.Request;

namespace BravoHC.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ProjectController : ControllerBase
	{
		private readonly IMediator _mediator;
        private readonly ProjectImportService _importService;

        public ProjectController(IMediator mediator, ProjectImportService importService)
		{
			_mediator = mediator;
			_importService = importService;
		}
		[HttpPost]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Add([FromBody] CreateProjectCommandRequest request)
		{
			return Ok(await _mediator.Send(request));
		}
		[HttpDelete]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Delete([FromBody] DeleteProjectCommandRequest request)
		{
			return Ok(await _mediator.Send(request));
		}
		[HttpPut]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Update([FromBody] UpdateProjectCommandRequest request)
		{
			return Ok(await _mediator.Send(request));
		}
		[HttpGet]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProjectQueryRequest request)
		{
			var projects = await _mediator.Send(request);

			return Ok(projects);
		}
		[HttpGet("{id}")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetById(int id)
		{
			var requestModel = new GetByIdProjectQueryRequest { Id = id };
			var Project = await _mediator.Send(requestModel);

			return Project != null
				? (IActionResult)Ok(Project)
				: NotFound(new { Message = "Project not found." });
		}


        [HttpGet("GetProjectSections")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetProjectSection([FromQuery] GetProjectSectionQueryRequest request)
        {
            var section = await _mediator.Send(request);

            if (section == null)
            {
                return NotFound();
            }

            return Ok(section);
        }

        [HttpPost("importexceldata")]
        [Authorize(Roles = "Admin, Recruiter")]
        public async Task<IActionResult> Import([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Please upload a valid Excel file.");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            await _importService.ImportAsync(stream);

            return Ok("File imported successfully.");
        }

        [HttpGet("projecthistory")]
        [Authorize(Roles = "Admin, HR Staff, Recruiter, Store Management")]
        public async Task<IActionResult> GetProjectHistory([FromQuery] GetProjectHistoryQueryRequest request)
        {
            var projectHistory = await _mediator.Send(request);

            if (projectHistory == null)
            {
                return NotFound();
            }

            return Ok(projectHistory);
        }

    }
}
