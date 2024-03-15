using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Core.Dtos.Proposal
{
    public class GetProposalDto
    {
        public long Id { get; set; }
        public string Name { get; set; }        
        public string Description { get; set; }        
        public string ProposalUrl { get; set; }        
        public long ProjectId { get; set; }
        public string ProjectTitle { get; set; }
    }
}