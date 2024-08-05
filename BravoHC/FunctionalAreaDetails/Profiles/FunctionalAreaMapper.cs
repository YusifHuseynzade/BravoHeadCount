using AutoMapper;
using Domain.Entities;
using FunctionalAreaDetails.Queries.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalAreaDetails.Profiles;

public class FunctionalAreaMapper :Profile
{
	private readonly IHttpContextAccessor _httpAccessor;
	public FunctionalAreaMapper(IHttpContextAccessor httpAccessor)
	{
		_httpAccessor = httpAccessor;

		CreateMap<FunctionalArea, GetAllFunctionalAreaQueryResponse>().ReverseMap(); 
		CreateMap<FunctionalArea, GetByIdFunctionalAreaQueryResponse>().ReverseMap();
		CreateMap<Project, GetFunctionalAreaProjectsQueryResponse>().ReverseMap();
	}
}
