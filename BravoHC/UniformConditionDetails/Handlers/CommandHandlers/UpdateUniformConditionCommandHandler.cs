//using Domain.Entities;
//using Domain.IRepositories;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using System.Security.Claims;
//using UniformConditionDetails.Commands.Request;
//using UniformConditionDetails.Commands.Response;

//namespace UniformConditionDetails.Handlers.CommandHandlers
//{
//    public class UpdateUniformConditionCommandHandler : IRequestHandler<UpdateUniformConditionCommandRequest, UpdateUniformConditionCommandResponse>
//    {
//        private readonly IUniformConditionRepository _uniformConditionRepository;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public UpdateUniformConditionCommandHandler(
//            IUniformConditionRepository uniformConditionRepository,
//            IHttpContextAccessor httpContextAccessor)
//        {
//            _uniformConditionRepository = uniformConditionRepository;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task<UpdateUniformConditionCommandResponse> Handle(UpdateUniformConditionCommandRequest request, CancellationToken cancellationToken)
//        {
           
//        }
//    }
//}
