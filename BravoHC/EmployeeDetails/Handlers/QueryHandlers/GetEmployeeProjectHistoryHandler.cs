using AutoMapper;
using Domain.IRepositories;
using EmployeeDetails.Queries.Request;
using EmployeeDetails.Queries.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDetails.Handlers.QueryHandlers
{
    public class GetEmployeeProjectHistoryHandler : IRequestHandler<GetEmployeeProjectHistoryQueryRequest, List<GetEmployeeHistoryListResponse>>
    {
        private readonly IHeadCountHistoryRepository _repository;
        private readonly IMapper _mapper;

        public GetEmployeeProjectHistoryHandler(IHeadCountHistoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetEmployeeHistoryListResponse>> Handle(GetEmployeeProjectHistoryQueryRequest request, CancellationToken cancellationToken)
        {
            var projectHistoriesQuery = _repository.GetAll(x => x.EmployeeId == request.EmployeeId).Include(h => h.Employee).Include(h => h.FromProject)
            .Include(h => h.ToProject);

            var projectHistories = projectHistoriesQuery.ToList();
            var response = _mapper.Map<List<GetEmployeeProjectHistoryResponse>>(projectHistories);

            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = projectHistories.Count;

            return new List<GetEmployeeHistoryListResponse>
            {
                new GetEmployeeHistoryListResponse
                {
                    TotalEmployeeHistoryCount = totalCount,
                    EmployeeHistories = response
                }
            };
        }
    }
}
