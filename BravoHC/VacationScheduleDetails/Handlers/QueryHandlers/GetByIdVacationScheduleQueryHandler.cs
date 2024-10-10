using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VacationScheduleDetails.Queries.Request;
using VacationScheduleDetails.Queries.Response;

namespace VacationScheduleDetails.Handlers.QueryHandlers
{
    public class GetByIdVacationScheduleQueryHandler : IRequestHandler<GetByIdVacationScheduleQueryRequest, GetByIdVacationScheduleQueryResponse>
    {
        private readonly IVacationScheduleRepository _repository;
        private readonly IMapper _mapper;

        public GetByIdVacationScheduleQueryHandler(IVacationScheduleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetByIdVacationScheduleQueryResponse> Handle(GetByIdVacationScheduleQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Include parametrelerini hazırla
                var includes = new string[]
                {
                    "Employee",
                    "Employee.Position",
                    "Employee.Section"
                };

                // GetAll metodunu include parametreleri ile çağır
                var vacationSchedule = await _repository.GetAll(x => x.Id == request.Id, includes)
                                                 .FirstOrDefaultAsync(cancellationToken);

                if (vacationSchedule != null)
                {
                    var response = _mapper.Map<GetByIdVacationScheduleQueryResponse>(vacationSchedule);
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
