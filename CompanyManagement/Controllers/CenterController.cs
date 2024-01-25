using AutoMapper;
using CompanyManagement.Dto;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CenterController : Controller
    {
        private readonly ICenterRepository _centerRepository;
        private readonly IMapper _mapper;

        public CenterController(ICenterRepository centerRepository, IMapper mapper) {
            _centerRepository = centerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Center>))]
        public IActionResult GetCenter()
        {
            var center = _mapper.Map<List<CenterDto>>(_centerRepository.GetCenter());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(center);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Center))]
        [ProducesResponseType(400)]
        public IActionResult GetCenter(int id)
        {
            if (!_centerRepository.CenterExists(id))
            {
                return NotFound();
            }
            var center = _mapper.Map<CenterDto>(_centerRepository.GetCenter(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(center);
        }
        [HttpGet("company/{centerId}")]
        [ProducesResponseType(200, Type = typeof(Center))]
        [ProducesResponseType(400)]
        public IActionResult GetCompanyByCenter(int centerId)
        {
            var company = _mapper.Map<List<CompanyDto>>(_centerRepository.GetCompanyByCenter(centerId));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(company);
        }
    }
}
