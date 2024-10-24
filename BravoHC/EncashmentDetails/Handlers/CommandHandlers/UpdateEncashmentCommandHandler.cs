using Domain.IRepositories;
using EncashmentDetails.Commands.Request;
using EncashmentDetails.Commands.Response;
using MediatR;

namespace EncashmentDetails.Handlers.CommandHandlers;

public class UpdateEncashmentCommandHandler : IRequestHandler<UpdateEncashmentCommandRequest, UpdateEncashmentCommandResponse>
{
    private readonly IEncashmentRepository _encashmentRepository;

    public UpdateEncashmentCommandHandler(IEncashmentRepository encashmentRepository)
    {
        _encashmentRepository = encashmentRepository;
    }

    public async Task<UpdateEncashmentCommandResponse> Handle(UpdateEncashmentCommandRequest request, CancellationToken cancellationToken)
    {
       
    }

}
