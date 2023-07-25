using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alumnos.models
{
    public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSchedule { get; set; }

        public string IdSemester { get; set; }
        public string IdCourse { get; set; }


        //---------Columnas adicionales Solucion
        [ForeignKey("IdSemester, IdCourse")]
        public virtual Semester Semester { get; set; }


        public string Day { get; set; }
        public string? HorInit { get; set; }
        public string? HorEnd { get; set; }

        public CourseAssignment? CourseAssignments { get; set; }
    }


}