using AutoMapper;
using CompanyManagement.Dto;
using CompanyManagement.Models;

namespace CompanyManagement.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() { 
            CreateMap<Company,CompanyDto>();
            CreateMap<Center,CenterDto>();
            CreateMap<Derpartment,DerpartmentDto>();
            CreateMap<Project,ProjectDto>();
            CreateMap<Employee,EmployeeDto>();
        }
    }
}
