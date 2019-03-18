namespace OnlineLearningWebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Courses")]
    public partial class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        [Required]
        [StringLength(100)]
        public string CourseLevel { get; set; }

        [DisplayFormat (DataFormatString = "{0:c}")]
        [Range (1, 100000)]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
