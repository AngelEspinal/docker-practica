using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alumnos.models
{
    public class Semester
    {
        public string IdSemester { get; set; }

        [ForeignKey(nameof(IdCourse))]
        public string IdCourse { get; set; }

        [ForeignKey(nameof(Career))]
        public string IdCareer { get; set; }

        public Course Course { get; set; }
        public Career Career { get; set; }
    }
}