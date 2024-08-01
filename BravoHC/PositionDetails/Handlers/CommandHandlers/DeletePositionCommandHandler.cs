using Domain.IRepositories;
using MediatR;
using PositionDetails.Commands.Request;
using PositionDetails.Commands.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionDetails.Handlers.CommandHandlers;

internal class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommandRequest, DeletePositionCommandResponse>
{
	private readonly IPositionRepository _repository;

	public DeletePositionCommandHandler(IPositionRepository repository)
	{
		_repository = repository;
	}

	public async Task<DeletePositionCommandResponse> Handle(DeletePositionCommandRequest request, CancellationToken cancellationToken)
	{
		var Position = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

		if (Position == null)
		{
			return new DeletePositionCommandResponse { IsSuccess = false };
		}

		_repository.Remove(Position);
		await _repository.CommitAsync();

		return new DeletePositionCommandResponse
		{
			IsSuccess = true
		};
	}
}
