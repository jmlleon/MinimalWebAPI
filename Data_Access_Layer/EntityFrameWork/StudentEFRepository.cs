using Domain_Layer.Interfaces;
using Domain_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.EntityFrameWork
{
    public class StudentEFRepository : IRepository<Student>
    {

        private readonly StudentContext _studentContext;

        public StudentEFRepository(StudentContext studentContext)
        {
            _studentContext = studentContext;

        }

        public async Task<Student> Add(Student student)
        {

            _studentContext.Students.Add(student);

            await _studentContext.SaveChangesAsync();

            return student;
        }

        public async void Delete(int studentId)
        {
            if (await _studentContext.Students.FindAsync(studentId) is Student student)
            {
                _studentContext.Students.Remove(student);
                await _studentContext.SaveChangesAsync();
                //return Results.NoContent();
            }
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _studentContext.Students.ToListAsync();
        }

        public async Task<Student> GetById(int studentId)
        {
            return await _studentContext.Students.FindAsync(studentId);

        }

        public async Task<Student> Update(Student studentAdd)
        {
            var student = await _studentContext.Students.FindAsync(studentAdd.Id);

            /* if (student == null)
             {
                 throw new Exception("Not Student Found");
             }*/

            student.Name = studentAdd.Name;
            student.Age = studentAdd.Age;
            student.Description = studentAdd.Description;

            await _studentContext.SaveChangesAsync();

            return student;
        }
    }
}
