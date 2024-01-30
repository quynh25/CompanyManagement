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
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCompany([FromBody] CompanyDto companyCreate)
        {
            if(companyCreate == null)
            {
                return BadRequest(ModelState);
            }
            var company = _companyRepository.GetCompanies()
                .Where(c => c.CompanyName.Trim().ToUpper() == companyCreate.CompanyName.TrimEnd().ToUpper())
                .FirstOrDefault();
            if(company != null) {
                ModelState.AddModelError("", "company already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var companyMap = _mapper.Map<Company>(companyCreate);
            if (!_companyRepository.CreateCompany(companyMap))
            {
                ModelState.AddModelError("", "something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("successfully created");
        }
        [HttpDelete("{companyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCompany(int companyId)
        {
            if (!_companyRepository.CompanyExists(companyId))
            {
                return NotFound();
            }
            var companyDelete = _companyRepository.GetCompany(companyId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_companyRepository.DeleteCompany(companyDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting company");
            }
            return NoContent();

        }
    }
}
