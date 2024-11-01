﻿using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using ProjectDetails.Queries.Response;

namespace ProjectDetails.Profiles;

public class ProjectMapper : Profile
{
    private readonly IHttpContextAccessor _httpAccessor;
    public ProjectMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<Project, GetAllProjectQueryResponse>().ReverseMap();
        CreateMap<Project, GetByIdProjectQueryResponse>().ReverseMap();
        CreateMap<Section, GetProjectSectionQueryResponse>().ReverseMap();
        CreateMap<ProjectHistory, GetProjectHistoryQueryResponse>().ReverseMap();
        CreateMap<SettingFinanceOperation, FinanceOperationResponse>().ReverseMap();
    }
}