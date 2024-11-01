//using BGSStockRequestDetails.Commands.Request;
//using BGSStockRequestDetails.Commands.Response;
//using Domain.IRepositories;
//using MediatR;
//using Microsoft.AspNetCore.Http;

//namespace BGSStockRequestDetails.Handlers.CommandHandlers
//{
//    public class CreateBGSStockRequestCommandHandler : IRequestHandler<CreateBGSStockRequestCommandRequest, CreateBGSStockRequestCommandResponse>
//    {
//        private readonly IBGSStockRequestRepository _BGSStockRequestRepository;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public CreateBGSStockRequestCommandHandler(
//            IBGSStockRequestRepository BGSStockRequestRepository,
//            IHttpContextAccessor httpContextAccessor)
//        {
//            _BGSStockRequestRepository = BGSStockRequestRepository;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task<CreateBGSStockRequestCommandResponse> Handle(CreateBGSStockRequestCommandRequest request, CancellationToken cancellationToken)
//        {

//        }
//    }
//}
