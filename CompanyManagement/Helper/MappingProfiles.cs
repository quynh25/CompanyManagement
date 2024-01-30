using AutoMapper;
using CompanyManagement.Dto;
using CompanyManagement.Models;

namespace CompanyManagement.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() { 
            CreateMap<Company,CompanyDto>();
            CreateMap<CompanyDto,Company>();
            CreateMap<Center,CenterDto>();
            CreateMap<CenterDto,Center>();
            CreateMap<Derpartment,DerpartmentDto>();
            CreateMap<DerpartmentDto,Derpartment>();
            CreateMap<Project,ProjectDto>();
            CreateMap<ProjectDto,Project>();
            CreateMap<Employee,EmployeeDto>();
            CreateMap<EmployeeDto,Employee>();
        }
    }
}
