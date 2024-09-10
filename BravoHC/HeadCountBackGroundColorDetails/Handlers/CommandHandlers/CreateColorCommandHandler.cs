using Domain.Entities;
using Domain.IRepositories;
using HeadCountBackGroundColorDetails.Commands.Request;
using HeadCountBackGroundColorDetails.Commands.Response;
using MediatR;

namespace HeadCountBackGroundColorDetails.Handlers.CommandHandlers;


public class CreateColorCommandHandler : IRequestHandler<CreateColorCommandRequest, CreateColorCommandResponse>
{
    private readonly IHeadCountBackgroundColorRepository _repository;

    public CreateColorCommandHandler(IHeadCountBackgroundColorRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateColorCommandResponse> Handle(CreateColorCommandRequest request, CancellationToken cancellationToken)
    {
        // Əgər eyni adlı vəzifə artıq mövcuddursa
        if (await _repository.IsExistAsync(d => d.ColorHexCode == request.ColorHexCode))
        {
            return new CreateColorCommandResponse
            {
                IsSuccess = false,
                ErrorMessage = "Bu rəng artıq mövcuddur."
            };
        }

        // Yeni vəzifə yarat
        var color = new HeadCountBackgroundColor();

        // Yaratılan vəzifəyə məlumatları təyin et
        color.SetDetail(request.ColorHexCode);

        // Yeni vəzifəni repository-də əlavə et və dəyişiklikləri yaddaşa sal

        await _repository.AddAsync(color);
        await _repository.CommitAsync();

        // Uğurla tamamlanmış sorğu üçün cavabı qaytar
        return new CreateColorCommandResponse
        {
            IsSuccess = true
        };
    }
}


