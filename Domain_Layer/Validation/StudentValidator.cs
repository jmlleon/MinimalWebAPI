using Domain_Layer.Errors;
using Domain_Layer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Validation
{
    public class StudentValidator
    {

        public static ValidationResult IsValidOnAdd(Student student) {

            var errors = new List<string>();

            if (student == null)
            {
                errors.Add($"The {nameof(student)} can't be null");
            }

            if (student!=null && student.Age <= 0) 
            {
                errors.Add($"The {nameof(student)} age is not correct");
            }

            //if (studentAdd.Id != id) return Results.BadRequest("The Student Id don't math");

            return errors.Count == 0 ? ValidationResult.Success! : new ValidationResult(string.Join(",",errors));
        
        }

        public static ValidationResult IsValidOnUpdate(Student student, int studentId)
        {

            var errors = new List<string>();

            if (student == null)
            {
                errors.Add($"The {nameof(student)} can't be null");
            }

            if (student != null && student.Age <= 0)
            {
                errors.Add($"The {nameof(student)} age is not correct");
            }

            //if (studentAdd.Id != id) return Results.BadRequest("The Student Id don't math");

            return errors.Count == 0 ? ValidationResult.Success! : new ValidationResult(string.Join(",", errors));

        }

    }
}
