using EmployeeDetails.Queries.Response;
using MediatR;

namespace EmployeeDetails.Queries.Request;

public class GetByIdEmployeeQueryRequest : IRequest<GetByIdEmployeeQueryResponse>
{
    public int Id { get; set; }
}
