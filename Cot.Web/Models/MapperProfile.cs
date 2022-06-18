using AutoMapper;
using Cot.Data.Core.Domain;

namespace Cot.Web.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Course, CourseCreateModel>()
                .ReverseMap()
                .ForMember(e => e.Id, o => o.Ignore())
                .ForMember(e => e.ModifiedDateTime, o => o.Ignore());

            CreateMap<Course, CourseEditModel>()
                .ReverseMap()
                .ForMember(e => e.Id, o => o.Ignore())
                .ForMember(e => e.AddedDateTime, o => o.Ignore());
        }
    }
}
