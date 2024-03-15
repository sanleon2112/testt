using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using webapi.Core.Context;
using webapi.Core.Dtos.Proposal;
using webapi.Core.Entities;
using AutoMapper;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProposalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProposalController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateProposal([FromForm] CreateProposalDto dto, IFormFile pdfFile, long projectId)
        {
            var fiveMegaBytes = 50 * 1024 * 1024;
            var pdfMimeType = "application/pdf";

            if (pdfFile.Length > fiveMegaBytes || pdfFile.ContentType != pdfMimeType)
            {
                return BadRequest("File is not valid");
            }

            var proposalUrl = Guid.NewGuid().ToString() + ".pdf";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents", "pdfs", proposalUrl);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await pdfFile.CopyToAsync(stream);
            }

            var newProposal = _mapper.Map<Proposal>(dto);
            newProposal.ProposalUrl = proposalUrl;
            newProposal.ProjectId = projectId; 
            
            await _context.Proposals.AddAsync(newProposal);
            await _context.SaveChangesAsync();

            return Ok("Proposal created successfully");
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<GetProposalDto>>> GetProposals()
        {
            var proposals = await _context.Proposals.Include(proposal => proposal.Project).ToListAsync();
            var convertedProposals = _mapper.Map<IEnumerable<GetProposalDto>>(proposals);

            return Ok(convertedProposals);
        }

        [HttpGet]
        [Route("download/{url}")]
        public IActionResult DownloadPdfFile(string url)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents", "pdfs", url);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found");
            }

            var pdfBytes = System.IO.File.ReadAllBytes(filePath);
            var file = File(pdfBytes, "application/pdf", url);
            return file;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProposal(long id, [FromBody] UpdateProposalDto dto)
        {
            var proposal = await _context.Proposals.FindAsync(id);

            if (proposal == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, proposal);            
            await _context.SaveChangesAsync();

            return Ok("Proposal updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProposal(long id)
        {
            var proposal = await _context.Proposals.FindAsync(id);
            if (proposal == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Documents", "pdfs", proposal.ProposalUrl);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _context.Proposals.Remove(proposal);
            await _context.SaveChangesAsync();

            return Ok("Proposal deleted successfully");
        }


        private bool ProposalExists(long id)
        {
            return _context.Proposals.Any(e => e.Id == id);
        }
    }
}

