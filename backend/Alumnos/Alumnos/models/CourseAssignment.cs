using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alumnos.models
{
    public class CourseAssignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCarga { get; set; }
        [ForeignKey(nameof(Schedule))]
        public int? IdSchedule { get; set; } 

        [ForeignKey(nameof(Teacher))]
        public string IdTeacher { get; set; }

        public Teacher Teacher { get; set; }

        public Schedule? Schedule { get; set; }
    }
}