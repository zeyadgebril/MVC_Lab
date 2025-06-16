using Day2__Lab.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Day2__Lab.ViewModel
{
    public class CourseWithDeptList
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Course Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Range(maximum: 100, minimum: 30)]
        [Required]
        [Display(Name = "Degree(in Numbers)")]
        public int degree { get; set; }
        [Range(maximum: 100, minimum: 2)]
        [Display(Name = "Minimum Required Degree")]
        public int minDegree { get; set; }
        [Display(Name = "Credit Hours")]
        public int Hours { get; set; }
        public int Dept_id { get; set; }

        public List<Department> DeptList { get; set; }

    }
}
