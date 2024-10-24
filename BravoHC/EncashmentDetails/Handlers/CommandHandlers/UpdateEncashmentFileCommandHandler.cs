using Core.Helpers;
using Domain.IRepositories;
using EncashmentDetails.Commands.Request;
using EncashmentDetails.Commands.Response;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace EncashmentDetails.Handlers.CommandHandlers
{
    public class UpdateEncashmentFileCommandHandler : IRequestHandler<UpdateEncashmentFileCommandRequest, UpdateEncashmentFileCommandResponse>
    {
        private readonly IEncashmentRepository _encashmentRepository;
        private readonly IHostEnvironment _env;
        private readonly IOptions<FileSettings> _settings;

        public UpdateEncashmentFileCommandHandler(
            IEncashmentRepository encashmentRepository,
            IHostEnvironment env,
            IOptions<FileSettings> settings)
        {
            _encashmentRepository = encashmentRepository;
            _env = env;
            _settings = settings;
        }

        public async Task<UpdateEncashmentFileCommandResponse> Handle(UpdateEncashmentFileCommandRequest request, CancellationToken cancellationToken)
        {
           
        }
    }
}
