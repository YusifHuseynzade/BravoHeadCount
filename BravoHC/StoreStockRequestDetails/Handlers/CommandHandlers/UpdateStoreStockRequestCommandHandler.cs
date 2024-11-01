//using Domain.IRepositories;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using StoreStockRequestDetails.Commands.Request;
//using StoreStockRequestDetails.Commands.Response;

//namespace StoreStockRequestDetails.Handlers.CommandHandlers
//{
//    public class UpdateStoreStockRequestCommandHandler : IRequestHandler<UpdateStoreStockRequestCommandRequest, UpdateStoreStockRequestCommandResponse>
//    {
//        private readonly IStoreStockRequestRepository _StoreStockRequestRepository;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public UpdateStoreStockRequestCommandHandler(
//            IStoreStockRequestRepository StoreStockRequestRepository,
//            IHttpContextAccessor httpContextAccessor)
//        {
//            _StoreStockRequestRepository = StoreStockRequestRepository;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task<UpdateStoreStockRequestCommandResponse> Handle(UpdateStoreStockRequestCommandRequest request, CancellationToken cancellationToken)
//        {

//        }
//    }
//}
