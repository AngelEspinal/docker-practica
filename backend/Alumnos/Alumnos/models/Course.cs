using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alumnos.models
{
    public class Course
    {
        [Key]
        public string IdCourse { get; set; }
        public string NameCourse { get; set; }

        public List<Semester> Semesters { get; set; } = null;

    }
}