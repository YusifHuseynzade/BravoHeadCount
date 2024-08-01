using Domain.IRepositories;
using MediatR;
using SubSectionDetails.Commands.Request;
using SubSectionDetails.Commands.Response;

namespace SubSectionDetails.Handlers.CommandHandlers;


public class DeleteSubSectionCommandHandler : IRequestHandler<DeleteSubSectionCommandRequest, DeleteSubSectionCommandResponse>
{
    private readonly ISubSectionRepository _repository;

    public DeleteSubSectionCommandHandler(ISubSectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteSubSectionCommandResponse> Handle(DeleteSubSectionCommandRequest request, CancellationToken cancellationToken)
    {
        var subSection = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (subSection == null)
        {
            return new DeleteSubSectionCommandResponse { IsSuccess = false };
        }

        _repository.Remove(subSection);
        await _repository.CommitAsync();

        return new DeleteSubSectionCommandResponse
        {
            IsSuccess = true
        };
    }
}