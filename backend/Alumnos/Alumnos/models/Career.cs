using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alumnos.models
{
    public class Career
    {
        [Key]
        public string IdCareer { get; set; }
        public string NameCareer { get; set; }

        [ForeignKey(nameof(ProfessionalSchool))]
        public int IdProfessionalSchool { get; set; }
        public List<Semester> Semester { get; set; }


        public ProfessionalSchool ProfessionalSchool { get; set; }
    }
}