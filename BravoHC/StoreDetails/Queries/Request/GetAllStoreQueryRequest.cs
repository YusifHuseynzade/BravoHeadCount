﻿using Common.Constants;
using MediatR;
using StoreDetails.Queries.Response;

namespace StoreDetails.Queries.Request;

public class GetAllStoreQueryRequest : IRequest<List<GetAllStoreListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
    public int? ProjectId { get; set; }
}
