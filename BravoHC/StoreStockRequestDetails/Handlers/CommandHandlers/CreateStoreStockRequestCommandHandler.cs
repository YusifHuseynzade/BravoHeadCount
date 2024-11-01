//using Domain.IRepositories;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using StoreStockRequestDetails.Commands.Request;
//using StoreStockRequestDetails.Commands.Response;

//namespace StoreStockRequestDetails.Handlers.CommandHandlers
//{
//    public class CreateStoreStockRequestCommandHandler : IRequestHandler<CreateStoreStockRequestCommandRequest, CreateStoreStockRequestCommandResponse>
//    {
//        private readonly IStoreStockRequestRepository _StoreStockRequestRepository;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public CreateStoreStockRequestCommandHandler(
//            IStoreStockRequestRepository StoreStockRequestRepository,
//            IHttpContextAccessor httpContextAccessor)
//        {
//            _StoreStockRequestRepository = StoreStockRequestRepository;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task<CreateStoreStockRequestCommandResponse> Handle(CreateStoreStockRequestCommandRequest request, CancellationToken cancellationToken)
//        {

//        }
//    }
//}
