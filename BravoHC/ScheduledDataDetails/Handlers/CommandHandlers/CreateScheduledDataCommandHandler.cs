//using Domain.IRepositories;
//using EmployeeDetails.Commands.Request;
//using MediatR;
//using ScheduledDataDetails.Commands.Response;

//namespace ScheduledDataDetails.Handlers.CommandHandlers
//{
//    public class CreateScheduledDataCommandHandler : IRequestHandler<CreateScheduledDataCommandRequest, CreateScheduledDataCommandResponse>
//    {
//        private readonly IEmployeeRepository _employeeRepository;
//        private readonly IProjectRepository _projectRepository;


//        public CreateScheduledDataCommandHandler(
//            IEmployeeRepository employeeRepository, IProjectRepository projectRepository)
//        {
//            _employeeRepository = employeeRepository;
//            _projectRepository = projectRepository;
//        }

//        public async Task<CreateScheduledDataCommandResponse> Handle(CreateScheduledDataCommandRequest request, CancellationToken cancellationToken)
//        {

//        }
//    }
//}
