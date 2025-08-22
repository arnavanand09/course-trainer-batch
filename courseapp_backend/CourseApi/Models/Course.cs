using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseApi.Models
{
    public class Course
    {
        public Course()
        {
            CourseName = string.Empty;
            Description = string.Empty;
        }

        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course Name is required")]
        [MaxLength(100)]
        public string CourseName { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        public int Duration { get; set; }

        public int CreatedBy { get; set; } = 1;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Batch>? Batches { get; set; }

        public List<Batch>? Batch { get; set; }

    }
}