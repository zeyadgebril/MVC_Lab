using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day2__Lab.Models
{
    public class Trainee
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public byte[]? Image { get; set; }
        [MaxLength(70)]
        public string? Address { get; set; }
        [Range (minimum:20,maximum:50)]
        public int grade { get; set; }
        [ForeignKey("Department")]
        public int Dept_id { get; set; }
        public virtual Department Department { get; set; }
    }
}
