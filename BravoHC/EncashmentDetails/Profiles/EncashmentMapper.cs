using AutoMapper;
using Domain.Entities;
using EncashmentDetails.Queries.Response;
using Microsoft.AspNetCore.Http;

namespace EncashmentDetails.Profiles;

public class EncashmentMapper : Profile
{

    private readonly IHttpContextAccessor _httpAccessor;

    public EncashmentMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<Encashment, GetAllEncashmentQueryResponse>().ReverseMap();
        CreateMap<Encashment, GetByIdEncashmentQueryResponse>().ReverseMap();
    }
}
