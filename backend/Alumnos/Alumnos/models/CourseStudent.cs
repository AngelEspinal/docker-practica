using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alumnos.models
{
    public class CourseStudent
    {
        [Column(Order = 0)]
        [ForeignKey(nameof(Student))]
        public string IdStudent { get; set; }

        [Column(Order = 1)]
        [ForeignKey(nameof(IdCourse))]
        public string IdCourse { get; set; }

        [Column(Order = 2)]
        public string IdTeacher { get; set; }

        [Column(Order = 3)]
        [ForeignKey(nameof(IdSemester))]
        public string IdSemester { get; set; }

        public Student Student { get; set;} 
        public Semester Semester { get; set; }
    
        
    }
}