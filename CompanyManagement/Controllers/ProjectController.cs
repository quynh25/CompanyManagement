using AutoMapper;
using CompanyManagement.Dto;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController:Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public ProjectController(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Project>))]
        public IActionResult GetProjects()
        {
            var projects = _mapper.Map<List<ProjectDto>>(_projectRepository.GetProjects());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(projects);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectById(int id) {
            if (!_projectRepository.ProjectExits(id))
            {
                return NotFound();
            }
            var project = _mapper.Map<ProjectDto>(_projectRepository.GetProjectById(id));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(project);
        }
        [HttpGet("deparment/{projectId}")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartmentByProjects(int projectId)
        {
            var department = _mapper.Map<List<DerpartmentDto>>(_projectRepository.GetDepartmentByProjects(projectId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(department);
        }
    }
}
