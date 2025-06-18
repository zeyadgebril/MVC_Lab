using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day2__Lab.Models
{
    public class crsResult
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int degree { get; set; }
        [ForeignKey("Course")]
        public int Crs_id { get; set; }
        public virtual Course? Course { get; set; }
        [ForeignKey("Trainee")]
        public int Traniee_id { get; set; }
        public virtual Trainee? Trainee { get; set; }
        public int? IsDeleted { get; set; }

    }
}
