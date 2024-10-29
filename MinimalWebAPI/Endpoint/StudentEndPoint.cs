using Application_Layer.DTO;
using Application_Layer.Interfaces;

using Domain_Layer.Models;
using Domain_Layer.Validation;

using System.ComponentModel.DataAnnotations;

namespace MinimalWebAPI.Endpoint
{
    public sealed class StudentEndPoint:IEndPoint
    {

        public static void AddEndPoint(IEndpointRouteBuilder app) {


            app.MapGet("/students", async (IStudentService<StudentDTO> studentService) =>
                await studentService.GetAll());

            app.MapGet("/students/{studentName}", async (string studentName, IStudentService<StudentDTO> studentService) =>
            {
                var students = await studentService.GetAll();
                return students.Where(s => s.Name!.Equals(studentName)).ToList();

            });


            app.MapGet("/students/{id:int}", async (int id, IStudentService<StudentDTO> studentService) =>
            {
                return await studentService.GetById(id) is StudentDTO student ? Results.Ok(student) : Results.NotFound();
            });

            app.MapPost("/students", async (StudentDTO student, IStudentService<StudentDTO> studentService) =>
            {

                var validationResult = StudentValidator.IsValid(student);

                if (validationResult==ValidationResult.Success)
                {
                    await studentService.Add(student);
                    return Results.Created($"/students/{student.Id}", student);
                }
                else
                {
                    var dictionaryValue = new Dictionary<string, string[]>{ { "error", validationResult.ErrorMessage!.Split(",") } };

                    return Results.ValidationProblem(dictionaryValue);
                    //return Results.BadRequest(validationResult.ErrorMessage);                    
                   

                }


            });

            app.MapPut("/students/{id}", async (int id, Student studenAdd, IStudentService<StudentDTO> studentService) =>
            {
                var student = await studentService.GetById(id);

                if (student is null) return Results.NotFound();

              /*  using(var validationResult = StudentValidator.IsValid(student))
                {
                    await studentService.Add(student);
                    return Results.Created($"/students/{student.Id}", student);
                }*/


                return Results.NoContent();
            });

            app.MapDelete("/students/{id}", async (int id, IStudentService<StudentDTO> studentService) =>
            {
                var student = await studentService.GetById(id);

                if (student is not null)
                {
                    return Results.NoContent();
                }

                return Results.NotFound();
            });


        }
    }
}
