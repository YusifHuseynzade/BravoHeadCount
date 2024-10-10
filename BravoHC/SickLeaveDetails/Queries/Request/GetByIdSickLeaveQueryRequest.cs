using MediatR;
using SickLeaveDetails.Queries.Response;

namespace SickLeaveDetails.Queries.Request;

public class GetByIdSickLeaveQueryRequest : IRequest<GetByIdSickLeaveQueryResponse>
{
    public int Id { get; set; }
}
