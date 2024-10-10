using Common.Constants;
using MediatR;
using VacationScheduleDetails.Queries.Response;

namespace VacationScheduleDetails.Queries.Request;

public class GetAllVacationScheduleQueryRequest : IRequest<List<GetAllVacationScheduleListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}
