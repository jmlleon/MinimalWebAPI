using Domain_Layer.Interfaces;
using Domain_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.InMemoryDB
{
    public class StudentMemoryRepository : IRepository<Student>
    {
        private List<Student> studentList;

        public StudentMemoryRepository()
        {
            studentList = new()
            {
                new() {Id=1, Name = "John Michel", Age = 35, Description = "First Student" },
                new() {Id=2, Name = "Silver Stone", Age = 28, Description = "Second Student" },
                new() {Id=3, Name = "Lucas Jonson", Age = 30, Description = "Third Student" },                 
            };

        }

        public async Task<Student> Add(Student student)
        {
            studentList.Add(student);
            return await Task.FromResult(student);
        }

        public async Task<int> Delete(int studentId)
        {
            return await Task.FromResult(studentList.RemoveAll(x => x.Id == studentId));
        }

        public Task<IQueryable<Student>> ExecuteQuery(Expression<Func<Student, bool>> predicate)
        {
            return Task.FromResult(studentList.AsQueryable().Where(predicate));
        }

        public Task<IEnumerable<Student>> GetAll() => Task.FromResult(studentList.Select(x => x));

        public async Task<Student?> GetById(int studentId) => await Task.FromResult(studentList.Find(s => s.Id.Equals(studentId))!);

        public async Task<int> Update(Student student)
        {
            var studentIndex = studentList.FindIndex(s => s.Id == student.Id);

            if(studentIndex > -1) {

                studentList[studentIndex] = student;

                return await Task.FromResult(1);
            
            }

            return await Task.FromResult(-1);
        }

    }
}
