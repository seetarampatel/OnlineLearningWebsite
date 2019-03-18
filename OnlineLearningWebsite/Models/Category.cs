namespace OnlineLearningWebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Courses = new HashSet<Course>();
        }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryArea { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [DisplayFormat (DataFormatString = "{0:n2}")]
        [Range (1, 10)]
        public int CategoryReview { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course> Courses { get; set; }
    }
}
