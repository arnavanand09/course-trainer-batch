using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseApi.Models
{
    public class Trainer
    {
        public Trainer()
        {
            TrainerName = string.Empty;
            Expertise = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
        }

        [Key]
        public int TrainerId { get; set; }

        [Required(ErrorMessage = "Trainer Name is required")]
        [MaxLength(100)]
        public string TrainerName { get; set; }

        [MaxLength(50)]
        public string Expertise { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        public int CreatedBy { get; set; } = 1;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
