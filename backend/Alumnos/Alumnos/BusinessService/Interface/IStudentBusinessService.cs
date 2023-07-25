using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alumnos.models;

namespace Alumnos.BusinessService.Interface
{
    public interface IStudentBusinessService
    {
        Student GetStudentById(string IdStudent);
        IEnumerable<Student> GetAllStudent();
        Student AddStudent(Student student);
    }
}