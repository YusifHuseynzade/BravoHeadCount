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
        private readonly ITrolleyHistoryRepository _historyRepository;

        public UpdateTrolleyCommandHandler(
            ITrolleyRepository trolleyRepository,
            IHttpContextAccessor httpContextAccessor,
            ITrolleyHistoryRepository historyRepository)
        {
            _trolleyRepository = trolleyRepository;
            _httpContextAccessor = httpContextAccessor;
            _historyRepository = historyRepository;
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
                trolley.CountDate = request.CountDate.ToUniversalTime();
                trolley.WorkingTrolleysCount = request.WorkingTrolleysCount;
                trolley.BrokenTrolleysCount = request.BrokenTrolleysCount;
                trolley.ModifiedBy = fullName;
                trolley.ModifiedDate = DateTime.UtcNow;


                // Commit changes to the database
                await _trolleyRepository.UpdateAsync(trolley);
                await _trolleyRepository.CommitAsync();

                // Log the update in TrolleyHistory
                var history = new TrolleyHistory
                {
                    TrolleyId = trolley.Id,
                    CountDate = trolley.CountDate.ToUniversalTime(),
                    WorkingTrolleysCount = trolley.WorkingTrolleysCount,
                    BrokenTrolleysCount = trolley.BrokenTrolleysCount,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = fullName
                };

                // Save the history entry to the database
                await _historyRepository.AddAsync(history);
                await _historyRepository.CommitAsync();

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
