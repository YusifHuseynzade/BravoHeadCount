using AutoMapper;
using DepartmentDetails.Queries.Request;
using DepartmentDetails.Queries.Response;
using Domain.IRepositories;
using MediatR;

namespace DepartmentDetails.Queries.GetById;


	public class GetByIdDepartmentQueryHandler : IRequestHandler<GetByIdDepartmentQueryRequest, GetByIdDepartmentQueryResponse>
	{
		private readonly IDepartmentRepository _repository;
		private readonly IMapper _mapper;

		public GetByIdDepartmentQueryHandler(IDepartmentRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<GetByIdDepartmentQueryResponse> Handle(GetByIdDepartmentQueryRequest request, CancellationToken cancellationToken)
		{
			var Departments = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

			if (Departments != null)
			{
				var response = _mapper.Map<GetByIdDepartmentQueryResponse>(Departments);
				return response;
			}

			return new GetByIdDepartmentQueryResponse();

		}
	}
