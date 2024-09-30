using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using StoreDetails.Queries.Response;

namespace StoreDetails.Profiles;

public class StoreMapper : Profile
{
    private readonly IHttpContextAccessor _httpAccessor;
    public StoreMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<Store, GetAllStoreQueryResponse>().ReverseMap();
        CreateMap<Store, GetByIdStoreQueryResponse>().ReverseMap();

        CreateMap<StoreHistory, GetStoreHistoryQueryResponse>().ReverseMap();

    }
}