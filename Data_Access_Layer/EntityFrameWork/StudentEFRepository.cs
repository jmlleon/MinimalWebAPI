using Domain_Layer.Interfaces;
using Domain_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.EntityFrameWork
{
    public class StudentEFRepository: IRepository<Student>
    {

        private readonly StudentContext _studentContext;

        public StudentEFRepository(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }

        public async Task<Student> Add(Student student)
        {
            _studentContext.Students.Add(student);

            //await _studentContext.SaveChangesAsync();

            return student;


        }

        public async Task<int> Delete(int studentId)
        {
            if (await _studentContext.Students.FindAsync(studentId) is Student student)
            {
                _studentContext.Students.Remove(student);
                 return 1;
            }

            return -1;
        }

        public async Task<IQueryable<Student>> ExecuteQuery(Expression<Func<Student, bool>> predicate)
        {
            return  _studentContext.Set<Student>().Where(predicate);
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _studentContext.Students.ToListAsync();
        }

        public async Task<Student?> GetById(int studentId)
        {
            if (await _studentContext.Students.FindAsync(studentId) is Student student) {

                return student;            
            }

            return null;
        }

        public async Task<int> Update(Student studentAdd)
        {           

            if (await _studentContext.Students.FindAsync(studentAdd.Id) is Student student) {

                student.Name = studentAdd.Name;
                student.Age = studentAdd.Age;
                student.Description = studentAdd.Description;

                return 1;

            }

            return -1;
          
        }
    }
}
