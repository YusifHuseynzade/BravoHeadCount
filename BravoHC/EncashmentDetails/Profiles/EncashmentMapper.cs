using AutoMapper;
using Core.Helpers;
using Domain.Entities;
using EncashmentDetails.Queries.Response;
using Microsoft.AspNetCore.Http;

namespace EncashmentDetails.Profiles;

public class EncashmentMapper : Profile
{

    private readonly IHttpContextAccessor _httpAccessor;

    public EncashmentMapper(IHttpContextAccessor httpAccessor)
    {
        _httpAccessor = httpAccessor;

        CreateMap<Encashment, GetAllEncashmentQueryResponse>()
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectCode))
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
                 .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src =>
                    src.Attachments.Select(a =>
                        !string.IsNullOrEmpty(a.FileUrl)
                            ? $"{RequestExtensions.BaseUrl(_httpAccessor.HttpContext)}/{a.FileUrl}"
                            : null).ToList()))
                .ReverseMap();

        CreateMap<Encashment, GetByIdEncashmentQueryResponse>()
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectCode))
            .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.Branch.Name))
            .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src =>
                src.Attachments.Select(a => a.FileUrl)))
            .ReverseMap();
    }
}
