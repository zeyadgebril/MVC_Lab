using System.ComponentModel.DataAnnotations;
using Day2__Lab.Models;
using Day2__Lab.ViewModel;

namespace Day2__Lab.CustomAttribute
{
    public class UniqueCourseNameAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var name = value.ToString();
            CompanyDbcontext db = new CompanyDbcontext();
            var courseData = (CourseWithDeptList)validationContext.ObjectInstance;
            var DataFromDB = db.Course.FirstOrDefault(c=>c.Name==name&&courseData.Dept_id==c.Dept_id);
            if (DataFromDB == null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"Course Is Alredey Available in Department");
        }
    }
}
