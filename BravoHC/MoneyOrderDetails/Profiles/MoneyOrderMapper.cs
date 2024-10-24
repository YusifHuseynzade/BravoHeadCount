using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using MoneyOrderDetails.Queries.Response;

namespace MoneyOrderDetails.Profiles;

public class MoneyOrderMapper : Profile
{

    private readonly IHttpContextAccessor _httpAccessor;

    public MoneyOrderMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<MoneyOrder, GetAllMoneyOrderQueryResponse>().ReverseMap();
        CreateMap<MoneyOrder, GetByIdMoneyOrderQueryResponse>().ReverseMap();
    }
}
