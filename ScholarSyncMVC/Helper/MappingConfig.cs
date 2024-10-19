using AutoMapper;
using ScholarSyncMVC.Models;
using ScholarSyncMVC.ViewModels;

namespace ScholarSyncMVC.Helper
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<Category, CategoryCreatedVM>().ReverseMap();
            CreateMap<UniversityVM, University>().ReverseMap();
            CreateMap<Scholarship, ScholarshipVM>().ReverseMap();
            CreateMap<Country, CountryDepartmentVM>().ReverseMap();
            CreateMap<Department, CountryDepartmentVM>().ReverseMap();
            CreateMap<Department, CounryDeptEditVM>().ReverseMap();
            CreateMap<Country, CounryDeptEditVM>().ReverseMap();
            CreateMap<Requirements, RequirementVM>().ReverseMap();
            CreateMap<Department, SimpleDept>().ReverseMap();
            CreateMap<Review, ReviewVM>()
                .ForMember(x => x.FullName , z => z.MapFrom(x => x.User.FirstName +" "+x.User.LastName))
                .ForMember(x => x.DepartmentName, z => z.MapFrom(x => x.User.Department.Name))
                .ForMember(x => x.UserPhoto, z => z.MapFrom(x => x.User.PhotoURL)).ReverseMap();
            CreateMap<Scholarship, ScholarshipDetailsVM>()
               .ForMember(x => x.CountryName, z => z.MapFrom(x => x.Country.Name))
               .ForMember(x => x.DepartmentName, z => z.MapFrom(x => x.Department.Name))
               .ForMember(x => x.UniversityName, z => z.MapFrom(x => x.University.Name))
               .ReverseMap();

        }
    }
}
