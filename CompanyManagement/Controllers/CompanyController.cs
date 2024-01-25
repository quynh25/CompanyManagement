using AutoMapper;
using CompanyManagement.Dto;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CompanyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        public readonly ICompanyRepository _companyRepository;
        public readonly IMapper _mapper;
        public CompanyController(ICompanyRepository companyRepository,IMapper mapper){
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
        public IActionResult GetCompany()
        {
            var companys = _mapper.Map<List<CompanyDto>>(_companyRepository.GetCompanies());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(companys);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200,Type= typeof(Company))]
        public IActionResult GetCompany(int id)
        {
            if (!_companyRepository.CompanyExists(id)){
                return NotFound();
            }
            var company = _mapper.Map < CompanyDto>(_companyRepository.GetCompany(id));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(company);
        }
       
    }
}
