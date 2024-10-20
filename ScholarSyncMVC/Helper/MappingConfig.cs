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
            CreateMap<EduLevel, EduLevelVM>().ReverseMap();
            CreateMap<ProfileVM, AppUser>().ReverseMap();
            CreateMap<Education, EducationVM>().ReverseMap();

        }
    }
}
