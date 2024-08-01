using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using FunctionalAreaDetails.Queries.Request;
using FunctionalAreaDetails.Queries.Response;
using MediatR;

namespace FunctionalAreaDetails.Handlers.QueryHandlers;

public class GetFunctionalAreaDepartmentsQueryHandler : IRequestHandler<GetFunctionalAreaDepartmentsQueryRequest, List<GetFunctionalAreaDepartmentsQueryResponse>>
{
    private readonly IFunctionalAreaRepository _repository;
    private readonly IMapper _mapper;

    public GetFunctionalAreaDepartmentsQueryHandler(IFunctionalAreaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetFunctionalAreaDepartmentsQueryResponse>> Handle(GetFunctionalAreaDepartmentsQueryRequest request, CancellationToken cancellationToken)
    {
        var functionalArea = await _repository.FirstOrDefaultAsync(x => x.Id == request.FunctionalAreaId, "Departments");

        if (functionalArea == null)
        {

            return null;
        }

        var departments = functionalArea.Departments;
        var departmentresponse = _mapper.Map<List<GetFunctionalAreaDepartmentsQueryResponse>>(departments);

        if (request.ShowMore != null)
        {
            departmentresponse = departmentresponse.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
        }

        PaginationListDto<GetFunctionalAreaDepartmentsQueryResponse> model =
               new PaginationListDto<GetFunctionalAreaDepartmentsQueryResponse>(departmentresponse, request.Page, request.ShowMore?.Take ?? departmentresponse.Count, departmentresponse.Count());

        return model.Items;
    }
}

