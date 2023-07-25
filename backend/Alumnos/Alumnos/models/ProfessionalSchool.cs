using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alumnos.models
{
    public class ProfessionalSchool
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProfessionalSchool { get; set; }
        public string NameProfessionalSchool { get; set; }

        public List<Career> Carrer{ get; set; }
    }
}