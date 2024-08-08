//using Domain.IRepositories;
//using MediatR;
//using ScheduledDataDetails.Commands.Request;
//using ScheduledDataDetails.Commands.Response;

//namespace ScheduledDataDetails.Handlers.CommandHandlers
//{
//    public class UpdateScheduledDataCommandHandler : IRequestHandler<UpdateScheduledDataCommandRequest, UpdateScheduledDataCommandResponse>
//    {
//        private readonly IEmployeeRepository _employeeRepository;
//        private readonly IProjectRepository _projectRepository;

//        public UpdateScheduledDataCommandHandler(IEmployeeRepository employeeRepository, IProjectRepository projectRepository)
//        {
//            _employeeRepository = employeeRepository;
//            _projectRepository = projectRepository;
//        }

//        public async Task<UpdateScheduledDataCommandResponse> Handle(UpdateScheduledDataCommandRequest request, CancellationToken cancellationToken)
//        {

//        }
//    }
//}
