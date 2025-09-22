using AutoMapper;
using Company.DAL.Models.EmployeeModule;
using Demo.BusniessLogic.Dtos;

namespace Company.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee , EmployeeDto>()
               .ForMember(dest =>dest.Gender,option =>option.MapFrom(src =>src.Gender ))
               .ForMember(dest =>dest.EmployeeType,option =>option.MapFrom(src =>src.EmployeeType ));

            CreateMap<Employee , EmployeeDetailsDto>()
               .ForMember(dest => dest.Gender, option => option.MapFrom(src => src.Gender))
               .ForMember(dest => dest.EmployeeType, option => option.MapFrom(src => src.EmployeeType))
               .ForMember(dest => dest.HiringDate, option => option.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
               ;
            CreateMap<CreatedEmployeeDto, Employee>()
               .ForMember(dest => dest.HiringDate, option => option.MapFrom(src =>src.HiringDate.ToDateTime(TimeOnly.MinValue)))
                ;
            CreateMap<UpdatedEmployeeDto, Employee>()
               .ForMember(dest => dest.HiringDate, option => option.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)))
                ;
        }
    }
}
