//using Domain.Entities;
//using Domain.IRepositories;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using System.Security.Claims;
//using TransactionPageDetails.Commands.Request;
//using TransactionPageDetails.Commands.Response;

//namespace TransactionPageDetails.Handlers.CommandHandlers
//{
//    public class CreateTransactionPageCommandHandler : IRequestHandler<CreateTransactionPageCommandRequest, CreateTransactionPageCommandResponse>
//    {
//        private readonly ITransactionPageRepository _transactionPageRepository;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public CreateTransactionPageCommandHandler(
//            ITransactionPageRepository transactionPageRepository,
//            IHttpContextAccessor httpContextAccessor)
//        {
//            _transactionPageRepository = transactionPageRepository;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task<CreateTransactionPageCommandResponse> Handle(CreateTransactionPageCommandRequest request, CancellationToken cancellationToken)
//        {
          
//        }
//    }
//}
