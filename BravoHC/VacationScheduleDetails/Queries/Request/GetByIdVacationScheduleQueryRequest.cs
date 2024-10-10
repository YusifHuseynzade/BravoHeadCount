using MediatR;
using VacationScheduleDetails.Queries.Response;

namespace VacationScheduleDetails.Queries.Request;

public class GetByIdVacationScheduleQueryRequest : IRequest<GetByIdVacationScheduleQueryResponse>
{
    public int Id { get; set; }
}
