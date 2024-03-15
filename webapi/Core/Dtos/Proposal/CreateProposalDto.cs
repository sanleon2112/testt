using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Core.Dtos.Proposal
{
    public class CreateProposalDto
    {
        public string Name { get; set; }
        public string Description { get; set; }                    
        public long ProyectId { get; set; }        
    }
}