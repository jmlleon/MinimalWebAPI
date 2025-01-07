using Application_Layer.DTO;
using Domain_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Mapping
{
    public static class MappingStudent
    {

        public static StudentDTO MapStundentToDTO(this Student student)
        {
            return new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Description = student.Description,
                Age = student.Age
            };
        }

        public static Student MapDTOtoStudent(this StudentDTO student)
        {
            return new Student
            {               
                Name = student.Name,
                Description = student.Description,
                Age = student.Age

            };

        }

    }
}
