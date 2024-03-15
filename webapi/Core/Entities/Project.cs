using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using webapi.Core.Enums;

namespace webapi.Core.Entities
{
    public class Project : BaseEntity
    {
        public string Title { get; set; }
        public ProjectType Type { get; set; }
                
        public long ClientId { get; set; }
        public Client Client { get; set; }
        public ICollection<Proposal> Proposals { get; set; }
    }
}