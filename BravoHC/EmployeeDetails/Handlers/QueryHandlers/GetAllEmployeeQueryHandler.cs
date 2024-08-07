﻿using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using EmployeeDetails.Queries.Request;
using EmployeeDetails.Queries.Response;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeDetails.Handlers.QueryHandlers
{
    public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQueryRequest, List<GetEmployeeListResponse>>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public GetAllEmployeeQueryHandler(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetEmployeeListResponse>> Handle(GetAllEmployeeQueryRequest request, CancellationToken cancellationToken)
        {
            var employees = _repository.GetAll(x => true);

            var response = _mapper.Map<List<GetAllEmployeeQueryResponse>>(employees);
            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = employees.Count();

            PaginationListDto<GetAllEmployeeQueryResponse> model =
                   new PaginationListDto<GetAllEmployeeQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetEmployeeListResponse>
            {
                new GetEmployeeListResponse
                {
                    TotalEmployeeCount = totalCount,
                    Employees = model.Items
                }
            };
        }
    }
}
