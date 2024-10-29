using Dapper;
using Domain_Layer.Interfaces;
using Domain_Layer.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Dapper
{
    public class StudentDapperRepository : IRepository<Student>
    {
        private readonly IConfiguration _configuration;
       // private SqlLiteDBCreation sqliteDb;

        public StudentDapperRepository(IConfiguration configuration) {
        
            _configuration = configuration;
            // sqliteDb = new SqlLiteDBCreation(configuration);
            

        }

        public Task<Student> Add(Student entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAll()
        {


            //Task<IEnumerable<Student>> students= new List<Student>();

            //if (await sqliteDb.CreateSqlLiteDBAsync()) {


            using var _connection = new SqliteConnection(_configuration.GetConnectionString("SqliteConnection"));

            var slqQuery = "select * from Student";

            var students = _connection.QueryAsync<Student>(slqQuery);

            return await students;

            //}

            //return Enumerable.Empty<Student>();


        }

        public async Task<Student> GetById(int studentId)
        {
            using var _connection = new SqliteConnection(_configuration.GetConnectionString("SqliteConnection"));

            var student = await _connection.QueryFirstAsync<Student>("select * from Student where Id=@id", new { id=studentId });

            return student;   

        }

        public Task<Student> Update(Student entity)
        {
            throw new NotImplementedException();
        }
    }
}
