using Application_Layer.DTO;
using Application_Layer.Interfaces;
using Application_Layer.Mapping;
using Domain_Layer.Errors;
using Domain_Layer.Models;
using Domain_Layer.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MinimalWebAPI.Endpoint
{
    public sealed class StudentEndPoint:IEndPoint
    {

        public static void AddEndPoint(IEndpointRouteBuilder app) {


            //Get All
            app.MapGet("/students", async ( IService<StudentDTO, Student> studentService) =>
            {
                var result = await studentService.GetAll();

                return Results.Ok(result.Value);

            });
               

            //Get by Name

            app.MapGet("/students/{studentName}", async (string studentName, IService<StudentDTO, Student> studentService) =>
            {
              
                var result= await studentService.ExecuteQuery(x=>x.Name==studentName);

                if (result.IsFailure) {
                    
                    return Results.NotFound(result.Error);       
                    
                }
                
                return Results.Ok(result.Value.ToList());

            });

            //Get By Id
            app.MapGet("/students/{id:int}", async (int id, IService<StudentDTO, Student> studentService) =>
            {

                //Result Pattern

                var result = await studentService.GetById(id);

                return result.IsFailure ? Results.NotFound(result.Error) : Results.Ok(result.Value);              
            });


            //Insert Student
            app.MapPost("/students", async (StudentDTO student,IService<StudentDTO, Student> studentService) =>
            {                               
                var result = await studentService.Add(student);
                return result.IsFailure ? Results.BadRequest(result.Error) : Results.Created($"/students/{student.Id}", student);               

            });

            //Update Student

            app.MapPut("/students/{id}", async (int id, StudentDTO studentAdd, IService<StudentDTO, Student> studentService) =>
            {             
                   
                var result= await studentService.Update(studentAdd,id);               

                return result.IsFailure ? Results.BadRequest(result.Error) : Results.NoContent();
              
               
            });

            //Remove Student

            app.MapDelete("/students/{id}", async (int id, IService<StudentDTO, Student> studentService) =>
            {
                var result = await studentService.Delete(id);

                return result.IsFailure ? Results.NotFound(result.Error) : Results.NoContent();
                         
               
            });


        }
    }
}
