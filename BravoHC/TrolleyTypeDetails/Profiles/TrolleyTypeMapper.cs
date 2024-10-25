using AutoMapper;
using Core.Helpers;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using TrolleyTypeDetails.Queries.Response;

namespace TrolleyTypeDetails.Profiles;

public class TrolleyTypeMapper : Profile
{

    private readonly IHttpContextAccessor _httpAccessor;

    public TrolleyTypeMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<TrolleyType, GetAllTrolleyTypeQueryResponse>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.Image)
                        ? $"{RequestExtensions.BaseUrl(_httpAccessor.HttpContext)}/{src.Image}"
                        : null))
                .ReverseMap();

        CreateMap<TrolleyType, GetByIdTrolleyTypeQueryResponse>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src =>
                !string.IsNullOrEmpty(src.Image)
                    ? $"{RequestExtensions.BaseUrl(_httpAccessor.HttpContext)}/{src.Image}"
                    : null))
            .ReverseMap();
    }
}
