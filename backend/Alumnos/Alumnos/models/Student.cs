using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alumnos.models
{
    public class Student
    {
        [Key]
        public string IdStudent{set;get;} 
        public string NameStudent{set;get;}


      
        public List<CourseStudent>? CourseStudent{get;set;}
    }
}