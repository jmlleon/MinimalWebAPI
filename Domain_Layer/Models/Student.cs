using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Models
{
    public class Student:BaseEntity
    {
        //public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Age { get; set;}
        public string? Description { get; set; }
    }
}
