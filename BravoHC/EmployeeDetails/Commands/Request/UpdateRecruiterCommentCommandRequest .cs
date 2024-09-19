using EmployeeDetails.Commands.Response;
using MediatR;

namespace EmployeeDetails.Commands.Request
{
    public class UpdateRecruiterCommentCommandRequest : IRequest<UpdateRecruiterCommentCommandResponse>
    {
        public int EmployeeId { get; set; }
        public string? RecruiterComment { get; set; }
    }
}
