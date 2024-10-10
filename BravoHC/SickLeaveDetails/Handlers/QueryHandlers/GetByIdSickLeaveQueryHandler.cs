using AutoMapper;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SickLeaveDetails.Queries.Request;
using SickLeaveDetails.Queries.Response;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SickLeaveDetails.Handlers.QueryHandlers
{
    public class GetByIdSickLeaveQueryHandler : IRequestHandler<GetByIdSickLeaveQueryRequest, GetByIdSickLeaveQueryResponse>
    {
        private readonly ISickLeaveRepository _repository;
        private readonly IMapper _mapper;

        public GetByIdSickLeaveQueryHandler(ISickLeaveRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetByIdSickLeaveQueryResponse> Handle(GetByIdSickLeaveQueryRequest request, CancellationToken cancellationToken)
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
                var sickLeave = await _repository.GetAll(x => x.Id == request.Id, includes)
                                                 .FirstOrDefaultAsync(cancellationToken);

                if (sickLeave != null)
                {
                    var response = _mapper.Map<GetByIdSickLeaveQueryResponse>(sickLeave);
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
