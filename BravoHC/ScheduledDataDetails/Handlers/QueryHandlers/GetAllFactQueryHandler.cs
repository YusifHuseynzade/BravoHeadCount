﻿using AutoMapper;
using Common.Constants;
using Domain.IRepositories;
using MediatR;
using ScheduledDataDetails.Queries.Request;
using ScheduledDataDetails.Queries.Response;

namespace ScheduledDataDetails.Handlers.QueryHandlers
{
    public class GetAllFactQueryHandler : IRequestHandler<GetAllFactQueryRequest, List<GetAllFactListQueryResponse>>
    {
        private readonly IFactRepository _repository;
        private readonly IMapper _mapper;

        public GetAllFactQueryHandler(IFactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllFactListQueryResponse>> Handle(GetAllFactQueryRequest request, CancellationToken cancellationToken)
        {
            var plans = _repository.GetAll(x => true);

            if (plans != null)
            {
                var response = _mapper.Map<List<GetAllFactQueryResponse>>(plans);

                if (request.ShowMore != null)
                {
                    response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
                }

                var totalCount = plans.Count();

                PaginationListDto<GetAllFactQueryResponse> model =
                       new PaginationListDto<GetAllFactQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

                return new List<GetAllFactListQueryResponse>
                {
                    new GetAllFactListQueryResponse
                    {
                        TotalFactCount = totalCount,
                        Facts = model.Items
                    }
                };
            }

            return new List<GetAllFactListQueryResponse>();
        }
    }
}