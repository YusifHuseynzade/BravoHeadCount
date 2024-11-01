//using Domain.Entities;
//using Domain.IRepositories;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using System.Security.Claims;
//using UniformDetails.Commands.Request;
//using UniformDetails.Commands.Response;

//namespace UniformDetails.Handlers.CommandHandlers
//{
//    public class UpdateUniformCommandHandler : IRequestHandler<UpdateUniformCommandRequest, UpdateUniformCommandResponse>
//    {
//        private readonly IUniformRepository _uniformRepository;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public UpdateUniformCommandHandler(
//            IUniformRepository uniformRepository,
//            IHttpContextAccessor httpContextAccessor)
//        {
//            _uniformRepository = uniformRepository;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task<UpdateUniformCommandResponse> Handle(UpdateUniformCommandRequest request, CancellationToken cancellationToken)
//        {
           
//        }
//    }
//}
