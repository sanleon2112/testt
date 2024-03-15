using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using webapi.Core.Enums;

namespace webapi.Core.Dtos.Client
{
    public class CreateClientDto
    {
        public string Name { get; set; }
        public CompanySize Size { get; set; }
    }
}
