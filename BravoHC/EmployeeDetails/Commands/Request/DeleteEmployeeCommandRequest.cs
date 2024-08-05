using EmployeeDetails.Commands.Response;
using MediatR;

namespace EmployeeDetails.Commands.Request;

public class DeleteEmployeeCommandRequest : IRequest<DeleteEmployeeCommandResponse>
{
    public int Id { get; set; }
}
