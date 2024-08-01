using AutoMapper;
using Domain.IRepositories;
using MediatR;
using PositionDetails.Queries.Request;
using PositionDetails.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionDetails.Handlers.QueryHandlers;

public class GetByIdPositionQueryHandler : IRequestHandler<GetByIdPositionQueryRequest, GetByIdPositionQueryResponse>
{
	private readonly IPositionRepository _repository;
	private readonly IMapper _mapper;

	public GetByIdPositionQueryHandler(IPositionRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<GetByIdPositionQueryResponse> Handle(GetByIdPositionQueryRequest request, CancellationToken cancellationToken)
	{
		try
		{
			var position = await _repository.GetAsync(x => x.Id == request.Id);

			if (position != null)
			{
				var response = _mapper.Map<GetByIdPositionQueryResponse>(position);

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
