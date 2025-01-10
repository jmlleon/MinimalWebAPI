using Dapper;
using Domain_Layer.Interfaces;
using Domain_Layer.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Data_Access_Layer.Dapper
{
    public sealed class StudentDapperRepository : IRepository<Student>
    {
        private readonly IConfiguration _configuration;
        // private SqlLiteDBCreation sqliteDb;

        public StudentDapperRepository(IConfiguration configuration) {

            _configuration = configuration;
            // sqliteDb = new SqlLiteDBCreation(configuration);

        }

        private SqliteConnection GetConnection() =>new (_configuration.GetConnectionString("SqliteConnection"));          
        

        public async Task<int> Add(Student entity)
        {
            using var _connection=GetConnection();
            var sqlQuery = "Insert into Student(Name,Age,Description ) values (@name, @age, @description)";
            return await _connection.ExecuteAsync(sqlQuery, new { name = entity.Name, age = entity.Age, description = entity.Description});
        }

        public async Task<int> Delete(int entityId)
        {
            using var _connection = GetConnection();
            var sqlQuery = "Delete from Student where Id=@id";
            return await _connection.ExecuteAsync(sqlQuery, new { id = entityId });
           
        }
        public async Task<IEnumerable<Student>> GetAll()
        {
            using var _connection = GetConnection();
            var slqQuery = "select * from Student";
            var students = _connection.QueryAsync<Student>(slqQuery);
            return await students;
        }

        public async Task<Student?> GetById(int studentId)
        {
            using var _connection = GetConnection();
            var slqQuery = "select * from Student where Id=@id";
            var student = await _connection.QuerySingleOrDefaultAsync<Student>(slqQuery, new { id=studentId });
            return student;   

        }

        public async Task<int> Update(Student entity)
        {
            using var _connection = GetConnection();
            var sqlQuery = "Update Student set Name=@name, Age=@age, Description=@description where Id=@id";
            return await _connection.ExecuteAsync(sqlQuery, new {id=entity.Id, name=entity.Name, age=entity.Age, description=entity.Description });
           
        }

        public async Task<IQueryable<Student>> ExecuteQuery(Expression<Func<Student, bool>> predicate)
        {
            using var _connection = GetConnection();
            var slqQuery = "select * from Student";
            var students =await _connection.QueryAsync<Student>(slqQuery);
           
            return students.AsQueryable().Where(predicate);
        }

    }
}
