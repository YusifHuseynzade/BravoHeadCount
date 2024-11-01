//using Domain.Entities;
//using Domain.IRepositories;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using System.Security.Claims;
//using UniformDetails.Commands.Request;
//using UniformDetails.Commands.Response;

//namespace UniformDetails.Handlers.CommandHandlers
//{
//    public class CreateUniformCommandHandler : IRequestHandler<CreateUniformCommandRequest, CreateUniformCommandResponse>
//    {
//        private readonly IUniformRepository _uniformRepository;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public CreateUniformCommandHandler(
//            IUniformRepository uniformRepository,
//            IHttpContextAccessor httpContextAccessor)
//        {
//            _uniformRepository = uniformRepository;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task<CreateUniformCommandResponse> Handle(CreateUniformCommandRequest request, CancellationToken cancellationToken)
//        {
           
//        }
//    }
//}
