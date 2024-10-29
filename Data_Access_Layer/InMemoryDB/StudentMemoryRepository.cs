using Domain_Layer.Interfaces;
using Domain_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                new() { Id = 1, Name = "John Michel", Age = 35, Description = "Student Description" }
            };

        }

        public async Task<Student> Add(Student student)
        {
            studentList.Add(student);
            return await Task.FromResult(student);
        }

        public void Delete(int studentId)
        {
            studentList.RemoveAll(x => x.Id == studentId);
        }

        public Task<IEnumerable<Student>> GetAll() => Task.FromResult(studentList.Select(x => x));

        public Task<Student> GetById(int studentId) => Task.FromResult(studentList.Find(s => s.Id.Equals(studentId)))!;

        public Task<Student> Update(Student student)
        {
            throw new NotImplementedException();
        }

    }
}
