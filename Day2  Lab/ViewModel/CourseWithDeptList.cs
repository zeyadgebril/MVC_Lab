using Day2__Lab.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Day2__Lab.CustomAttribute;
using Microsoft.AspNetCore.Mvc;

namespace Day2__Lab.ViewModel
{
    public class CourseWithDeptList
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(20,ErrorMessage ="Name Must Be less then 20 Character")]
        [MinLength(2, ErrorMessage = "Name Must Be grater then 2 Character")]
        [Required]
        [UniqueCourseName]//custom validation 
        [Display(Name = "Course Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Range(50, 100, ErrorMessage = "must be between 50 and 100.")]
        [Required]
        public int degree { get; set; }
        
        [Remote("CheckMinDegree","Course",AdditionalFields = "degree")]
        public int minDegree { get; set; }


        [Display(Name = "Credit Hours")]
        [DivideBy3]//custome Validation 
        public int Hours { get; set; }
        public int Dept_id { get; set; }

        public List<Department> DeptList { get; set; }
        public int IsDeleted { get; set; }


    }
}
