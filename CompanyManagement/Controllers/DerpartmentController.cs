using AutoMapper;
using CompanyManagement.Dto;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DerpartmentController : Controller
    {
        private readonly IDerpartmentRepository _derpartmentRepository;
        private readonly IMapper _mapper;
        public DerpartmentController(IDerpartmentRepository derpartmentRepository, IMapper mapper)
        {
            _derpartmentRepository = derpartmentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Derpartment>))]
        public IActionResult GetDerpartments() {
            var derparment = _mapper.Map<List<DerpartmentDto>>(_derpartmentRepository.GetDerpartments());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(derparment);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Derpartment))]
        [ProducesResponseType(400)]
        public IActionResult GetDerpartmentsById(int id)
        {
            if (!_derpartmentRepository.DerpartmentExits(id))
            {
                return NotFound();
            }
            var derpartment = _mapper.Map<DerpartmentDto>(_derpartmentRepository.GetDerpartmentsById(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(derpartment);
        }
        [HttpGet("center/{derparmentId}")]
        [ProducesResponseType(200, Type = typeof(Derpartment))]
        [ProducesResponseType(400)]
        public IActionResult GetCenterByDerpartment(int derparmentId)
        {
            var center = _mapper.Map<List<CenterDto>>(_derpartmentRepository.GetCenterByDerpartment(derparmentId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(center);
        }
    }
}
