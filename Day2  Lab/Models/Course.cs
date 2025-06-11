using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day2__Lab.Models
{
    public class Course
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        [Range(maximum:100,minimum:30)]
        [Required]
        public int degree { get; set; }
        [Range(maximum: 10, minimum: 2)]
        public int minDegree { get; set; }
        public int Hours { get; set; }

        [ForeignKey("Department")]
        public int Dept_id { get; set; }
        public virtual Department Department { get; set; }
    }
}
