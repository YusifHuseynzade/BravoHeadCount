using FunctionalAreaDetails.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalAreaDetails.Queries.Request;

public class GetByIdFunctionalAreaQueryRequest : IRequest<GetByIdFunctionalAreaQueryResponse>
{
    public int Id { get; set; }
}
