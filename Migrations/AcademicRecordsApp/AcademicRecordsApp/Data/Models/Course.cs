using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicRecordsApp.Data.Models
{
    public class Course
    {
        [Key]
        public int Id{ get; set; }

        [Required]
        [MaxLength(100)]
        public string Name{ get; set; } = string.Empty;

        public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();

        public virtual ICollection<StudentCourse> StudentsCourses { get; set; } = new List<StudentCourse>();
    }
}
