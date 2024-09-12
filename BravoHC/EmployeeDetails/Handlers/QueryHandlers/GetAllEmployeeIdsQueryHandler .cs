using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.IRepositories;
using EmployeeDetails.Queries.Request;

namespace EmployeeDetails.Queries.Handlers
{
    public class GetAllEmployeeIdsQueryHandler : IRequestHandler<GetAllEmployeeIdsQueryRequest, List<int>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeeIdsQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<int>> Handle(GetAllEmployeeIdsQueryRequest request, CancellationToken cancellationToken)
        {
            var employeeIds = await _employeeRepository.GetAllEmployeeIdsAsync();
            return employeeIds;
        }
    }
}
