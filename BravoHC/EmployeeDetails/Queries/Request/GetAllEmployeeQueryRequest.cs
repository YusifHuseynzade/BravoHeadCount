﻿using Common.Constants;
using EmployeeDetails.Queries.Response;
using MediatR;

namespace EmployeeDetails.Queries.Request;

public class GetAllEmployeeQueryRequest : IRequest<List<GetEmployeeListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
    public string? Search { get; set; }
}
