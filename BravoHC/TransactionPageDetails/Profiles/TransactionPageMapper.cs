using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace TransactionPageDetails.Profiles;

public class TransactionPageMapper : Profile
{

    private readonly IHttpContextAccessor _httpAccessor;

    public TransactionPageMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

    }
}
