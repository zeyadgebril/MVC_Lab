using System.ComponentModel.DataAnnotations.Schema;

namespace Day2__Lab.Models
{
    public class instructor
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string? Image { get; set; }
        public float salary { get; set; }
        public string? Address { get; set; }
        [ForeignKey("Department")]
        public int Dept_id { get; set; }
        public virtual Department Department { get; set; }
        [ForeignKey("Course")]
        public int Crs_id { get; set; }
        public virtual Course Course { get; set; }
    }
}
