using Domain.IRepositories;
using EndOfMonthReportDetails.Commands.Request;
using EndOfMonthReportDetails.Commands.Response;
using MediatR;

namespace EndOfMonthReportDetails.Handlers.CommandHandlers;

public class UpdateEndOfMonthReportCommandHandler : IRequestHandler<UpdateEndOfMonthReportCommandRequest, UpdateEndOfMonthReportCommandResponse>
{
    private readonly IEndOfMonthReportRepository _endOfMonthReportRepository;

    public UpdateEndOfMonthReportCommandHandler(IEndOfMonthReportRepository endOfMonthReportRepository)
    {
        _endOfMonthReportRepository = endOfMonthReportRepository;
    }

    public async Task<UpdateEndOfMonthReportCommandResponse> Handle(UpdateEndOfMonthReportCommandRequest request, CancellationToken cancellationToken)
    {

    }

}
