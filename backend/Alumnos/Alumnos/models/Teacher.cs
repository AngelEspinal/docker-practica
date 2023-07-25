using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alumnos.models
{
    public class Teacher
    {
        [Key]
        public string IdTeacher{set;get;} 
        public string NameTeacher{set;get;}
         
        public ICollection<CourseAssignment> CourseAssignment { get; set; }

    }
}