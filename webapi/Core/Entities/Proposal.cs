using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Core.Entities
{
    public class Proposal : BaseEntity
    {
        public string Name { get; set; }        
        public string Description { get; set; }        
        public string ProposalUrl { get; set; }
        
        public long ProjectId { get; set; }
        public Project Project { get; set; }
    }
}