using Domain.IRepositories;
using EndOfMonthReportDetails.Commands.Request;
using EndOfMonthReportDetails.Commands.Response;
using MediatR;

namespace EndOfMonthReportDetails.Handlers.CommandHandlers;


public class CreateEndOfMonthReportCommandHandler : IRequestHandler<CreateEndOfMonthReportCommandRequest, CreateEndOfMonthReportCommandResponse>
{
    private readonly IEndOfMonthReportRepository _repository;

    public CreateEndOfMonthReportCommandHandler(IEndOfMonthReportRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateEndOfMonthReportCommandResponse> Handle(CreateEndOfMonthReportCommandRequest request, CancellationToken cancellationToken)
    {

    }
}


