using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using webapi.Core.Enums;
using webapi.Core.Entities;

namespace webapi.Core.Dtos.Project
{
    public class CreateProjectDto
    {        
        public string Title { get; set; }
        public ProjectType Type { get; set; }        
        public long ClientId { get; set; }
    }
}