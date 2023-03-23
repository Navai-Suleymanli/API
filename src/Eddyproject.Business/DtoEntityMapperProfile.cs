using AutoMapper;
using Eddyproject.Common.Dtos.Address;
using Eddyproject.Common.Dtos.Budget;
using Eddyproject.Common.Dtos.Course;
using Eddyproject.Common.Dtos.Student;
using Eddyproject.Common.Model;

namespace Eddyproject.Business;

public class DtoEntityMapperProfile : Profile
{
    public DtoEntityMapperProfile()
    {
        CreateMap<AddressCreate, Address>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<AddressUpdate, Address>();
        CreateMap<Address, AddressGet>();


        CreateMap<BudgetCreate, Budget>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<BudgetUpdate, Budget>();
        CreateMap<Budget, BudgetGet>();


        CreateMap<StudentCreate, Student>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Courses, opt => opt.Ignore())
            .ForMember(dest => dest.Budget, opt => opt.Ignore());

        CreateMap<StudentUpdate, Student>()
            .ForMember(dest => dest.Courses, opt => opt.Ignore())
            .ForMember(dest => dest.Budget, opt => opt.Ignore());

        CreateMap<Student, StudentDetails>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
          //.ForMember(dest => dest.Courses, opt => opt.Ignore()) to do - add courses
            .ForMember(dest => dest.budget, opt => opt.Ignore())
            .ForMember(dest => dest.address, opt => opt.Ignore());

        CreateMap<Student, StudentList>();
        CreateMap<CourseCreate ,Course>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Students, opt => opt.Ignore());
        CreateMap<CourseUpdate, Course>()
            .ForMember(dest => dest.Students, opt => opt.Ignore());
        CreateMap<Course, CourseGet>();

    }
}


