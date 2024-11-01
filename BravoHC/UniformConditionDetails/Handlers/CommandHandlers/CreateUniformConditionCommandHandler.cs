//using Domain.Entities;
//using Domain.IRepositories;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using System.Security.Claims;
//using UniformConditionDetails.Commands.Request;
//using UniformConditionDetails.Commands.Response;

//namespace UniformConditionDetails.Handlers.CommandHandlers
//{
//    public class CreateUniformConditionCommandHandler : IRequestHandler<CreateUniformConditionCommandRequest, CreateUniformConditionCommandResponse>
//    {
//        private readonly IUniformConditionRepository _uniformConditionRepository;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public CreateUniformConditionCommandHandler(
//            IUniformConditionRepository uniformConditionRepository,
//            IHttpContextAccessor httpContextAccessor)
//        {
//            _uniformConditionRepository = uniformConditionRepository;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public async Task<CreateUniformConditionCommandResponse> Handle(CreateUniformConditionCommandRequest request, CancellationToken cancellationToken)
//        {
           
//        }
//    }
//}
