using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TrolleyDetails.Commands.Request;
using TrolleyDetails.Commands.Response;

namespace TrolleyDetails.Handlers.CommandHandlers
{
    public class CreateTrolleyCommandHandler : IRequestHandler<CreateTrolleyCommandRequest, CreateTrolleyCommandResponse>
    {
        private readonly ITrolleyRepository _trolleyRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateTrolleyCommandHandler(
            ITrolleyRepository trolleyRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _trolleyRepository = trolleyRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateTrolleyCommandResponse> Handle(CreateTrolleyCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateTrolleyCommandResponse();

            try
            {
                // Retrieve user information from HttpContext
                var fullName = _httpContextAccessor.HttpContext?.User?.Claims
                    .FirstOrDefault(c => c.Type == "FullName")?.Value;

                var userIdString = _httpContextAccessor.HttpContext?.User?.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(userIdString))
                {
                    response.IsSuccess = false;
                    response.Message = "User information is not available.";
                    return response;
                }

                // Create a new Trolley entity
                var trolley = new Trolley
                {
                    ProjectId = request.ProjectId,
                    TrolleyTypeId = request.TrolleyTypeId,
                    CountDate = request.CountDate.ToUniversalTime(),
                    WorkingTrolleysCount = request.WorkingTrolleysCount,
                    BrokenTrolleysCount = request.BrokenTrolleysCount,
                    CreatedBy = fullName,
                    CreatedDate = DateTime.UtcNow,
                };

                // Save the new Trolley to the database
                await _trolleyRepository.AddAsync(trolley);
                await _trolleyRepository.CommitAsync();

                response.IsSuccess = true;
                response.Message = "Trolley created successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
    }
}
