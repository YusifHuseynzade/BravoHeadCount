using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TrolleyDetails.Commands.Request;
using TrolleyDetails.Commands.Response;

namespace TrolleyDetails.Handlers.CommandHandlers
{
    public class UpdateTrolleyCommandHandler : IRequestHandler<UpdateTrolleyCommandRequest, UpdateTrolleyCommandResponse>
    {
        private readonly ITrolleyRepository _trolleyRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateTrolleyCommandHandler(
            ITrolleyRepository trolleyRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _trolleyRepository = trolleyRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateTrolleyCommandResponse> Handle(UpdateTrolleyCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new UpdateTrolleyCommandResponse();

            try
            {
                // Fetch the existing Trolley
                var trolley = await _trolleyRepository.GetAsync(
                    x => x.Id == request.Id,
                    nameof(Trolley.Project),
                    nameof(Trolley.TrolleyType));

                if (trolley == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Trolley not found.";
                    return response;
                }

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

                // Update the Trolley details
                trolley.ProjectId = request.ProjectId;
                trolley.TrolleyTypeId = request.TrolleyTypeId;
                trolley.CountDate = request.CountDate;
                trolley.WorkingTrolleysCount = request.WorkingTrolleysCount;
                trolley.BrokenTrolleysCount = request.BrokenTrolleysCount;
                trolley.ModifiedBy = fullName;
                trolley.ModifiedDate = DateTime.UtcNow;


                // Commit changes to the database
                await _trolleyRepository.UpdateAsync(trolley);
                await _trolleyRepository.CommitAsync();

                response.IsSuccess = true;
                response.Message = "Trolley updated successfully.";
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
