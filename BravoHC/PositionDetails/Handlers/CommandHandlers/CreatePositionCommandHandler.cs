using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using PositionDetails.Commands.Request;
using PositionDetails.Commands.Response;

namespace PositionDetails.Handlers.CommandHandlers;


public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommandRequest, CreatePositionCommandResponse>
{
    private readonly IPositionRepository _repository;

    public CreatePositionCommandHandler(IPositionRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreatePositionCommandResponse> Handle(CreatePositionCommandRequest request, CancellationToken cancellationToken)
    {
        // Əgər eyni adlı vəzifə artıq mövcuddursa
        if (await _repository.IsExistAsync(d => d.Name == request.Name))
        {
            return new CreatePositionCommandResponse
            {
                IsSuccess = false,
                ErrorMessage = "Bu adlı vəzifə artıq mövcuddur."
            };
        }

        // Yeni vəzifə yarat
        var position = new Position();

        // Yaratılan vəzifəyə məlumatları təyin et
        position.SetDetail(request.Name);

        // Yeni vəzifəni repository-də əlavə et və dəyişiklikləri yaddaşa sal

        await _repository.AddAsync(position);
        await _repository.CommitAsync();

        // Uğurla tamamlanmış sorğu üçün cavabı qaytar
        return new CreatePositionCommandResponse
        {
            IsSuccess = true
        };
    }
}


