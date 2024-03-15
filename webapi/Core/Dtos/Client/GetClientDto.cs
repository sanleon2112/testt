using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using webapi.Core.Enums;

namespace webapi.Core.Dtos.Client
{
    public class GetClientDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CompanySize Size { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;         
    }
}