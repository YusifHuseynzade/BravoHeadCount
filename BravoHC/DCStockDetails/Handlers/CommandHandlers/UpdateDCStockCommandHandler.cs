//using DCStockDetails.Commands.Request;
//using DCStockDetails.Commands.Response;
//using Domain.Entities;
//using Domain.IRepositories;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using System.Security.Claims;

//namespace DCStockDetails.Handlers.CommandHandlers
//{
//    public class UpdateDCStockCommandHandler : IRequestHandler<UpdateDCStockCommandRequest, UpdateDCStockCommandResponse>
//    {
//        private readonly IDCStockRepository _DCStockRepository;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public UpdateDCStockCommandHandler(
//            IDCStockRepository DCStockRepository,
//            IHttpContextAccessor httpContextAccessor)
//        {
//            _DCStockRepository = DCStockRepository;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task<UpdateDCStockCommandResponse> Handle(UpdateDCStockCommandRequest request, CancellationToken cancellationToken)
//        {
            
//        }
//    }
//}
