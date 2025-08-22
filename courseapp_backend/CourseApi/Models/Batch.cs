
﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

﻿using System.ComponentModel.DataAnnotations.Schema;


namespace CourseApi.Models
{
    public class Batch
    {

        [Key]
        public int BatchId { get; set; }

        [Required(ErrorMessage = "Batch Name is required")]
        [MaxLength(100)]
        public string BatchName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;

        // ✅ Foreign Keys
        [ForeignKey("Course")] // Correct way to specify foreign key
        [Required(ErrorMessage = "CourseId is required")]
        public int CourseId { get; set; }

        [ForeignKey("Trainer")] // Correct way to specify foreign key
        public int? TrainerId { get; set; }

        // ✅ Audit fields
        public int CreatedBy { get; set; } = 1;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        // ✅ Navigation properties
        public Course? Course { get; set; }
        public Trainer? Trainer { get; set; }
    }
}