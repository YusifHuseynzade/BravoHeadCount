using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SummaryDetails.Queries.Request;
using SummaryDetails.Queries.Response;

namespace SummaryDetails.Handlers.QueryHandlers
{
    public class GetByIdSummaryQueryHandler : IRequestHandler<GetByIdSummaryQueryRequest, GetByIdSummaryQueryResponse>
    {
        private readonly ISummaryRepository _repository;
        private readonly IMapper _mapper;

        public GetByIdSummaryQueryHandler(ISummaryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetByIdSummaryQueryResponse> Handle(GetByIdSummaryQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Include parametrelerini hazırla
                var includes = new string[]
                {
                    "Month",
                    "Employee",
                    "Employee.Position",
                    "Employee.Section"
                };

                // GetAll metodunu include parametreleri ile çağır
                var summary = await _repository.GetAll(x => x.Id == request.Id, includes)
                                                 .FirstOrDefaultAsync(cancellationToken);

                if (summary != null)
                {
                    var response = _mapper.Map<GetByIdSummaryQueryResponse>(summary);
                    return response;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta oldu: {ex.Message}");
                throw;
            }
        }
    }
}
