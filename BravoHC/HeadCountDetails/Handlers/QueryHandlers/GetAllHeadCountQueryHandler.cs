﻿using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using HeadCountDetails.Queries.Request;
using HeadCountDetails.Queries.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HeadCountDetails.Handlers.QueryHandlers
{
    public class GetAllHeadCountQueryHandler : IRequestHandler<GetAllHeadCountQueryRequest, List<GetHeadCountListResponse>>
    {
        private readonly IHeadCountRepository _repository;
        private readonly IMapper _mapper;

        public GetAllHeadCountQueryHandler(IHeadCountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetHeadCountListResponse>> Handle(GetAllHeadCountQueryRequest request, CancellationToken cancellationToken)
        {
            var headCountsQuery = _repository.GetAll(x => true).Include(x => x.Project).ThenInclude(p => p.Stores)
            .Include(x => x.Section)                    
            .Include(x => x.SubSection)                 
            .Include(x => x.Position)                   
            .Include(x => x.Color)                      
            .Include(x => x.Employee)                  
            .ThenInclude(e => e.ResidentalArea)
            .Include(x => x.Employee)
            .ThenInclude(ra => ra.BakuDistrict)
            .Include(x => x.Employee)
            .ThenInclude(e => e.BakuMetro)
            .Include(x => x.Employee)
            .ThenInclude(e => e.BakuTarget)
            .AsQueryable();

            if (!string.IsNullOrEmpty(request.Searchbox))
            {
                var searchValue = request.Searchbox.ToLower();
                headCountsQuery = headCountsQuery.Where(x =>
                    x.Employee != null &&
                    (x.Employee.FullName.ToLower().Contains(searchValue) || x.Employee.Badge.ToLower().Contains(searchValue))
                );
            }

            headCountsQuery = request.ProjectId.HasValue
            ? headCountsQuery.Where(x => x.ProjectId == request.ProjectId.Value)
            : headCountsQuery;

            // SectionId filtreleme
            headCountsQuery = (request.SectionIds != null && request.SectionIds.Any())
                ? headCountsQuery.Where(x => request.SectionIds.Contains(x.SectionId ?? 0))
                : headCountsQuery;

           headCountsQuery = request.PositionId.HasValue
            ? headCountsQuery.Where(x => x.PositionId == request.PositionId.Value)
            : headCountsQuery;

            if (request.IsVacant.HasValue)
            {
                headCountsQuery = headCountsQuery.Where(x => x.IsVacant == request.IsVacant.Value);
            }

            // ExcessStaff filtreleme (boolean, sarı renk için)
            if (request.ExcessStaff.HasValue && request.ExcessStaff.Value)
            {
                headCountsQuery = headCountsQuery
                    .Where(x => x.Project.Stores.Any(store => x.HCNumber > store.HeadCountNumber));
            }

            headCountsQuery = request.OrderBy?.ToLower() switch
            {
                "asc" => headCountsQuery.OrderBy(x => x.HCNumber),
                "desc" => headCountsQuery.OrderByDescending(x => x.HCNumber),
                _ => headCountsQuery.OrderBy(x => x.HCNumber), 
            };

            var headCounts = headCountsQuery.ToList();

            var response = _mapper.Map<List<GetAllHeadCountQueryResponse>>(headCounts);

            if (request.ShowMore != null)
            {
                response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
            }

            var totalCount = headCounts.Count();

            PaginationListDto<GetAllHeadCountQueryResponse> model =
                   new PaginationListDto<GetAllHeadCountQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

            return new List<GetHeadCountListResponse>
        {
            new GetHeadCountListResponse
        {
            TotalHeadCount = totalCount,
            HeadCounts = model.Items
        }
    };
        }

    }
}
