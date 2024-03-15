using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Core.Enums;

namespace webapi.Core.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public CompanySize Size { get; set; }
        
        public ICollection<Project> Projects { get; set; }
    }
}