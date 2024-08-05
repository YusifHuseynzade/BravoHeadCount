using Common.Constants;
using MediatR;
using RoleDetails.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUserDetails.AppUserRoleDetails.Queries.Request;

public class GetAllRoleQueryRequest : IRequest<List<GetRoleListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}
