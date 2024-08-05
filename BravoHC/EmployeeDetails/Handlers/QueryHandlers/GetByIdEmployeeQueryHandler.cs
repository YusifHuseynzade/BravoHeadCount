using AutoMapper;
using Domain.IRepositories;
using EmployeeDetails.Queries.Request;
using EmployeeDetails.Queries.Response;
using MediatR;

namespace EmployeeDetails.Handlers.QueryHandlers;

public class GetByIdEmployeeQueryHandler : IRequestHandler<GetByIdEmployeeQueryRequest, GetByIdEmployeeQueryResponse>
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdEmployeeQueryHandler(IEmployeeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdEmployeeQueryResponse> Handle(GetByIdEmployeeQueryRequest request, CancellationToken cancellationToken)
    {
        var employees = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (employees != null)
        {
            var response = _mapper.Map<GetByIdEmployeeQueryResponse>(employees);
            return response;
        }

        return new GetByIdEmployeeQueryResponse();

    }
}