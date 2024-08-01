using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using PositionDetails.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionDetails.Profiles;

public class PositionMapper : Profile
{

	private readonly IHttpContextAccessor _httpAccessor;

	public PositionMapper(IHttpContextAccessor httpAccessor)
	{
		_httpAccessor = httpAccessor;

		CreateMap<Position, GetAllPositionQueryResponse>().ReverseMap(); 
		CreateMap<Position, GetByIdPositionQueryResponse>().ReverseMap();
	}
}
