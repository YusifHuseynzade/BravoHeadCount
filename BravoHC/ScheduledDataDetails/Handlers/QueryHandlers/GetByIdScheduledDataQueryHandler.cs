using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScheduledDataDetails.Queries.Request;
using ScheduledDataDetails.Queries.Response;

namespace ScheduledDataDetails.Handlers.QueryHandlers;

public class GetByIdScheduledDataQueryHandler : IRequestHandler<GetByIdScheduledDataQueryRequest, GetByIdScheduledDataQueryResponse>
{
    private readonly IScheduledDataRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdScheduledDataQueryHandler(IScheduledDataRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetByIdScheduledDataQueryResponse> Handle(GetByIdScheduledDataQueryRequest request, CancellationToken cancellationToken)
    {
        var scheduledData = await _repository.GetAll(x => x.Id == request.Id)
               .Include(sd => sd.Plan) // Plan ile ilişki
               .Include(sd => sd.Fact)
               .Include(sd => sd.Employee) // Employee ile ilişki
                   .ThenInclude(e => e.Position) // Employee ve Position ile ilişki
               .Include(sd => sd.Employee) // Employee ile ilişki
                   .ThenInclude(e => e.Section)
               .Include(sd => sd.Employee) // Employee ile ilişki
                   .ThenInclude(e => e.EmployeeBalances)
               .Include(sd => sd.Project) // Project ile ilişki
               .FirstOrDefaultAsync(cancellationToken);

        if (scheduledData != null)
        {
            var response = _mapper.Map<GetByIdScheduledDataQueryResponse>(scheduledData);
            return response;
        }

        return new GetByIdScheduledDataQueryResponse();

    }
}