using Core.Helpers;
using Domain.IRepositories;
using EmployeeDetails.Commands.Request;
using EmployeeDetails.Commands.Response;
using MediatR;

namespace EmployeeDetails.Handlers.CommandHandlers
{
    public class UpdateRecruiterCommentCommandHandler : IRequestHandler<UpdateRecruiterCommentCommandRequest, UpdateRecruiterCommentCommandResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateRecruiterCommentCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<UpdateRecruiterCommentCommandResponse> Handle(UpdateRecruiterCommentCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.EmployeeId <= 0)
                    throw new BadRequestException("EmployeeId is required and must be greater than 0.");

                var employee = await _employeeRepository.GetAsync(d => d.Id == request.EmployeeId);
                if (employee == null)
                    throw new BadRequestException($"Employee with ID {request.EmployeeId} does not exist.");

                // RecruiterComment güncelleniyor
                employee.RecruiterComment = request.RecruiterComment;

                await _employeeRepository.UpdateAsync(employee);
                await _employeeRepository.CommitAsync();

                return new UpdateRecruiterCommentCommandResponse
                {
                    IsSuccess = true,
                    Message = "Recruiter comment updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new UpdateRecruiterCommentCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Error occurred while updating recruiter comment: {ex.Message}"
                };
            }
        }
    }
}
