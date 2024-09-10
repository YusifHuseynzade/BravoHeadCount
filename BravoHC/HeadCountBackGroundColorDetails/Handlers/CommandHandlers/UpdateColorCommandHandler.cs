using Domain.IRepositories;
using HeadCountBackGroundColorDetails.Commands.Request;
using HeadCountBackGroundColorDetails.Commands.Response;
using MediatR;

namespace HeadCountBackGroundColorDetails.Handlers.CommandHandlers;

public class UpdateColorCommandHandler : IRequestHandler<UpdateColorCommandRequest, UpdateColorCommandResponse>
{
    private readonly IHeadCountBackgroundColorRepository _repository;

    public UpdateColorCommandHandler(IHeadCountBackgroundColorRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateColorCommandResponse> Handle(UpdateColorCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new UpdateColorCommandResponse();

        try
        {
            var color = await _repository.GetAsync(p => p.Id == request.Id);
            if (color != null)
            {
                color.SetDetail(request.ColorHexCode);
                await _repository.UpdateAsync(color);
                response.IsSuccess = true;
                response.Message = "Color updated successfully.";
            }
            else
            {
                response.Message = "Color not found.";
            }
        }
        catch (Exception ex)
        {
            response.Message = $"Error updating color: {ex.Message}";
        }

        return response;
    }

}
