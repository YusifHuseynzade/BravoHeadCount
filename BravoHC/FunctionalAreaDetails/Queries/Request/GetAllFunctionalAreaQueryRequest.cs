using Common.Constants;
using FunctionalAreaDetails.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalAreaDetails.Queries.Request;

public class GetAllFunctionalAreaQueryRequest : IRequest<List<GetFunctionalAreaListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}
