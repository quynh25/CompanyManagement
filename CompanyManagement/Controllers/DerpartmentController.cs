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
    public class DerpartmentController : Controller
    {
        private readonly IDerpartmentRepository _derpartmentRepository;
        private readonly ICenterRepository _centerRepository;
        private readonly IMapper _mapper;
        public DerpartmentController(IDerpartmentRepository derpartmentRepository, 
            ICenterRepository centerRepository
            ,IMapper mapper)
        {
            _derpartmentRepository = derpartmentRepository;
            _centerRepository = centerRepository;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDerpartment([FromQuery] int centerId,[FromBody] DerpartmentDto createDerpartment)
        {
            if(createDerpartment == null)
            {
                return BadRequest(ModelState);
            }
            var derpartment = _derpartmentRepository.GetDerpartments()
                .Where(d => d.DerpartmentName.Trim().ToUpper()==createDerpartment.DerpartmentName.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (derpartment != null) {
                ModelState.AddModelError("", "department alrealy exists");
                return StatusCode(422, ModelState);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var derpartmentMap = _mapper.Map<Derpartment>(createDerpartment);
            derpartmentMap.Center = _centerRepository.GetCenter(centerId);
            if(!_derpartmentRepository.CreateDeparment(derpartmentMap) ){
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("successfully created");
        }
        [HttpDelete("{departmentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDepartment(int departmentId)
        {
            if (!_derpartmentRepository.DerpartmentExits(departmentId))
            {
                return NotFound();
            }
            var departmentDelete = _derpartmentRepository.GetDerpartmentsById(departmentId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_derpartmentRepository.DeleteDeparment(departmentDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting department");
            }
            return NoContent();
        }
        [HttpPut("{departmentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDeparment(int departmentId, [FromBody] DerpartmentDto updateDepartment)
        {
            if (updateDepartment == null)
            {
                return BadRequest(ModelState);
            }
            if (departmentId != updateDepartment.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_derpartmentRepository.DerpartmentExits(departmentId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var departmentMap = _mapper.Map<Derpartment>(updateDepartment);
            if (!_derpartmentRepository.UpdateDeparment(departmentMap))
            {
                ModelState.AddModelError("", "Something went wrong updating department");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
