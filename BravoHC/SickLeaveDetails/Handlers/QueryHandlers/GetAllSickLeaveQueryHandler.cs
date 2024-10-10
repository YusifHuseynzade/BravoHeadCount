using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SickLeaveDetails.Queries.Request;
using SickLeaveDetails.Queries.Response;

namespace SickLeaveDetails.Handlers.QueryHandlers
{
    public class GetAllSickLeaveQueryHandler : IRequestHandler<GetAllSickLeaveQueryRequest, List<GetAllSickLeaveListQueryResponse>>
    {
        private readonly ISickLeaveRepository _repository;
        private readonly IMapper _mapper;

        public GetAllSickLeaveQueryHandler(ISickLeaveRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllSickLeaveListQueryResponse>> Handle(GetAllSickLeaveQueryRequest request, CancellationToken cancellationToken)
        {
            var sickLeaves = _repository.GetAll(x => true).Include(s => s.Employee)
                .ThenInclude(e => e.Position)
                .Include(s => s.Employee)
                .ThenInclude(e => e.Section);
               

            if (sickLeaves != null)
            {
                var response = _mapper.Map<List<GetAllSickLeaveQueryResponse>>(sickLeaves);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = sickLeaves.Count();

                PaginationListDto<GetAllSickLeaveQueryResponse> model =
                       new PaginationListDto<GetAllSickLeaveQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllSickLeaveListQueryResponse>
                {
                    new GetAllSickLeaveListQueryResponse
                    {
                        TotalSickLeaveCount = totalCount,
                        SickLeaves = model.Items
                    }
                };
            }

            return new List<GetAllSickLeaveListQueryResponse>();
        }
    }
}
