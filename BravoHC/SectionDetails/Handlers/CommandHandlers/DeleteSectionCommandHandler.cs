using Domain.IRepositories;
using MediatR;
using SectionDetails.Commands.Request;
using SectionDetails.Commands.Response;

namespace SectionDetails.Handlers.CommandHandlers;

public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommandRequest, DeleteSectionCommandResponse>
{
    private readonly ISectionRepository _repository;

    public DeleteSectionCommandHandler(ISectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteSectionCommandResponse> Handle(DeleteSectionCommandRequest request, CancellationToken cancellationToken)
    {
        var section = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (section == null)
        {
            return new DeleteSectionCommandResponse { IsSuccess = false };
        }

        _repository.Remove(section);
        await _repository.CommitAsync();

        return new DeleteSectionCommandResponse
        {
            IsSuccess = true
        };
    }
}
