using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using webapi.Core.Dtos.Client;
using webapi.Core.Context; 
using webapi.Core.Entities;
using AutoMapper; 
using Microsoft.EntityFrameworkCore;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public ClientController(ApplicationDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDto dto)
        {
            Client newClient = _mapper.Map<Client>(dto);
            await _context.Clients.AddAsync(newClient);
            await _context.SaveChangesAsync();

            return Ok("Client created successfully");
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<GetClientDto>>> GetClients()
        {
            var clients = await _context.Clients.ToListAsync();
            var convertedClients = _mapper.Map<IEnumerable<GetClientDto>>(clients);

            return Ok(convertedClients);
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<ActionResult<GetClientDto>> GetClient(long id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            var convertedClient = _mapper.Map<GetClientDto>(client);

            return Ok(convertedClient);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateClient(long id, [FromBody] UpdateClientDto dto)
        {
            var existingClient = await _context.Clients.FindAsync(id);

            if (existingClient == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, existingClient);
            await _context.SaveChangesAsync();

            return Ok("Client updated successfully");
        }


        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteClient(long id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return Ok("Client deleted successfully");
        }
    }
}