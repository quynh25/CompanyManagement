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
    public class EmployeeController:Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDerpartmentRepository _derpartmentRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository,IDerpartmentRepository derpartmentRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _derpartmentRepository = derpartmentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployees()
        {
            var employee = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetEmployees());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(employee);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200,Type=typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployeeById(int id) {
            if (!_employeeRepository.EmployeeExits(id))
            {
                return NotFound();
            }
            var employee = _mapper.Map<EmployeeDto>(_employeeRepository.GetEmployeeById(id));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(employee);
        }
        [HttpGet("department/{employeeId}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult GetDerpartmentByEmployee(int employeeId)
        {
            var department = _mapper.Map<List<DerpartmentDto>>(_employeeRepository.GetDerpartmentByEmployee(employeeId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(department);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployee([FromQuery] int derpartmentId, [FromBody] EmployeeDto createEmployee)
        {
            if(createEmployee == null)
            {
                return BadRequest(ModelState);
            }
            var employee = _employeeRepository.GetEmployees()
                .Where(e => e.EmployeeName.Trim().ToUpper() == createEmployee.EmployeeName.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (employee != null)
            {
                ModelState.AddModelError("", "employee alrealy exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var employeeMap = _mapper.Map<Employee>(createEmployee);
            employeeMap.Derpartment = _derpartmentRepository.GetDerpartmentsById(derpartmentId);
            if (!_employeeRepository.CreateEmployee(employeeMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("successfully created");
        }
        [HttpDelete("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEmployee(int employeeId)
        {
            if (!_employeeRepository.EmployeeExits(employeeId))
            {
                return NotFound();
            }
            var employeeDelete = _employeeRepository.GetEmployeeById(employeeId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_employeeRepository.DeleteEmployee(employeeDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting employee");
            }
            return NoContent();

        }
    }
}
