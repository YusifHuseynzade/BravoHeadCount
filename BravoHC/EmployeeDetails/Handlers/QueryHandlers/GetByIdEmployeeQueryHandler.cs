using AutoMapper;
using Domain.IRepositories;
using EmployeeDetails.Queries.Request;
using EmployeeDetails.Queries.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeDetails.Handlers.QueryHandlers
{
    public class GetByIdEmployeeQueryHandler : IRequestHandler<GetByIdEmployeeQueryRequest, GetByIdEmployeeQueryResponse>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public GetByIdEmployeeQueryHandler(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetByIdEmployeeQueryResponse> Handle(GetByIdEmployeeQueryRequest request, CancellationToken cancellationToken)
        {
            // Include işlemi ile ilişkili entity'leri de sorguya dahil ediyoruz
            var employees = await _repository.GetAll(x => x.Id == request.Id)
                .Include(x => x.ResidentalArea)
                .Include(x => x.BakuDistrict)
                .Include(x => x.BakuMetro)
                .Include(x => x.BakuTarget)
                .Include(x => x.Project)
                .Include(x => x.Position)
                .Include(x => x.Section)
                .Include(x => x.SubSection)
                .FirstOrDefaultAsync(cancellationToken);

            if (employees != null)
            {
                var response = _mapper.Map<GetByIdEmployeeQueryResponse>(employees);
                return response;
            }

            return new GetByIdEmployeeQueryResponse();
        }
    }
}
