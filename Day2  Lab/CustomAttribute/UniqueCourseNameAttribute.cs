using System.ComponentModel.DataAnnotations;
using Day2__Lab.Models;
using Day2__Lab.ViewModel;

namespace Day2__Lab.CustomAttribute
{
    public class UniqueCourseNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var db = validationContext.GetService<CompanyDbcontext>();//Incase you Inject Using SQL-Injection 

            if (db == null)
            {
                throw new Exception("DbContext is not available in ValidationContext.");
            }

            var name = value?.ToString();
            var courseData = (CourseWithDeptList)validationContext.ObjectInstance;

            var existingCourse = db.Course.FirstOrDefault(c =>
                c.Name == name && c.Dept_id == courseData.Dept_id);

            if (existingCourse == null)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Course is already available in this department.");
        }
    }
}
