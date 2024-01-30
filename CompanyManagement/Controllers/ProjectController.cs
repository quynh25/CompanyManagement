using AutoMapper;
using CompanyManagement.Dto;
using CompanyManagement.Interfaces;
using CompanyManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IDerpartmentRepository _derpartmentRepository;
        private readonly IMapper _mapper;
        public ProjectController(IProjectRepository projectRepository, IDerpartmentRepository derpartmentRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _derpartmentRepository = derpartmentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Project>))]
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
        public IActionResult GetProjectById(int id)
        {
            if (!_projectRepository.ProjectExits(id))
            {
                return NotFound();
            }
            var project = _mapper.Map<ProjectDto>(_projectRepository.GetProjectById(id));
            if (!ModelState.IsValid)
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProject([FromQuery] int derpartmentId, [FromBody] ProjectDto createProject)
        {
            if (createProject == null)
            {
                return BadRequest(ModelState);
            }
            var project = _projectRepository.GetProjects()
                .Where(p => p.ProjectName.Trim().ToUpper() == createProject.ProjectName.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (project != null)
            {
                ModelState.AddModelError("", "department alrealy exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var projectMap = _mapper.Map<Project>(createProject);
            projectMap.Derpartment = _derpartmentRepository.GetDerpartmentsById(derpartmentId);
            if (!_projectRepository.CreateProject(projectMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");

            }
            return Ok("succefully create");
        }
        [HttpDelete("{projectId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProject(int projectId)
        {
            if (!_projectRepository.ProjectExits(projectId))
            {
                return NotFound();
            }
            var projectDelete = _projectRepository.GetProjectById(projectId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_projectRepository.DeleteProject(projectDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting company");
            }
            return NoContent();

        }
    }
}
