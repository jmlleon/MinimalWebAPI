using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.DTO
{
    public class StudentDTO:BaseDTO
    {
        public string Name { get; set; } = String.Empty;
        public int Age { get; set; }
        public string? Description { get; set; }
    }
}
