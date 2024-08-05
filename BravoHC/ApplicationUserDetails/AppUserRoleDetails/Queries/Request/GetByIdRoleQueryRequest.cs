using MediatR;
using RoleDetails.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUserDetails.AppUserRoleDetails.Queries.Request;

public class GetByIdRoleQueryRequest : IRequest<GetByIdRoleQueryResponse>
{
	public int Id { get; set; }
}
