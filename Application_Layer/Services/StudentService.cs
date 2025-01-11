using Application_Layer.DTO;
using Application_Layer.Interfaces;
using Application_Layer.Mapping;
using Data_Access_Layer.Dapper;
using Domain_Layer.Errors;
using Domain_Layer.Interfaces;
using Domain_Layer.Models;
using Domain_Layer.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Services
{
    public class StudentService : IService<StudentDTO, Student>
    {

        //Service for repository implementation using Dapper and Memory Database

        private readonly IRepository<Student> _studentRepository;
        public StudentService(IRepository<Student> studentRepository) { 
        
        _studentRepository = studentRepository;

        } 


        public async Task<Result> Add(StudentDTO studentDTO)
        {         
            
                 var validationResult = StudentValidator.IsValidOnAdd(studentDTO.MapDTOtoStudent());

                if (validationResult != ValidationResult.Success)
                {
                    return Result.Failure(new Error("Validation Error", String.Join(",", validationResult.ErrorMessage!.Split(","))));                    
                    
                }

                var result = await _studentRepository.Add(studentDTO.MapDTOtoStudent());            

                return result > 0 ? Result.Success(): Result.Failure(StudentErrors.StudentAddError); ;           
            
        }

        public async Task<CustomResult<int>> Delete(int entity)
        {                      

            if (await _studentRepository.Delete(entity) == 0) {

                return CustomResult<int>.Failure(CustomError.DeleteError("Error on Delete Student"));            
            }

            return CustomResult<int>.Success(1);  
        }

        public async Task<CustomResult<IQueryable<StudentDTO>>> ExecuteQuery(Expression<Func<Student, bool>> predicate)
        {
            var result = await _studentRepository.ExecuteQuery(predicate);

            if (!result.Any()) {

                return CustomResult<IQueryable<StudentDTO>>.Failure(CustomError.RecordNotFound("Student not found by name"));            
            
            }

            return CustomResult<IQueryable<StudentDTO>>.Success(result.Select(x=>x.MapStundentToDTO()));
        }

        public async Task<CustomResult<IEnumerable<StudentDTO>>> GetAll()
        {
             var studentList=await _studentRepository.GetAll();   
            
            return CustomResult<IEnumerable<StudentDTO>>.Success(studentList.Select(s=>s.MapStundentToDTO()));
        }

        public async Task<CustomResult<StudentDTO>> GetById(int studentId)
        {
            var student=await _studentRepository.GetById(studentId);           

            if (student == null) {

                return CustomResult<StudentDTO>.Failure(CustomError.RecordNotFound("Student Not Found"));
            
            }
            
            return CustomResult<StudentDTO>.Success(student.MapStundentToDTO());
        }

        public async Task<IEnumerable<StudentDTO>> GetByName(string name) {

            var students = await ExecuteQuery(x=>x.Name==name);

            return students.Value.Where(s => s.Name!.Equals(name));
        }

        public async Task<CustomResult<int>> Update(StudentDTO studentDTO, int id)
        {

            var validationResult = StudentValidator.IsValidOnUpdate(studentDTO.MapDTOtoStudent(), id);

            if (validationResult != ValidationResult.Success)
            {
                return CustomResult<int>.Failure(CustomError.ValidationError(String.Join(" ", validationResult.ErrorMessage!.Split(","))));
                //return Results.ValidationProblem(dictionaryValue);
            }

            var result= await _studentRepository.Update(studentDTO.MapDTOtoStudent());
           
            if(result ==0) {

                return CustomResult<int>.Failure(CustomError.UpdateError("Student Update Error"));                 
            }            
            
            return CustomResult<int>.Success(result);
            
        }
    }
}
