using BakuMetroDetails.Commands.Request;
using BakuMetroDetails.Commands.Response;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace BakuMetroDetails.Handlers.CommandHandlers;

public class CreateBakuMetroCommandHandler : IRequestHandler<CreateBakuMetroCommandRequest, CreateBakuMetroCommandResponse>
{

    private readonly IBakuMetroRepository _repository;

    public CreateBakuMetroCommandHandler(IBakuMetroRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateBakuMetroCommandResponse> Handle(CreateBakuMetroCommandRequest request, CancellationToken cancellationToken)
    {
        if (await _repository.IsExistAsync(f => f.Name == request.Name))
        {
            return new CreateBakuMetroCommandResponse
            {
                IsSuccess = false,
            };
        }
        var bakuMetro = new BakuMetro();
        bakuMetro.SetDetail(request.Name);

        await _repository.AddAsync(bakuMetro);
        await _repository.CommitAsync();
        return new CreateBakuMetroCommandResponse
        {
            IsSuccess = true,
        };
    }
}