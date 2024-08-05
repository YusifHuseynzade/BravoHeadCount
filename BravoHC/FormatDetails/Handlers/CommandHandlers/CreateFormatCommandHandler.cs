using Domain.Entities;
using Domain.IRepositories;
using FormatDetails.Commands.Request;
using FormatDetails.Commands.Response;
using MediatR;

namespace FormatDetails.Handlers.CommandHandlers;

public class CreateFormatCommandHandler : IRequestHandler<CreateFormatCommandRequest, CreateFormatCommandResponse>
{

    private readonly IFormatRepository _repository;

    public CreateFormatCommandHandler(IFormatRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateFormatCommandResponse> Handle(CreateFormatCommandRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.IsExistAsync(f => f.Name == request.Name))
        {
            return new CreateFormatCommandResponse
            {
                IsSuccess = false,
            };
        }
        var format = new Format();
        format.SetDetail(request.Name);

        await _repository.AddAsync(format);
        await _repository.CommitAsync();
        return new CreateFormatCommandResponse
        {
            IsSuccess = true,
        };
    }
}
