using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PositionDetails.Commands.Request;
using PositionDetails.Commands.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionDetails.Handlers.CommandHandlers;

public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommandRequest, UpdatePositionCommandResponse>
{
	private readonly IPositionRepository _positionRepository;

	public UpdatePositionCommandHandler(IPositionRepository positionRepository)
	{
		_positionRepository = positionRepository;
	}

	public async Task<UpdatePositionCommandResponse> Handle(UpdatePositionCommandRequest request, CancellationToken cancellationToken)
	{
		var response = new UpdatePositionCommandResponse();

		try
		{
			// Position-ı yenilə
			var position = await _positionRepository.GetAsync(p => p.Id == request.Id);
			if (position != null)
			{
				position.SetDetail(request.Name);
				await _positionRepository.UpdateAsync(position);
				response.IsSuccess = true;
				response.Message = "Position updated successfully.";
			}
			else
			{
				response.Message = "Position not found.";
			}
		}
		catch (Exception ex)
		{
			response.Message = $"Error updating position: {ex.Message}";
		}

		return response;
	}

}
