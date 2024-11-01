//using DCStockDetails.Commands.Request;
//using DCStockDetails.Commands.Response;
//using Domain.IRepositories;
//using MediatR;
//using Microsoft.AspNetCore.Http;

//namespace DCStockDetails.Handlers.CommandHandlers
//{
//    public class CreateDCStockCommandHandler : IRequestHandler<CreateDCStockCommandRequest, CreateDCStockCommandResponse>
//    {
//        private readonly IDCStockRepository _DCStockRepository;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public CreateDCStockCommandHandler(
//            IDCStockRepository DCStockRepository,
//            IHttpContextAccessor httpContextAccessor)
//        {
//            _DCStockRepository = DCStockRepository;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task<CreateDCStockCommandResponse> Handle(CreateDCStockCommandRequest request, CancellationToken cancellationToken)
//        {

//        }
//    }
//}
