using AutoMapper;
using CompanyManagement.Dto;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace CompanyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CenterController : Controller
    {
        private readonly ICenterRepository _centerRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CenterController(ICenterRepository centerRepository,ICompanyRepository companyRepository, IMapper mapper) {
            _centerRepository = centerRepository;
            _companyRepository= companyRepository;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCenter([FromQuery] int companyId,[FromBody] CenterDto centerCreate)
        {
            if(centerCreate == null)
            {
                return BadRequest(ModelState);
            }
            var centers = _centerRepository.GetCenter()
                .Where(c => c.CenterName.Trim().ToUpper() == centerCreate.CenterName.TrimEnd().ToUpper())
                .FirstOrDefault();
            if(centers != null)
            {
                ModelState.AddModelError("", "center alrealy exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var centerMap = _mapper.Map<Center>(centerCreate);
            centerMap.Company = _companyRepository.GetCompany(companyId);
            if (!_centerRepository.CreateCenter(centerMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500,ModelState);
            }
            return Ok("successfully created");

        }

        [HttpDelete("{centerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCenter(int centerId)
        {
            if (!_centerRepository.CenterExists(centerId))
            {
                return NotFound();
            }
            var centerDelete = _centerRepository.GetCenter(centerId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_centerRepository.DeleteCenter(centerDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting center");
            }
            return NoContent();
        }
        [HttpPut("{centerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCenter(int centerId, [FromBody] CenterDto updateCenter)
        {
            if (updateCenter == null)
            {
                return BadRequest(ModelState);
            }
            if (centerId != updateCenter.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_centerRepository.CenterExists(centerId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var centerMap = _mapper.Map<Center>(updateCenter);
            if (!_centerRepository.UpdateCenter(centerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating center");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
