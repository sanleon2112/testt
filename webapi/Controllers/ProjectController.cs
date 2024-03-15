using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using webapi.Core.Dtos.Project;
using webapi.Core.Context; 
using webapi.Core.Entities;
using AutoMapper; 
using Microsoft.EntityFrameworkCore;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProjectController(ApplicationDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
        {
            var existingClient = await _context.Clients.FindAsync(dto.ClientId);

            if (existingClient == null)
            {
                return NotFound("Client not found");
            }

            var newProject = _mapper.Map<Project>(dto);
            await _context.Projects.AddAsync(newProject);
            await _context.SaveChangesAsync();

            return Ok("Project created successfully");
        }


        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<GetProjectDto>>> GetProjects()
        {            
            var projects = await _context.Projects.Include(project => project.Client).ToListAsync();
            var convertedProject = _mapper.Map<IEnumerable<GetProjectDto>>(projects);

            return Ok(convertedProject);
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<ActionResult<GetProjectDto>> GetProject(long id)
        {
            var project = await _context.Projects.Include(p => p.Client).FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            var convertedProject = _mapper.Map<GetProjectDto>(project);

            return Ok(convertedProject);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateProject(long id, [FromBody] UpdateProjectDto dto)
        {
            var existingProject = await _context.Projects.FindAsync(id);

            if (existingProject == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, existingProject);

            _context.Projects.Attach(existingProject);
            _context.Entry(existingProject).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Project updated successfully");
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return Ok("Project deleted successfully");
        }
    }
}
