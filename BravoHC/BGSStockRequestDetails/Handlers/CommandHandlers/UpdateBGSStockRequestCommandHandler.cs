//using BGSStockRequestDetails.Commands.Request;
//using BGSStockRequestDetails.Commands.Response;
//using Domain.Entities;
//using Domain.IRepositories;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using System.Security.Claims;

//namespace BGSStockRequestDetails.Handlers.CommandHandlers
//{
//    public class UpdateBGSStockRequestCommandHandler : IRequestHandler<UpdateBGSStockRequestCommandRequest, UpdateBGSStockRequestCommandResponse>
//    {
//        private readonly IBGSStockRequestRepository _BGSStockRequestRepository;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public UpdateBGSStockRequestCommandHandler(
//            IBGSStockRequestRepository BGSStockRequestRepository,
//            IHttpContextAccessor httpContextAccessor)
//        {
//            _BGSStockRequestRepository = BGSStockRequestRepository;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task<UpdateBGSStockRequestCommandResponse> Handle(UpdateBGSStockRequestCommandRequest request, CancellationToken cancellationToken)
//        {
         
//        }
//    }
//}
