﻿using Common.Constants;
using MediatR;
using SummaryDetails.Queries.Response;

namespace SummaryDetails.Queries.Request;

public class GetAllSummaryQueryRequest : IRequest<List<GetAllSummaryListQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
    public int? Month { get; set; } // Month filter
    public int? Year { get; set; }  // Year filter
}
