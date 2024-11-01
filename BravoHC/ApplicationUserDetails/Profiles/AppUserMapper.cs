﻿using ApplicationUserDetails.Commands.Request;
using ApplicationUserDetails.Querires.Response;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using RoleDetails.Queries.Response;

namespace ApplicationUserDetails.Profiles
{
    public class AppUserMapper : Profile
    {
        private readonly IHttpContextAccessor _httpAccessor;
        public AppUserMapper(IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;

            CreateMap<AppUser, GetAllAppUserQueryResponse>()
            .ForMember(dest => dest.RoleIds, opt => opt.MapFrom(src => src.AppUserRoles.Select(ur => ur.RoleId).ToList()));
            CreateMap<AppUser, GetByIdAppUserQueryResponse>()
            .ForMember(dest => dest.RoleIds, opt => opt.MapFrom(src => src.AppUserRoles.Select(ur => ur.RoleId).ToList()));
            CreateMap<CreateAppUserCommandRequest, AppUser>();
            CreateMap<UpdateAppUserCommandRequest, AppUser>();

            CreateMap<Role, GetAllRoleQueryResponse>().ReverseMap();
            CreateMap<Role, GetByIdRoleQueryResponse>().ReverseMap();
        }
    }
}