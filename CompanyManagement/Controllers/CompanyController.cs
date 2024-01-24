using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class CompanyController : Controller
    {
        public readonly ICompanyRepository _companyRepository;
        public CompanyController(ICompanyRepository companyRepository){
            _companyRepository = companyRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
        public IActionResult GetCompany()
        {
            var companys = _companyRepository.GetCompanies();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(companys);
        }
    }
}
