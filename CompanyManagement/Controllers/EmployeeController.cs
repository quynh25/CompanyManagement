using AutoMapper;
using CompanyManagement.Dto;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController:Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
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
    }
}
