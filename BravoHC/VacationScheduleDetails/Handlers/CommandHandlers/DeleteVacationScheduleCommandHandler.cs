using Domain.IRepositories;
using MediatR;
using VacationScheduleDetails.Commands.Request;
using VacationScheduleDetails.Commands.Response;

namespace VacationScheduleDetails.Handlers.CommandHandlers;

internal class DeleteVacationScheduleCommandHandler : IRequestHandler<DeleteVacationScheduleCommandRequest, DeleteVacationScheduleCommandResponse>
{
    private readonly IVacationScheduleRepository _repository;

    public DeleteVacationScheduleCommandHandler(IVacationScheduleRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteVacationScheduleCommandResponse> Handle(DeleteVacationScheduleCommandRequest request, CancellationToken cancellationToken)
    {
        var vacationSchedule = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (vacationSchedule == null)
        {
            return new DeleteVacationScheduleCommandResponse { IsSuccess = false };
        }

        _repository.Remove(vacationSchedule);
        await _repository.CommitAsync();

        return new DeleteVacationScheduleCommandResponse
        {
            IsSuccess = true
        };
    }
}
