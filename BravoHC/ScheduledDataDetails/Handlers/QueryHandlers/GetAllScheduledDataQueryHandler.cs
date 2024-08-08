//using AutoMapper;
//using Common.Constants;
//using Domain.IRepositories;
//using MediatR;
//using ScheduledDataDetails.Queries.Request;
//using ScheduledDataDetails.Queries.Response;

//namespace ScheduledDataDetails.Handlers.QueryHandlers
//{
//    public class GetAllScheduledDataQueryHandler : IRequestHandler<GetAllScheduledDataQueryRequest, List<GetScheduledDataListResponse>>
//    {
//        private readonly IScheduledDataRepository _repository;
//        private readonly IMapper _mapper;

//        public GetAllScheduledDataQueryHandler(IScheduledDataRepository repository, IMapper mapper)
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }

//        //public async Task<List<GetScheduledDataListResponse>> Handle(GetAllScheduledDataQueryRequest request, CancellationToken cancellationToken)
//        //{
//        //    var employeesQuery = _repository.GetAll(x => true);

//        //    employeesQuery = !string.IsNullOrEmpty(request.Badge) ? employeesQuery.Where(x => x.Badge.Contains(request.Badge)) : employeesQuery;
//        //    employeesQuery = !string.IsNullOrEmpty(request.FullName) ? employeesQuery.Where(x => x.FullName.Contains(request.FullName)) : employeesQuery;

//        //    var employees = employeesQuery.ToList();

//        //    var response = _mapper.Map<List<GetAllScheduledDataQueryResponse>>(employees);
//        //    if (request.ShowMore != null)
//        //    {
//        //        response = response.Skip((request.Page - 1) * request.ShowMore.Take).Take(request.ShowMore.Take).ToList();
//        //    }

//        //    var totalCount = employees.Count();

//        //    PaginationListDto<GetAllScheduledDataQueryResponse> model =
//        //           new PaginationListDto<GetAllScheduledDataQueryResponse>(response, request.Page, request.ShowMore?.Take ?? response.Count, totalCount);

//        //    return new List<GetScheduledDataListResponse>
//        //    {
//        //        new GetScheduledDataListResponse
//        //        {
//        //            TotalEmployeeCount = totalCount,
//        //            Employees = model.Items
//        //        }
//        //    };
//        //}
//    }
//}
