using AutoMapper;
using Domain.Entities;
using HeadCountDetails.Queries.Response;
using Microsoft.AspNetCore.Http;

namespace HeadCountDetails.Profiles
{
    public class HeadCountMapper : Profile
    {
        private readonly IHttpContextAccessor _httpAccessor;
        public HeadCountMapper(IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;

            // HeadCount -> GetAllHeadCountQueryResponse
            CreateMap<HeadCount, GetAllHeadCountQueryResponse>()
           .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee))
           .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent != null ? new ManagerResponse { Id = src.Parent.Employee.Id, FullName = src.Parent.Employee.FullName } : null))
           .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Project))
           .ForMember(dest => dest.Section, opt => opt.MapFrom(src => src.Section))
           .ForMember(dest => dest.SubSection, opt => opt.MapFrom(src => src.SubSection))
           .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
           .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
           .ReverseMap();



            CreateMap<Employee, EmployeeResponse>()
                .ForMember(dest => dest.ResidentalArea, opt => opt.MapFrom(src => src.ResidentalArea))
                .ReverseMap();

 
            CreateMap<ResidentalArea, ResidentalAreaResponse>()
                .ReverseMap();

            CreateMap<Project, ProjectResponse>()
                .ReverseMap();

            CreateMap<Section, SectionResponse>()
                .ReverseMap();

            CreateMap<SubSection, SubSectionResponse>()
                .ReverseMap();

            CreateMap<Position, PositionResponse>()
                .ReverseMap();

            CreateMap<HeadCountBackgroundColor, ColorResponse>()
                .ReverseMap();

            CreateMap<HeadCount, GetByIdHeadCountQueryResponse>().ReverseMap();
        }
    }
}
