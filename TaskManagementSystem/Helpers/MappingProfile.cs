using AutoMapper;
using System.Net.Http.Headers;
using TaskManagementSystem.api.Dtos;
using TaskManagementSystem.core.Entities;

namespace TaskManagementSystem.api.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Taskat, TaskToReturnDto>()
                .ForMember(t => t.Category, o => o.MapFrom(o => o.Category.Name))
                .ForMember(t => t.Status,o => o.MapFrom(o => o.Status))
                ;
        }
    }
}
