using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using webapi.Core.Enums;
using webapi.Core.Entities;

namespace webapi.Core.Dtos.Project
{
    public class GetProjectDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public ProjectType Type { get; set; }        
        public long ClientId { get; set; }
        public string ClientName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;         
    }
}