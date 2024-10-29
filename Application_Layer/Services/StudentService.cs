using Application_Layer.DTO;
using Application_Layer.Interfaces;
using Data_Access_Layer.Dapper;
using Domain_Layer.Interfaces;
using Domain_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Services
{
    internal class StudentService : IStudentService<StudentDTO>
    {
        private readonly IRepository<Student> _studentRepository;
        public StudentService(IRepository<Student> studentRepository) { 
        
        _studentRepository = studentRepository;
        } 


        public async Task<StudentDTO> Add(Student entity)
        {
            var student=await _studentRepository.Add(entity);

            return new StudentDTO
            {
                Name = student.Name,
                Description = student.Description,
                Age = student.Age,
            };
        }

        public void Delete(int entity)
        {
            _studentRepository.Delete(entity);  
        }

        public async Task<IEnumerable<StudentDTO>> GetAll()
        {
             var studentList=await _studentRepository.GetAll();    
            
            return studentList.Select(s=>new StudentDTO {            
                Name = s.Name, 
                Description = s.Description,
                Age = s.Age,            
            });
        }

        public async Task<StudentDTO> GetById(int studentId)
        {
            var student=await _studentRepository.GetById(studentId);

            return new StudentDTO { Name = student.Name, Age = student.Age, Description = student.Description };
        }

        public async Task<StudentDTO> Update(Student entity)
        {
            var student=await _studentRepository.Update(entity);

            return new StudentDTO
            {
                Name = student.Name,
                Description = student.Description,
                Age = student.Age,
            };
        }
    }
}
