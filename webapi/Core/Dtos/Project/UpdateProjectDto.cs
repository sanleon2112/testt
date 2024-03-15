using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using webapi.Core.Enums;

namespace webapi.Core.Dtos.Project
{
    public class UpdateProjectDto
    {
        public string Title { get; set; }
        public ProjectType Type { get; set; }
    }
}
