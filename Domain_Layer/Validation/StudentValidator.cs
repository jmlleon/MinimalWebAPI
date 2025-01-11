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

            var errors = CommonErrors(student);

            return errors.Count == 0 ? ValidationResult.Success! : new ValidationResult(string.Join(",",errors));
        
        }

        public static ValidationResult IsValidOnUpdate(Student student, int studentId)
        {
            var errors = CommonErrors(student);            

            if (student != null && student.Id != studentId)
            {
                errors.Add($"The {nameof(student)} Id don't math");
            }

            return errors.Count == 0 ? ValidationResult.Success! : new ValidationResult(string.Join(",", errors));
        }

        private static List<string> CommonErrors(Student student) {

            List<string> errors = new();

            if (student == null)
            {
                errors.Add($"The {nameof(student)} can't be null");
            }

            if (student != null && student.Age <= 0)
            {
                errors.Add($"The {nameof(student)} age is not correct");
            }

            return errors;
        
        }

    }
}
