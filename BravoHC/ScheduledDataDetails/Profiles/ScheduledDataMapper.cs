using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using ScheduledDataDetails.Queries.Response;

namespace ScheduledDataDetails.Profiles;

public class ScheduledDataMapper : Profile
{
    private readonly IHttpContextAccessor _httpAccessor;
    public ScheduledDataMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<ScheduledData, GetAllScheduledDataQueryResponse>().ReverseMap();
        CreateMap<ScheduledData, GetByIdScheduledDataQueryResponse>().ReverseMap();
    }
}