using AutoMapper;
using CompanyManagement.Dto;
using CompanyManagement.Models;

namespace CompanyManagement.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() { 
            CreateMap<Company,CompanyDto>();
        }
    }
}
