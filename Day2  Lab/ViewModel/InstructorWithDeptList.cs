using Day2__Lab.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day2__Lab.ViewModel
{
    public class InstructorWithDeptList
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string? Image { get; set; }
        public float salary { get; set; }
        public string? Address { get; set; }
        public int Dept_id { get; set; }
        public int Crs_id { get; set; }
        public List<Department> DeptList { get; set; }
        public List<Course> CourseList{ get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
