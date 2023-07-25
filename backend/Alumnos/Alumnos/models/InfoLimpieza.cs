using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Alumnos.models
{
    public class InfoLimpieza
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string IdStudent { get; set; }


        public string IdCourse { get; set; }


        public string IdTeacher { get; set; }



        public string IdSemester { get; set; }

        public string Day { get; set; }
        public string? Aula { get; set; }
        public string? HoraInit { get; set; }
        public string? HorEnd { get; set; }

        
        

    }
}